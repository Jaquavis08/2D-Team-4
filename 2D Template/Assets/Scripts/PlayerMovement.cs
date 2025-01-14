using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameObject influence;
    public static PlayerMovement Instance;
    bool invincible = false;
    public float fademin;
    public float fademax;
    public float fadestep;
    int fadedir = 0;
    public float MinTime = 0; //min jumpscare time
    public float MaxTime = 1200; // max jumpscare time
    public GameObject JumpScare;

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

    private bool MovementInvert = false;

    private Dictionary<KeyCode, int> sortingOrderMapping = new Dictionary<KeyCode, int>
    {
        { KeyCode.W, 0 },
        { KeyCode.S, 1 }
    };

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

        if (invincible)
        {

            if (fadedir == 0)
            {
                spriteRenderer.color -= new Color(0, 0, 0, fadestep);
            }
            else
            {
                spriteRenderer.color += new Color(0, 0, 0, fadestep);
            }
            if (spriteRenderer.color.a <= fademin)
            {
                fadedir = 1;
            }
            else if (spriteRenderer.color.a >= fademax)
            {
                fadedir = 0;
            }
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }

        if (influence != null) // curse nun script
        {
            MovementInvert = true;
        }
        else
        {
            MovementInvert = false;
        }

        MinTime += 1f * Time.deltaTime;
        if(MinTime >= MaxTime)
        {
            MinTime = 0;
            StartCoroutine(jumpScare());
        }
    }

    IEnumerator jumpScare()
    {
        JumpScare.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.35f);
        JumpScare.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (MovementInvert)
        {
            rb.velocity = -movement * moveSpeed;
        }
        else
        {
            rb.velocity = movement * moveSpeed;
        }
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
            if(firepoint != null)
            firepoint.localPosition = shouldFlip ? firepointFlippedOffset : firepointOffset;

            //// If facing backwards (W key), send the weapon behind the player by adjusting sorting order
            //if (Input.GetKeyDown(moveUpKey)) // Player is facing backwards
            //{
            //    renderer.sortingOrder = -1;  // Weapon appears behind the player
            //}
            //if (Input.GetKeyDown(moveDownKey)) // Player is facing forwards (S key)
            //{
            //    renderer.sortingOrder = 1;  // Weapon appears in front of the player
            //}
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy" && !invincible)
        {
            GameObject.FindWithTag("hb").GetComponent<healthbar>().Hurt(5);
            invincible = true;
            Invoke("xes",0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        
        if (other.gameObject.tag=="curse")
        {
            influence = other.gameObject.GetComponent<curse>().GetSender();
            Destroy(other.gameObject);
        }
    }
    void xes()
    {
        invincible = false;
    }
}
