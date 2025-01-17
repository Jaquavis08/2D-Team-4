using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    private GameObject Object;
    private float ry;

    // Start is called before the first frame update

    private void Awake()
    {

    }
    void Start()
    {
        Object = this.gameObject;
        ry = Random.Range(0, 360);
        Object.transform.eulerAngles = new Vector3(Object.transform.rotation.x, Object.transform.rotation.z, ry * 1.65f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
