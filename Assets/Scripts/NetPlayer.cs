using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetPlayer : NetworkBehaviour {

    
    public List<int> players = new List<int>();
    [SyncVar]
    int playerNumber = -1;


    public int assignPlayer() {

        playerNumber++;
        players.Add(playerNumber);
        return playerNumber;
    }



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
