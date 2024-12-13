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
    float life=5;
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
        agent.SetDestination(target.position);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            life -= 1;
            Destroy(other.gameObject);
        }
        if (life<=0) 
        {
            Destroy(gameObject);
        }
    }
}