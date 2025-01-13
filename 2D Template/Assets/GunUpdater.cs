using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GunUpdater : MonoBehaviour
{
    public TMP_Text Name;
    public TMP_Text MagAndAmmo;
    public GameObject WeaponHolder;
    public GameObject Image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponHolder.GetComponentInChildren<Shooting>() != null || WeaponHolder.GetComponentInChildren<SpriteRenderer>() != null)
        {
            if (WeaponHolder.GetComponentInChildren<SpriteRenderer>().gameObject.name == "Flashlight")
            {
                Name.text = "Flashlight";
                MagAndAmmo.text = "0/0";
                Image.GetComponent<Image>().sprite = null;
            }
            else
            {
                Name.text = WeaponHolder.GetComponentInChildren<Shooting>().name;
                MagAndAmmo.text = WeaponHolder.GetComponentInChildren<Shooting>().MagAmount + "/" + WeaponHolder.GetComponentInChildren<Shooting>().Ammo;
                Image.GetComponent<Image>().sprite = WeaponHolder.GetComponentInChildren<Shooting>().GunSprite;
            }
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
