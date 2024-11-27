using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    private Rigidbody2D rb;
    public float roatateSpeed = 0.0025f;
    private int EnemyChance;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!target)
        {
            GetTarget();
        }
        else
        {
            RotateTowardsTarget();
        }




    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, roatateSpeed);
    }

    private void GetTarget()
    {
        EnemyChance = Random.Range(1, 3);

        if (EnemyChance == 1 && GameObject.FindGameObjectWithTag("Player1"))
        {
            target = GameObject.FindGameObjectWithTag("Player1").transform;
        }


        else
        {
            target = GameObject.FindGameObjectWithTag("Player1").transform;
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            Destroy(other.gameObject);
            target = null;
        }


        if (other.gameObject.CompareTag("Bullet"))
        {

            Destroy(other.gameObject);
            Destroy(gameObject);
        }


    }
}
