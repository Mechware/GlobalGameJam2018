using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetPlayer : NetworkBehaviour
{
    [SyncVar]
    int playerNumber = -1;

    public int assignPlayer() {
        return ++playerNumber;
    }
}
