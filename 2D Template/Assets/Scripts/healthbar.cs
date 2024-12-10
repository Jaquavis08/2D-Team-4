using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    public static healthbar Instance;

<<<<<<< HEAD:2D Template/Assets/healthbar.cs

    float life;
=======
    public float life;
>>>>>>> b53ccbfe76b559726ebc348e7538d3d824ff2d70:2D Template/Assets/Scripts/healthbar.cs
    public float lifemax;
    public GameObject Player;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD:2D Template/Assets/healthbar.cs
        life = 100;
=======
        life = lifemax;
>>>>>>> b53ccbfe76b559726ebc348e7538d3d824ff2d70:2D Template/Assets/Scripts/healthbar.cs
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
<<<<<<< HEAD:2D Template/Assets/healthbar.cs
        transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(life / lifemax,1,1);
=======

        transform.GetChild(0).localScale = new Vector3(life / lifemax,1,1);
        //this.GetComponent<Slider>().value = new Vector3 (life / lifemax,1,1);
>>>>>>> b53ccbfe76b559726ebc348e7538d3d824ff2d70:2D Template/Assets/Scripts/healthbar.cs
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
