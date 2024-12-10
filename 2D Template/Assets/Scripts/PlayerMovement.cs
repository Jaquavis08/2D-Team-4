using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Camera mainCamera;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer weaponSpriteRenderer;
    public GameObject Weapon;
    private Rigidbody2D WeaponRb;
    public Sprite PlayerBack;
    public Sprite PlayerFront;
    public GameObject PlayerGFX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        WeaponRb = Weapon.GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        spriteRenderer = PlayerGFX.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0)
        {
            spriteRenderer.flipX = movement.x > 0;
        }

        if (Input.GetKey(KeyCode.W))
        {
            spriteRenderer.sprite = PlayerBack;
        }

        if (Input.GetKey(KeyCode.S))
        {
            spriteRenderer.sprite = PlayerFront;
        }

        movement = movement.normalized;

        RotateTowardsMouse();
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }
    void RotateTowardsMouse()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set weapon position and rotation
        Weapon.transform.position = new Vector2(rb.position.x, rb.position.y);
        WeaponRb.rotation = angle;

        // Check if the angle is between 90 and -90 degrees
        bool shouldFlip = angle > 90f || angle < -90f;

        // Flip all child renderers of the weapon

        foreach (Transform child in Weapon.transform)
        {
            Transform firepoint = Weapon.transform.Find("M1911").transform.Find("Firepoint");
            SpriteRenderer renderer = child.GetComponent<SpriteRenderer>();

            if (Input.GetKey(KeyCode.W))
            {
                renderer.sortingOrder = 0;
            }

            if (Input.GetKey(KeyCode.S))
            { 
                renderer.sortingOrder = 1;
            }

            if (renderer != null)
            {

                renderer.flipY = shouldFlip;
                if (shouldFlip == true)
                {
                    firepoint.localPosition = new Vector3(-7.11f, -1.03f, firepoint.localPosition.z);
                }
                else
                {
                    firepoint.localPosition = new Vector3(-7.11f, 1.03f, firepoint.localPosition.z);
                }
            }
        }
    }
    public void physics()
    {
        rb.velocity = Vector3.zero;
    }
}
