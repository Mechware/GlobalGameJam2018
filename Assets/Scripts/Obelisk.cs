using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obelisk : MonoBehaviour
{
    //make network class
    //create SyncVar for player owner
    // hook = when player owner changes, change color 

	public const int NO_OWNER = -1;
	public int PlayerOwner = NO_OWNER;
	public bool Occupied = false;

    public Color[] colors = new Color[]
    {
        Color.red,
        Color.blue,
    };

    public void SetPlayerOwnership(int playerNum)
    {
        PlayerOwner = playerNum;
        //GetComponent<MeshRenderer>().material.color = colors[playerNum];
    }

}
