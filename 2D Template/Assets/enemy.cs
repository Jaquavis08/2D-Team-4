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
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        healers = GameObject.FindGameObjectsWithTag("healer");
        if (gameObject.tag != "healer")
        {
            agent.SetDestination(target.position);
        }
        if (gameObject.tag=="enemy")
        {

            foreach (GameObject healer in healers)
            {
                if (Vector3.Distance(healer.transform.position, transform.position) < 20)
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