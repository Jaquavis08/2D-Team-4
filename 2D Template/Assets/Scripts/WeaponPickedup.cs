using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPickedup : MonoBehaviour
{
    public GameObject WeaponHolder;
    public KeyCode Slot1 = KeyCode.Alpha1, Slot2 = KeyCode.Alpha2;
    public GameObject GunUi;
    public float EquipDelay;

    private GameObject currentlyEquippedWeapon = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            Debug.Log("Weapon picked up: " + collision.gameObject.name);

            collision.transform.SetParent(WeaponHolder.transform);
            collision.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.SetActive(false);

            // Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Ammo"))
        {
            Shooting.Instance.Ammo += 15;
            Destroy(collision.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        EquipWeapon("Gun");
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
