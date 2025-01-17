using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generation : MonoBehaviour
{

    public healernungeneration healernuns;

    int round=-1;
    void Update()
    {
        if (FindObjectsOfType<enemy>().Length==0)
        {
            round++;
            healernuns.RefreshHealers();
            foreach (enemygeneration generator in FindObjectsOfType<enemygeneration>())
            {
                generator.Generate(generator.hs, generator.vs, generator.rounds[round]);
            }
        }
    }
    public int GetRound()
    {
        return round;
    }
}
