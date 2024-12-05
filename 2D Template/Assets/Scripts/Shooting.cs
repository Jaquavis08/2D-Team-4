using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public static Shooting Instance;
    public Transform FirePiont;
    public GameObject BulletPrefab;
    private int MagAmount;
    public int MaxMagAmount;
    public int Ammo;
    public float BulletCooldown;
    public float ReloadCooldown;
    public float bulletForce = 20f;
    private bool canShoot = true;
    private bool isShooting = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        MagAmount = MaxMagAmount;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(MagAmount <= 0 && MagAmount < MaxMagAmount && Ammo > 0)
                StartCoroutine(ReloadDelay());
            if (canShoot)
                StartCoroutine(ShootDelay());
        }
        
        if (Input.GetKeyDown(KeyCode.R) && MagAmount < MaxMagAmount && Ammo > 0)
        {
            StartCoroutine(ReloadDelay());
        }
    }

    private IEnumerator ShootBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(2f);
        Destroy(bullet);
    }

    private IEnumerator ShootDelay()
    {
        canShoot = false;
        if (MagAmount > 0)
        {
            isShooting = true;
            MagAmount --;
            GameObject Bullet = Instantiate(BulletPrefab, FirePiont.position, FirePiont.rotation);
            Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(FirePiont.up * bulletForce, ForceMode2D.Impulse);
            StartCoroutine(ShootBullet(Bullet));
            yield return new WaitForSeconds(BulletCooldown);
            isShooting = false;
            canShoot = true;
        }
        if (MagAmount < 0)
            canShoot = false;
    }

    private IEnumerator ReloadDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(ReloadCooldown);
        if(isShooting == false)
        {
            int ammoNeeded = MaxMagAmount - MagAmount;

            int ammoToReload = Mathf.Min(ammoNeeded, Ammo);
            MagAmount += ammoToReload;
            Ammo -= ammoToReload;
            canShoot = true;
        }
    }

}
