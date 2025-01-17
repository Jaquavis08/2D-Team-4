using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitresponse : MonoBehaviour
{
    SpriteRenderer sprRend;
    // Start is called before the first frame update
    void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sprRend.color[3]>0)
        {
            sprRend.color -= new Color(0,0,0,0.1f);
        }
    }
    public void hit()
    {
        sprRend.color = new Color(1,1,1, 1);
    }
}
