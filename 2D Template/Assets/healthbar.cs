using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthbar : MonoBehaviour
{
    public static healthbar Instance;


    float life;
    public float lifemax;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        life = 100;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Player);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(life / lifemax,1,1);
    }
    public void Hurt(float subtrahend)
    {
        life -= subtrahend;
        if (life < 0)
        {
            life = 0;
            Destroy(Player);
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
