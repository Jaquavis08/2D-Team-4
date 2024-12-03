using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthbar : MonoBehaviour
{
    float life = 50;
    public float lifemax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).localScale = new Vector3(life / lifemax,1,1);
    }
    public void Hurt(float subtrahend)
    {
        life -= subtrahend;
        if (life<0)
        {
            life = 0;
        }
    }
    public void Heal(float addend)
    {
        life += addend;
        if (life>lifemax)
        {
            life = lifemax;
        }
    }
}
