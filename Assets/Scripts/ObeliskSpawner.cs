using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ObeliskSpawner : MonoBehaviour {

    public GameObject Player1, Player2;
    public GameObject Obelisk;
    public int NumberOfObelisks;

    private BoxCollider SpawnArea;


	// Use this for initialization
	void Start () {
        SpawnArea = GetComponent<BoxCollider>();

        RandomlySpawnObelisks(NumberOfObelisks);
        SpawnPlayers();
	}
	
    void SpawnPlayers()
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
    }

    void RandomlySpawnObelisks(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Instantiate(Obelisk, GetRandomSpotInSpawnArea(), Quaternion.identity);
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
