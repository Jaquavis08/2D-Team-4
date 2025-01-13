using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="enemy"|| collision.gameObject.tag == "healer" || collision.gameObject.tag == "curser")
        {

            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag!="Player1"&& collision.gameObject.tag !="Weapon")
        {
            Destroy(this.gameObject);

        }
    }

}
