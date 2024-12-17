using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlashlightHolder : MonoBehaviour
{
    public Transform flashlight;
    public Camera mainCamera;

    // Update is called once per frame
    void Update()
    {
        if (flashlight == null || mainCamera == null) return;

        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - flashlight.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        flashlight.rotation = Quaternion.Euler(0, 0, angle);
    }
}
