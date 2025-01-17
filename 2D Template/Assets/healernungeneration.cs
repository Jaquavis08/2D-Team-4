using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healernungeneration : MonoBehaviour
{
    public GameObject healernun;
    GameObject[] healerPositions;
    private void Start()
    {
        healerPositions = GameObject.FindGameObjectsWithTag("healerposition");
    }
    public void RefreshHealers()
    {
        foreach (GameObject newhealerpos in healerPositions)
        {
            GameObject newhealer = Instantiate(healernun);
            newhealer.transform.position = newhealerpos.transform.position;
        }
    }
}
