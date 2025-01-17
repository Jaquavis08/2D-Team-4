using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGunSpawn : MonoBehaviour
{
    public Transform[] GunSpawns;
    public GameObject GunPrefab;
    public GameObject FlashlightPrefab;
    public GameObject Global;

    private GameObject Gun;
    private GameObject Flashlight;

    void Start()
    {
        if (GunSpawns.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

    }

    public void Update()
    {
        GameObject gunInScene = GameObject.Find("Gun");
        if (!gunInScene)
        {
            Gun = Instantiate(GunPrefab, transform.position, Quaternion.identity);
            int randomIndex = Random.Range(0, GunSpawns.Length);
            Gun.transform.position = GunSpawns[randomIndex].position;
            Gun.name = GunPrefab.name;
        }

        GameObject FlashlightInScene = GameObject.Find("Flashlight");
        if (!FlashlightInScene)
        {
            Flashlight = Instantiate(FlashlightPrefab, transform.position, Quaternion.identity);
            Flashlight.transform.position = new Vector3(-3.1f, 6.5f, 0f);
            Flashlight.name = FlashlightPrefab.name;
        }

        GameObject GlobalInScene = GameObject.Find("Global");
        if (GlobalInScene)
        {
            Global.SetActive(false);
        }
    }
}