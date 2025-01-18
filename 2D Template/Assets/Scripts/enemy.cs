using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class enemy : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    private float life;
    public float maxLife;
    GameObject[] healers;
    public GameObject AmmoPrefab;
    SpriteRenderer sr;

    private bool isHealing = false;
    // Start is called before the first frame update
    void Start()
    {
        sr= GetComponent<SpriteRenderer>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.Warp(transform.position);
        
    }

    private void Awake()
    {
            DontDestroyOnLoad(gameObject);
        life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("Player").transform;
        healers = GameObject.FindGameObjectsWithTag("healer");
        if (gameObject.tag != "healer")
        {
            agent.SetDestination(target.position);
        }
        if (gameObject.tag=="enemy")
        {

            foreach (GameObject healer in healers)
            {
                if (Vector3.Distance(healer.transform.position, transform.position) < 10)
                {

                    if (!isHealing && life < maxLife)
                    {
                        StartCoroutine(LifeUp());
                    }
                }
            }
        }
        Debug.Log(life);
    }

    IEnumerator LifeUp()
    {
        isHealing = true; // Prevent overlapping healing
        yield return new WaitForSeconds(0.35f); // Heal every second
        life += 1;
        life = Mathf.Clamp(life, 0, maxLife); // Ensure life doesn't exceed maxLife
        isHealing = false;
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            int randomV = Random.Range(1, 3);
            life -= randomV;
            transform.GetChild(0).GetComponent<hitresponse>().hit();
        }
        if (life<=0) 
        {
            int randomValue = Random.Range(1, 5);
            if (randomValue >= 3)
            {
                GameObject Ammo = Instantiate(AmmoPrefab, transform.position, Quaternion.identity);
                Ammo.name = AmmoPrefab.name;
                Ammo.transform.position = gameObject.transform.position;
            }
            print(randomValue);
            Destroy(gameObject);
        }
    }

}