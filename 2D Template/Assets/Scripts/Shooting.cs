using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform FirePiont;
    public GameObject BulletPrefab;
    public int BulletAmount;
    public int MaxBulletCount;
    public float BulletCooldown;
    public float ReloadCooldown;
    public float bulletForce = 20f;
    private bool canShoot = true;
    private bool isShooting = false;


    private void Start()
    {
        BulletAmount = MaxBulletCount;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (canShoot)
            StartCoroutine(ShootDelay());
        }
        
        if (Input.GetKeyDown(KeyCode.R))
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
        if (BulletAmount > 0)
        {
            isShooting = true;
            BulletAmount -= 1;
            GameObject Bullet = Instantiate(BulletPrefab, FirePiont.position, FirePiont.rotation);
            Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(FirePiont.up * bulletForce, ForceMode2D.Impulse);
            StartCoroutine(ShootBullet(Bullet));
        }
        if (BulletAmount < 0)
            canShoot = false;
        yield return new WaitForSeconds(BulletCooldown);
        isShooting = false;
        canShoot = true;
    }

    private IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(ReloadCooldown);
        if(!isShooting)
        {
            BulletAmount = MaxBulletCount;
            canShoot = true;
        }
    }

}
