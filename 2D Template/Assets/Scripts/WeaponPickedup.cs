using System.Collections;
using System.Collections.Generic;
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
    private List<GameObject> weapons = new List<GameObject>();

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ammo")
        {
            TryPickupAmmo(collision.gameObject);
        }
    }

    void CheckForWeapon()
    {
        Collider2D[] nearbyWeapons = Physics2D.OverlapCircleAll(transform.position, Range);
        weaponInRange = null;

        foreach (var collider in nearbyWeapons)
        {
            if (collider.CompareTag("Weapon") && collider.transform.parent != WeaponHolder.transform)
            {
                //if (collider.name == "Ammo")
                //{
                //    TryPickupAmmo(collider.gameObject);
                //    return;
                //}

                weaponInRange = collider.gameObject;
                ShowPickupPrompt(weaponInRange.name);
                return;
            }
        }

        HidePickupPrompt();
    }

    void TryPickupAmmo(GameObject ammo)
    {
        float distance = Vector3.Distance(transform.position, ammo.transform.position);
        if (distance <= 1.2f)
        {
            Shooting.Instance.Ammo += UnityEngine.Random.Range(5, 20);
            Destroy(ammo);
        }
    }

    void ShowPickupPrompt(string weaponName)
    {
        if (pickupUIPrompt != null)
        {
            if (weaponName == "Ammo")
            {
                pickupUIPrompt.GetComponent<TMP_Text>().SetText($"Pick Up: {weaponName}");
                pickupUIPrompt.SetActive(true);
                return;
            }
            else
            {
                pickupUIPrompt.GetComponent<TMP_Text>().SetText($"Press [E] To Pick Up: {weaponName}");
                pickupUIPrompt.SetActive(true);
                return;
            }
        }
    }

    void HidePickupPrompt()
    {
        if (pickupUIPrompt != null)
        {
            pickupUIPrompt.SetActive(false);
        }
    }

    void PickupWeapon()
    {
        if (weaponInRange == null) return;

        //if (weaponInRange.name == "Ammo")
        //{
        //    TryPickupAmmo(weaponInRange);
        //}
        else if (weapons.Count < weaponSlots.Length)
        {
            weaponInRange.transform.SetParent(WeaponHolder.transform);
            weaponInRange.GetComponent<BoxCollider2D>().enabled = false;
            weaponInRange.SetActive(false);
            weapons.Add(weaponInRange);
        }

        HidePickupPrompt();
        weaponInRange = null;
    }

    void EquipWeaponBySlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= weapons.Count) return;

        GameObject weapon = weapons[slotIndex];

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

        // Deactivate the currently equipped weapon
        if (currentlyEquippedWeapon != null)
        {
            currentlyEquippedWeapon.SetActive(false);
            Shooting shooting = currentlyEquippedWeapon.GetComponent<Shooting>();
            if (shooting != null)
            {
                shooting.StopAllCoroutines();
                shooting.enabled = false;
            }
        }

        // Equip the new weapon
        weapon.SetActive(true);
        currentlyEquippedWeapon = weapon;

        playerMovement.SetEquippedWeapon(weapon);

        weapon.transform.localPosition = new Vector3(0.5f, 0, 0);
        weapon.transform.localRotation = Quaternion.Euler(0, -180, 0);

        Shooting newShooting = weapon.GetComponent<Shooting>();
        if (newShooting != null)
        {
            newShooting.enabled = true;
            newShooting.ResetState();
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

    private void OnDrawGizmosSelected()
    {
        // Visualize weapon detection range in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}