using System.Collections;
using UnityEngine;
using TMPro;

public class WeaponPickedup : MonoBehaviour
{
    public GameObject WeaponHolder;
    public KeyCode[] weaponSlots = { KeyCode.Alpha1, KeyCode.Alpha2 };
    public GameObject GunUi;
    public float EquipDelay = 0.5f;

    private GameObject currentlyEquippedWeapon = null;

    // Pickup Variables
    public float Range = 5f;
    public KeyCode PickupKey = KeyCode.E;
    private GameObject weaponInRange;
    public GameObject pickupUIPrompt;

    private PlayerMovement playerMovement;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Ammo")
        {
            Shooting.Instance.Ammo += Random.Range(5, 20);
            Destroy(weaponInRange.gameObject);
        }
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        CheckForWeapon();

        // Handle pickup
        if (weaponInRange != null && Input.GetKeyDown(PickupKey))
        {
            PickupWeapon();
        }

        // Handle weapon slot selection
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (Input.GetKeyDown(weaponSlots[i]))
            {
                EquipWeaponBySlot(i);
            }
        }
    }

    void CheckForWeapon()
    {
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
        weaponInRange = null;

        foreach (GameObject weapon in weapons)
        {
            float distance = Vector3.Distance(transform.position, weapon.transform.position);

            if (distance <= Range && weapon.transform.parent != WeaponHolder.transform)
            {
                weaponInRange = weapon;

                if (pickupUIPrompt != null)
                {
                    pickupUIPrompt.GetComponent<TMP_Text>().SetText($"Press [E] To Pick Up: {weapon.name}");
                    pickupUIPrompt.SetActive(true);
                }
                return;
            }
        }

        if (pickupUIPrompt != null)
        {
            pickupUIPrompt.SetActive(false);
        }
    }

    void PickupWeapon()
    {
        if (weaponInRange == null) return;

        if(weaponInRange.name == "Ammo")
        {
            Shooting.Instance.Ammo += Random.Range(10, 20);
            Destroy(weaponInRange.gameObject);

        }
        if(weaponInRange.name != "Ammo")
        {
            weaponInRange.transform.SetParent(WeaponHolder.transform);
            weaponInRange.GetComponent<BoxCollider2D>().enabled = false;
            weaponInRange.gameObject.SetActive(false);
        }

        if (pickupUIPrompt != null)
        {
            pickupUIPrompt.SetActive(false);
        }

        weaponInRange = null;
    }

    void EquipWeaponBySlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= WeaponHolder.transform.childCount) return;

        Transform weaponTransform = WeaponHolder.transform.GetChild(slotIndex);
        if (weaponTransform == null) return;

        GameObject weapon = weaponTransform.gameObject;

        if (currentlyEquippedWeapon == weapon)
        {
            StartCoroutine(UnequipWeapon());
        }
        else
        {
            StartCoroutine(EquipWeapon(weapon));
        }
    }

    IEnumerator EquipWeapon(GameObject weapon)
    {
        yield return new WaitForSeconds(EquipDelay);

        if (currentlyEquippedWeapon != null)
        {
            StartCoroutine(UnequipWeapon());
        }

        weapon.SetActive(true);
        currentlyEquippedWeapon = weapon;

        playerMovement.SetEquippedWeapon(weapon);

        weapon.transform.localPosition = new Vector3(0.5f, 0, 0);
        weapon.transform.localRotation = Quaternion.Euler(0, -180, 0);

        Shooting shooting = weapon.GetComponent<Shooting>();
        if (shooting != null)
        {
            shooting.enabled = true;
            shooting.ResetState();
        }

        if (GunUi != null)
        {
            GunUi.SetActive(true);
        }
    }

    IEnumerator UnequipWeapon()
    {
        yield return new WaitForSeconds(EquipDelay);

        if (currentlyEquippedWeapon == null) yield break;

        Shooting shooting = currentlyEquippedWeapon.GetComponent<Shooting>();
        if (shooting != null)
        {
            shooting.StopAllCoroutines();
            shooting.enabled = false;
        }

        currentlyEquippedWeapon.SetActive(false);
        playerMovement.SetEquippedWeapon(null); // Notify PlayerMovement
        currentlyEquippedWeapon = null;
    }
}
