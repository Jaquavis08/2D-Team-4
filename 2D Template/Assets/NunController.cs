using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class NunController : MonoBehaviour
{
    //public Transform pointA;
    //public Transform pointB;
    public float Speed = 5f;
    public float WaitTime = 1f;
    public Transform targetPoint;
    public bool Waiting = false;

    public bool HearingNun;
    public bool SeeingNun;
    public bool SpeakingNun;

    public Animator animator;
    
    public float Range;
    public Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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

            //if (SeeingNun)
            //{
            //    SeeingNunAi();
            //}

            //if (SpeakingNun)
            //{
            //    SpeakingNunAi();
            //}
        }
    }

    void HearingNunAi()
    {

        float distance = Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().gameObject.transform.position);
        if (distance <= Range)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, Speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f);
            {
                StartCoroutine(WaitAndSwitch());
            }
        }



        //float distance = Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().gameObject.transform.position);
        //if (distance <= Range)
        //{
        //    Target = FindObjectOfType<PlayerMovement>().gameObject.transform;
        //    this.gameObject Vector2 = new Vector2(Speed, Target.position.y);
        //}
        //else
        //{
        //    Target = null;
        //}
    }
    private IEnumerator WaitAndSwitch()
    {
        Waiting = true;
        yield return new WaitForSeconds(WaitTime);
        //targetPoint = targetPoint == pointA ? pointB : pointA;
        Waiting = false;
    }
}
