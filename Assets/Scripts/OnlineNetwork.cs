using UnityEngine;
using UnityEngine.Networking;

public class OnlineNetwork : NetworkBehaviour, INetworkChecker
{
    public bool IsLocalPlayer()
    {
        return isLocalPlayer;
    }
}

