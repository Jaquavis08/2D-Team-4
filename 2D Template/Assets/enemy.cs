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
    float life=10;
    GameObject[] healers;
    public GameObject AmmoPrefab;
    SpriteRenderer sr;
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

                    if (life < 10)
                    {

                        life += 2;
                    }
                }
            }
        }
        Debug.Log(life);
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            life -= 1;
        }
        if (life<=0) 
        {
            int randomValue = Random.Range(1, 5);
            if (randomValue == 3)
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