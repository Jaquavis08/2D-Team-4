using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class curse : MonoBehaviour
{//"healer" && collision.gameObject.tag != "enemy"
    GameObject sender;
    public GameObject GetSender()
    {
        return sender;
    }
    public void SetSender(GameObject newsender) 
    {
        sender = newsender;
    }
}
