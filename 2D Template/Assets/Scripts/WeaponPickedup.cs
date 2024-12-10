using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponPickedup : MonoBehaviour
{
    public GameObject WeaponHolder;
    public KeyCode Slot1 = KeyCode.Alpha1, Slot2 = KeyCode.Alpha2;
    public GameObject GunUi;
    public float EquipDelay;

    private GameObject currentlyEquippedWeapon = null;

    // Pickup //
    public float Range;
    public KeyCode Pickup = KeyCode.E;
    private GameObject WeaponInRange;
    public GameObject pickupUIPrompt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Weapon"))
        //{
        //    Debug.Log("Weapon picked up: " + collision.gameObject.name);

        //    collision.transform.SetParent(WeaponHolder.transform);
        //    collision.GetComponent<BoxCollider2D>().enabled = false;
        //    collision.gameObject.SetActive(false);

        //    // Destroy(collision.gameObject);
        //}

        if (collision.CompareTag("Weapon") && collision.name == "Ammo")
        {
            Shooting.Instance.Ammo += 15;
            Destroy(collision.gameObject);
            pickupUIPrompt.SetActive(false);
        }
    }


    void CheckForWeapon()
    {
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
        WeaponInRange = null;
        foreach (GameObject weapon in weapons)
        {
            float distance = Vector3.Distance(this.gameObject.transform.position, weapon.transform.position);

            if (distance <= Range && !weapon.GetComponentInParent<PlayerMovement>())
            {
                WeaponInRange = weapon;

                if (pickupUIPrompt != null)
                {
                    pickupUIPrompt.GetComponent<TMP_Text>().SetText("Press [E] To Pick Up: " + weapon.name);
                    pickupUIPrompt.SetActive(true);
                    return;
                }
            }

            if (pickupUIPrompt != null)
            {
                pickupUIPrompt.SetActive(false);
            }
        }
    }

    void PickupWeapon()
    {
        if (WeaponInRange.name == "M1911")
        {
            WeaponInRange.transform.SetParent(WeaponHolder.transform);
            WeaponInRange.GetComponent<BoxCollider2D>().enabled = false;
            WeaponInRange.gameObject.SetActive(false);
        }

        if (WeaponInRange.name == "Ammo")
        {
            Shooting.Instance.Ammo += 15;
            Destroy(WeaponInRange.gameObject);
        }

            //Destroy(WeaponInRange.gameObject);
            if (pickupUIPrompt != null)
        {
            pickupUIPrompt.SetActive(false);
        }
        WeaponInRange = null;
    }


    // Update is called once per frame
    void Update()
    {
        CheckForWeapon();

        if(WeaponInRange != null && Input.GetKeyDown(Pickup))
        {
            PickupWeapon();
        }


        if (Input.GetKeyDown(Slot2))
        {
            StartCoroutine(EquipedDelayed());
        }

        if (Input.GetKeyDown(Slot1))
        {
            EquipWeapon("Bat");
        }


    }

    IEnumerator EquipedDelayed()
    {
        yield return new WaitForSeconds(EquipDelay);
        EquipWeapon("M1911");
        if(currentlyEquippedWeapon != null)
        {
            GunUi.SetActive(true);
        }
    }

    private IEnumerator UnequipDelayed()
    {
        yield return new WaitForSeconds(EquipDelay);

        if (currentlyEquippedWeapon != null)
        {
            
            Shooting shooting = currentlyEquippedWeapon.GetComponent<Shooting>();
            if (shooting != null)
            {
                shooting.StopAllCoroutines();
                shooting.enabled = false; 
            }

            currentlyEquippedWeapon.SetActive(false);
            Debug.Log(currentlyEquippedWeapon.name + " has been unequipped!");
        }
    }

    private void EquipWeapon(string weaponName)
    {
        if (WeaponHolder != null)
        {
            
            Transform newWeaponTransform = WeaponHolder.transform.Find(weaponName);
            if (newWeaponTransform != null)
            {
                GameObject newWeapon = newWeaponTransform.gameObject;

                
                if (currentlyEquippedWeapon == newWeapon)
                {
                    
                    newWeapon.SetActive(false);
                    currentlyEquippedWeapon = null;
                    Debug.Log(weaponName + " has been unequipped!");
                }
                else
                {
                    
                    if (currentlyEquippedWeapon != null)
                    {
                        StartCoroutine(UnequipDelayed());
                    }

                    
                    newWeapon.SetActive(true);
                    currentlyEquippedWeapon = newWeapon;

                   
                    newWeaponTransform.localPosition = new Vector3 (0.5f,0,0);
                    newWeaponTransform.localRotation = Quaternion.Euler(0,-180,0);


                    Shooting shooting = newWeapon.GetComponent<Shooting>();
                    if (shooting != null)
                    {
                        shooting.enabled = true;
                        shooting.ResetState();
                    }

                    newWeapon.transform.GetComponent<Shooting>().enabled = true;

                    Debug.Log(weaponName + " has been equipped and positioned!");
                }
            }
            else
            {
                Debug.LogWarning("No weapon named '" + weaponName + "' found in WeaponHolder!");
            }
        }
        else
        {
            Debug.LogError("WeaponHolder is not assigned in the Inspector!");
        }
    }
}
