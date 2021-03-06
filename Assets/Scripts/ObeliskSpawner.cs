﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(BoxCollider))]
public class ObeliskSpawner : NetworkBehaviour {

    //public GameObject Player1, Player2;
    public GameObject Spawn1, Spawn2;
    public GameObject Obelisk;
    public int NumberOfObelisks;

    private BoxCollider SpawnArea;

    public bool Online = true;

	// Use this for initialization
	void Start () {

        if(!Online)
        {
            SpawnArea = GetComponent<BoxCollider>();

            Spawn1.transform.position = SpawnArea.bounds.min;
            Spawn2.transform.position = SpawnArea.bounds.max;

            RandomlySpawnObelisks(NumberOfObelisks);
            RandomSpawner();
            //SpawnPlayers();
        }
	}

    public override void OnStartServer() {
        if(Online)
        {
            SpawnArea = GetComponent<BoxCollider>();

            Spawn1.transform.position = SpawnArea.bounds.min;
            Spawn2.transform.position = SpawnArea.bounds.max;

            RandomlySpawnObelisks(NumberOfObelisks);
            RandomSpawner();
        }
    }

    void RandomlySpawnObelisks(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            var empty  = Instantiate(Obelisk, GetRandomSpotInSpawnArea(), Quaternion.identity);
            NetworkServer.Spawn(empty);
        }
    }

    Vector3 GetRandomSpotInSpawnArea()
    {
        Vector3 randomPoint = new Vector3();
        randomPoint.x = Random.Range(SpawnArea.bounds.min.x, SpawnArea.bounds.max.x);
        randomPoint.y = Random.Range(SpawnArea.bounds.min.x, SpawnArea.bounds.max.y);
        randomPoint.z = Random.Range(SpawnArea.bounds.min.x, SpawnArea.bounds.max.z);
        return randomPoint;
    }

	// Update is called once per frame
	void Update () {
		
	}

    void RandomSpawner() {
        float num;
        RandomlySpawnObelisks(1);
        num = Random.Range(15, 40);
        StartCoroutine(WaitToSpawn(num));
    }

    IEnumerator WaitToSpawn(float seconds) {
         yield return new WaitForSeconds(seconds);
         RandomSpawner();
    }
}
