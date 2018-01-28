using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(RigidbodyFirstPersonController))]
[RequireComponent(typeof(Obelisk))]
[RequireComponent(typeof(ObeliskSwitcher))]
public class Player : MonoBehaviour {

    public static List<GameObject> NumberOfPlayers = new List<GameObject>();
    public int PlayerNumber;

    private void Awake()
    {

        NumberOfPlayers.Add(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        GetComponent<RigidbodyFirstPersonController>().PlayerNumber = PlayerNumber;
        GetComponent<Obelisk>().PlayerOwner = PlayerNumber;
        GetComponent<ObeliskSwitcher>().PlayerNumber = PlayerNumber;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
