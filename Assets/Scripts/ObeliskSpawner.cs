using System.Collections;
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


	// Use this for initialization
	/*void Start () {
        SpawnArea = GetComponent<BoxCollider>();

        Spawn1.transform.position=SpawnArea.bounds.min;
        Spawn2.transform.position=SpawnArea.bounds.max;

       // RandomlySpawnObelisks(NumberOfObelisks);
       // SpawnPlayers();
	}*/

    public override void OnStartServer() {
        SpawnArea = GetComponent<BoxCollider>();

        Spawn1.transform.position=SpawnArea.bounds.min;
        Spawn2.transform.position=SpawnArea.bounds.max;

        RandomlySpawnObelisks(NumberOfObelisks);

    }
	
    /*void SpawnPlayers()
    {
        Player1.transform.position = SpawnArea.bounds.min;
        Player1.transform.LookAt(SpawnArea.bounds.center);
        Quaternion temp = Player1.transform.rotation;
        temp.x = 0;
        temp.z = 0;
        Player1.transform.rotation = temp;

        Player2.transform.position = SpawnArea.bounds.max;
        Player2.transform.LookAt(SpawnArea.bounds.center);
        temp = Player2.transform.rotation;
        temp.x = 0;
        temp.z = 0;
        Player2.transform.rotation = temp;
    }*/

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
}
