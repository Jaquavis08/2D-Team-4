using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlashlightHolder : MonoBehaviour
{
    public Transform flashlight;
    public Rigidbody2D flashRb;
    public Camera mainCamera;
    public float offsetDistance = 1f;
    public Rigidbody2D Rb;
    public GameObject FlashlightHold;


    private void Start()
    {
        flashRb = flashlight.GetComponent<Rigidbody2D>();
        Rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
        if (FlashlightHold == null) return;

        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set weapon position and rotation based on mouse direction and distance
        FlashlightHold.transform.position = (Vector2)transform.position + direction * offsetDistance;
        flashRb.rotation = angle;

        //bool shouldFlip = angle > 90f || angle < -90f;

        // Get the weapon's SpriteRenderer
        //SpriteRenderer renderer = FlashlightHold.GetComponentInChildren<SpriteRenderer>();
        //if (renderer != null)
        //{
        //    renderer.flipY = shouldFlip;


        //    // If facing backwards (W key), send the weapon behind the player by adjusting sorting order
        //    if (Input.GetKeyDown(moveUpKey)) // Player is facing backwards
        //    {
        //        renderer.sortingOrder = -1;  // Weapon appears behind the player
        //    }
        //    if (Input.GetKeyDown(moveDownKey)) // Player is facing forwards (S key)
        //    {
        //        renderer.sortingOrder = 1;  // Weapon appears in front of the player
        //    }
    }
}
