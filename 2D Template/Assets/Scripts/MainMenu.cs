using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject objectToParent; // The object to parent
    private GameObject currentInstance; // Holds the instance of the objectToParent

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Instantiate and parent the object to the button
        if (objectToParent != null && currentInstance == null)
        {
            currentInstance = Instantiate(objectToParent, transform);
            currentInstance.transform.localPosition = Vector3.zero; // Align to the button's position
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Destroy the parented object on mouse exit
        if (currentInstance != null)
        {
            Destroy(currentInstance);
        }
    }

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
