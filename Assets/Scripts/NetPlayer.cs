using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetPlayer : NetworkBehaviour {

    
    public SyncList<int> players;
    [SyncVar]
    int playerNumber = -1;


    public int assignPlayer() {
        if (!isServer) {
            return -1;
        }
        playerNumber++;
        //players.Add(playerNumber);
        return playerNumber;
    }



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
