using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    public static healthbar Instance;

    public float life;
    public float lifemax;
    public GameObject Player;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        life = lifemax;
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

        transform.GetChild(0).localScale = new Vector3(life / lifemax,1,1);
        //this.GetComponent<Slider>().value = new Vector3 (life / lifemax,1,1);
    }
    public void Hurt(float subtrahend)
    {
        life -= subtrahend;
        if (life < 0)
        {
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
