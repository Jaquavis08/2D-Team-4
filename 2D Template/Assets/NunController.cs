using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UIElements.Experimental;
using Unity.VisualScripting;

public class NunController : MonoBehaviour
{
    //public Transform pointA;
    //public Transform pointB;
    public float Speed = 5f;
    public float Health = 100f;
    public float MaxHealth = 100f;
    public bool Hited;

    public float WaitTime = 1f;
    public Transform targetPoint;
    public bool Waiting = false;

    public bool HearingNun;
    public bool SeeingNun;
    public bool SpeakingNun;

    public Animator animator;
    bool Healing;
    
    public float Range;
    public Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        animator = gameObject.GetComponent<Animator>();
        targetPoint = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Waiting)
        {
            if (HearingNun)
            {
                HearingNunAi();
            }

            if (SeeingNun)
            {
                SeeingNunAi();
            }

            if (SpeakingNun)
            {
                SpeakingNunAi();
            }
        }
    }

    //transform.position = Vector3.MoveTowards(this.transform.position, targetPoint.position, Speed* Time.deltaTime);

    void HearingNunAi()
    {
        float distance = Vector3.Distance(transform.position, FindObjectOfType<NunController>().gameObject.transform.position);
        if (distance <= Range)
        {
            targetPoint = FindObjectOfType<PlayerMovement>().gameObject.transform;
            //transform.position = Vector3.MoveTowards(this.transform.position, targetPoint.position, Speed * Time.deltaTime);

            animator.SetBool("Healing", true);

        }
    }

    void SeeingNunAi()
    {
        float distance = Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().gameObject.transform.position);
        if (distance <= Range)
        {
            targetPoint = FindObjectOfType<PlayerMovement>().gameObject.transform;
            transform.position = Vector3.MoveTowards(this.transform.position, targetPoint.position, Speed * Time.deltaTime);



            //if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f);
            //{
            //    StartCoroutine(WaitAndSwitch());
            //}
        }
    }

    void SpeakingNunAi()
    {
        float distance = Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().gameObject.transform.position);
        if (distance <= Range)
        {
            targetPoint = FindObjectOfType<PlayerMovement>().gameObject.transform;
            transform.position = Vector3.MoveTowards(this.transform.position, targetPoint.position, Speed * Time.deltaTime);

            if (Hited == false)
            {
                animator.SetBool("Healing", true);
            }




        }
       

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Hited = true;
            animator.SetBool("Healing", false);
            this.Health -= 5;
            Destroy(collision.gameObject);
            animator.SetBool("Hit", true);
            StartCoroutine(HearingNun());

        }

        IEnumerator HearingNun()
        {
            yield return new WaitForSeconds(1);
                 animator.SetBool("Hit", false);
            Hited = false;
        }

        if (collision.gameObject.CompareTag("Player1"))
        {
            healthbar.Instance.Hurt(5);
            //Destroy(collision.gameObject);
        }
    }

    private IEnumerator WaitAndSwitch()
    {
        Waiting = true;
        yield return new WaitForSeconds(WaitTime);
        //targetPoint = targetPoint == pointA ? pointB : pointA;
        Waiting = false;
    }
}
