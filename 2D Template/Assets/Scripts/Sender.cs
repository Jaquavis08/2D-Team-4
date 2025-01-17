using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UI.Image;
using TMPro;

public class Sender : MonoBehaviour
{
    public bool SceneTp = true;
    public string SceneName;
    public bool PositionTp = false;
    public Vector3 Position;
    private GameObject Player;
    public bool DestroyAndRebuild;
    public bool Require;
    public GameObject RequireFromPlayer_1;
    public GameObject RequireFromPlayer_2;  // Second required item
    [SerializeField] private GameObject playerPrefab;
    public TMP_Text RequireText;

    private void Start()
    {
        Player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    public void Update()
    {
        if (RequireText == null)
        {
            GameObject RequireText1 = GameObject.Find("Player").transform.Find("Canvas").transform.Find("RequireText").gameObject;
            RequireText = RequireText1.GetComponent<TMP_Text>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            if (SceneTp == true && PositionTp == true)
            {
                // Check if both required items are present in the player's weapon holder
                if (Require == true &&
                    collision.transform.Find("Weaponholder").transform.Find(RequireFromPlayer_1.gameObject.name) != null &&
                    collision.transform.Find("Weaponholder").transform.Find(RequireFromPlayer_2.gameObject.name) != null)
                {
                    RequireText.gameObject.SetActive(false);
                    Instantiate(gameObject, transform.position, Quaternion.identity);
                    gameObject.transform.SetParent(collision.gameObject.transform, false);
                    SceneManager.LoadScene(SceneName);
                    collision.gameObject.transform.position = Position;
                    Destroy(gameObject);
                    return;
                }
                else if (Require == true)
                {
                    // If one or both required items are missing, show a message
                    string missingItem = "";
                    if (collision.transform.Find("Weaponholder").transform.Find(RequireFromPlayer_1.gameObject.name) == null)
                    {
                        missingItem = RequireFromPlayer_1.gameObject.name;
                    }
                    if (collision.transform.Find("Weaponholder").transform.Find(RequireFromPlayer_2.gameObject.name) == null)
                    {
                        if (missingItem != "") missingItem += " and the ";
                        missingItem += RequireFromPlayer_2.gameObject.name;
                    }

                    RequireText.text = "You need the " + missingItem + " to enter, come back when you find it";
                    RequireText.gameObject.SetActive(true);
                }
                else if (Require == false)
                {
                    Instantiate(gameObject, transform.position, Quaternion.identity);
                    gameObject.transform.SetParent(collision.gameObject.transform, false);
                    SceneManager.LoadScene(SceneName);
                    collision.gameObject.transform.position = Position;
                    Destroy(gameObject);
                    return;
                }
            }

            // Handling Position and Scene teleporting
            if (PositionTp == true && SceneTp == false)
            {
                collision.transform.position = Position;
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

        if (DestroyAndRebuild == true && SceneTp == true)
        {
            Destroy(Player);
            Vector3 spawnPosition = new Vector3(0, 0, 0); // Replace with your desired position
            Quaternion spawnRotation = Quaternion.identity; // Replace with your desired rotation
        }

        if (PositionTp == true && SceneTp == false)
        {
            Player.transform.position = Position;
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
