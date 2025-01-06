using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickAScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
