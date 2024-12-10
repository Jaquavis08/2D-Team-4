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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponHolder.GetComponentInChildren<Shooting>() != null)
        {
            Name.text = WeaponHolder.GetComponentInChildren<Shooting>().GunName;
            MagAndAmmo.text = WeaponHolder.GetComponentInChildren<Shooting>().MagAmount + "/" + WeaponHolder.GetComponentInChildren<Shooting>().Ammo;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
