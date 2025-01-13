using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class enemy : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    float life=10;
    GameObject[] healers;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag=="enemy")
        {


            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.Warp(transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {

        target = GameObject.FindWithTag("Player1").transform;
        healers = GameObject.FindGameObjectsWithTag("healer");
        if (gameObject.tag=="enemy")
        {
            agent.destination=target.position;
            foreach (GameObject healer in healers)
            {
                if (Vector3.Distance(healer.transform.position, transform.position) < 10)
                {

                    if (life <= 10)
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
            Destroy(gameObject);
        }
    }

}