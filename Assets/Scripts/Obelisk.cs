using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obelisk : MonoBehaviour
{
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
        GetComponent<MeshRenderer>().material.color = colors[playerNum];
    }

    public void Kill()
    {
        if(Occupied)
        {
            // transfer player to closest obelisk
            //GetComponent<MeshRenderer>().material.color.a = 0.5f;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
