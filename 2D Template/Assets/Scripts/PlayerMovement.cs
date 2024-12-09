using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Camera mainCamera;

    private SpriteRenderer spriteRenderer;
    private GameObject equippedWeapon;
    private Rigidbody2D weaponRb;
    [SerializeField] private Sprite playerBack;
    [SerializeField] private Sprite playerFront;
    [SerializeField] private GameObject playerGFX;

    [SerializeField] private Transform firepoint;
    [SerializeField] private Vector3 firepointOffset = new Vector3(-7.11f, 1.03f, 0);
    [SerializeField] private Vector3 firepointFlippedOffset = new Vector3(-7.11f, -1.03f, 0);

    [SerializeField] private KeyCode moveUpKey = KeyCode.W;
    [SerializeField] private KeyCode moveDownKey = KeyCode.S;

    private Dictionary<KeyCode, int> sortingOrderMapping = new Dictionary<KeyCode, int>
    {
        { KeyCode.W, 0 },
        { KeyCode.S, 1 }
    };

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        spriteRenderer = playerGFX.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

       
        if (movement.x != 0)
        {
            spriteRenderer.flipX = movement.x > 0;
        }

        
        if (Input.GetKey(moveUpKey))
        {
            spriteRenderer.sprite = playerBack;
        }
        else if (Input.GetKey(moveDownKey))
        {
            spriteRenderer.sprite = playerFront;
        }

       
        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }

        RotateTowardsMouse();
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }

    [SerializeField] private float weaponDistance = 1.5f;

    void RotateTowardsMouse()
    {
        if (equippedWeapon == null) return;

        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set weapon position and rotation based on mouse direction and distance
        equippedWeapon.transform.position = rb.position + (Vector2)(direction * weaponDistance);
        weaponRb.rotation = angle;

        bool shouldFlip = angle > 90f || angle < -90f;

        // Get the weapon's SpriteRenderer
        SpriteRenderer renderer = equippedWeapon.GetComponentInChildren<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.flipY = shouldFlip;

            // Adjust firepoint position
            firepoint.localPosition = shouldFlip ? firepointFlippedOffset : firepointOffset;

            // If facing backwards (W key), send the weapon behind the player by adjusting sorting order
            if (Input.GetKeyDown(moveUpKey)) // Player is facing backwards
            {
                renderer.sortingOrder = -1;  // Weapon appears behind the player
            }
            if(Input.GetKeyDown(moveDownKey)) // Player is facing forwards (S key)
            {
                renderer.sortingOrder = 1;  // Weapon appears in front of the player
            }
        }
    }



    public void SetEquippedWeapon(GameObject weapon)
    {
        equippedWeapon = weapon;
        if (equippedWeapon != null)
        {
            weaponRb = equippedWeapon.GetComponent<Rigidbody2D>();
            firepoint = equippedWeapon.transform.Find("Firepoint");
        }
    }

    public void StopPhysics()
    {
        rb.velocity = Vector3.zero;
    }
}
