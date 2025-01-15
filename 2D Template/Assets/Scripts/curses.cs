using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class curses : MonoBehaviour
{
    public GameObject curse;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject avatar = GameObject.FindGameObjectWithTag("Player1");
        if (Vector3.Distance(transform.position,avatar.transform.position)<15&&timer<=0) 
        {
            timer = 350;
            GameObject newcurse = Instantiate(curse);
            newcurse.transform.position = transform.position;
            Vector2 direction = avatar.transform.position - newcurse.transform.position;
            newcurse.GetComponent<Rigidbody2D>().AddForce(direction.normalized*500);
            newcurse.GetComponent<curse>().SetSender(gameObject);
        }
        else
        {
            timer -= 1;
        }
    }

}
