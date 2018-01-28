using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineNetwork : MonoBehaviour, INetworkChecker
{
    bool INetworkChecker.IsLocalPlayer()
    {
        return true;
    }
}