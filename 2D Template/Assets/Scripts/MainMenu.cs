using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{

    public void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0 && SceneManager.GetActiveScene().name == "MainMenu")
        {

            Time.timeScale = 1f;
            healthbar.Instance.lifemax = 100;
            healthbar.Instance.Heal(100);
            if (this.gameObject.name == "GameOver")
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void PickAScene(string SceneName)

    {
        Time.timeScale = 1f;
        if (healthbar.Instance != null)
        {
            //healthbar.Instance.lifemax = 100;
            healthbar.Instance.Heal(100);
        }
        if (this.gameObject.name == "GameOver")
        {
            WeaponPickedup.Instance.ClearWeaponHolder();

            GameObject[] Attackers = GameObject.FindGameObjectsWithTag("enemy");
            foreach (GameObject enemy in Attackers)
            {
                Destroy(enemy);
            }

            GameObject[] Healers = GameObject.FindGameObjectsWithTag("healer");
            foreach (GameObject enemy in Healers)
            {
                Destroy(enemy);
            }

            GameObject[] Curser = GameObject.FindGameObjectsWithTag("curser");
            foreach (GameObject enemy in Curser)
            {
                Destroy(enemy);
            }

            this.gameObject.SetActive(false);
        }
        //Shooting.Instance.MagAmount = 15;
        //Shooting.Instance.Ammo = 100;
        SceneManager.LoadScene(SceneName);
        if (GameObject.Find("Player") != null)
        {
            GameObject.Find("Player").gameObject.transform.position = new Vector3(-5f, 7.5f, -15f);
        }
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
