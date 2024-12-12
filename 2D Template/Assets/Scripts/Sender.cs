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
    private GameObject Player;
    public bool DestroyAndRebuild;
    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        Player = FindObjectOfType<PlayerMovement>().gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
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

    public void Tping()
    {
        if (SceneTp == true && PositionTp == true)
        {
            Instantiate(gameObject, transform.position, Quaternion.identity);
            gameObject.transform.SetParent(Player.transform, false);
            SceneManager.LoadScene(SceneName);
            Player.transform.position = Position;
            Destroy(gameObject);
            return;
        }

        if(DestroyAndRebuild == true && SceneTp == true)
        {
            Destroy(Player);
            Vector3 spawnPosition = new Vector3(0, 0, 0); // Replace with your desired position
            Quaternion spawnRotation = Quaternion.identity; // Replace with your desired rotation
        }

        if (PositionTp == true && SceneTp == false)
        {
            Position = Player.transform.position;
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
