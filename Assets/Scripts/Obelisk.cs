using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Obelisk : NetworkBehaviour
{
    //make network class
    //create SyncVar for player owner
    // hook = when player owner changes, change color 

	public const int NO_OWNER = -1;
    [SyncVar (hook = "changeNumber")]
	public int PlayerOwner = NO_OWNER;
    public GameObject red, blue;
	public bool Occupied = false;
    public Text playNum;

    public Color[] colors = new Color[]
    {
        Color.red,
        Color.blue,
    };

    public override void OnStartServer() {
        if(playNum!=null)
        playNum.text = PlayerOwner.ToString();
    }



    public void SetPlayerOwnership(int playerNum)
    {
        PlayerOwner = playerNum;

        //GetComponent<MeshRenderer>().material.color = colors[playerNum];
    }

    public void changeNumber(int owner) {
        if (red != null) {
            playNum.text = owner.ToString();

            if(owner == 0) {
                red.SetActive(true);
            } else {
                blue.SetActive(true);
            }

        }
    }

}
