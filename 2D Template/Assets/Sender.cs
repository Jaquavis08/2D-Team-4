using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UI.Image;

public class Sender : MonoBehaviour
{
    public bool SceneTp = true;
    public string SceneName;
    public bool PositionTp = false;
    public Vector3 Position;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            DontDestroyOnLoad(collision.gameObject);
            if (SceneTp == true && PositionTp == true)
            {
                Instantiate(gameObject, transform.position, Quaternion.identity);
                gameObject.transform.SetParent(collision.gameObject.transform, false);
                SceneManager.LoadScene(SceneName);
                collision.gameObject.transform.position = Position;
                Destroy(gameObject);
                return;
            }
            if (PositionTp == true && SceneTp == false)
            {
                Position = collision.transform.position;
                return;
            }
            if (PositionTp == false && SceneTp == true)
            {
                SceneManager.LoadScene(SceneName);
                return;
            }
            if (PositionTp == false && SceneTp == false)
            {
                return;
            }
            
        }
    }
}
