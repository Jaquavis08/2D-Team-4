using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class healthbar : MonoBehaviour
{
    public static healthbar Instance;

    public float lifemax;
    public GameObject Player;
    public GameObject Pivot;
    public GameObject percent;
    public GameObject GameOver;

    private float life;
    void Start()
    {
        life = lifemax;
        GameOver.SetActive(false);
        StartCoroutine(PassiveHeal());
    }

    IEnumerator PassiveHeal()
    {
        while (true)
        {

            yield return new WaitForSeconds(15); // Heal every 15 seconds
            if (life < lifemax)
            Heal(1);
        }
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


    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(life / lifemax,1,1);

        transform.GetChild(0).localScale = new Vector3(life / lifemax,1,1);

        if(IsMouseOverUI())
        {
            percent.GetComponent<TMP_Text>().SetText(life +  "%");
            percent.SetActive(true);
        }
        else
        {
            percent.SetActive(false);
        }

        if (life <= 100)
        {
            if (life >= 51)
                for (int i = 0; i < 3; i++)
                {
                    Pivot.transform.GetChild(i).GetComponent<Image>().color = Color.green;
                }
        }

        if (life <= 50)
        {
            if(life >= 26)
            for(int i = 0; i < 3; i++)
            {
                Pivot.transform.GetChild(i).GetComponent<Image>().color = Color.yellow;
            }
        }

        if (life <= 25)
        {
            if (life >= 0)
                for (int i = 0; i < 3; i++)
                {
                    Pivot.transform.GetChild(i).GetComponent<Image>().color = Color.red;
                }
        }

        if(life <= 0)
        {
            Time.timeScale = 0f;
            GameOver.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            GameOver.SetActive(false);
        }
    }
    public void Hurt(float subtrahend)
    {
        life -= subtrahend;
        if (life < 0)
        {
            life = 0;
            Time.timeScale = 0f;
            GameOver.SetActive(true);
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
    public float GetLife()
    {
        return life;
    }
}
