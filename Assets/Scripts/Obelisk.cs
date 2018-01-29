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
    public bool Occupied = false;


    public GameObject red, blue;
    public Text playNum;

    public Color[] colors = new Color[]
    {
        Color.red,
        Color.blue,
    };


    public void Awake()
    {
        if(GetComponent<ObeliskSwitcher>() != null)
        {
            Occupied = true;
            PlayerOwner = GameObject.Find("NetworkPlayerWatchNugget").GetComponent<NetPlayer>().assignPlayer();
        }
        changeNumber(PlayerOwner);
    }

    void Update()
    {
        changeNumber(PlayerOwner);
    }

    public void changeNumber(int owner) {

        if (red != null && owner != NO_OWNER) {
            if(owner % 2 == 0) {
                red.SetActive(true);
            } else {
                blue.SetActive(true);
            }
        }
    }

}
