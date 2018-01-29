using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Utility;
using UnityEngine.Networking;
using UnityEngine.UI;


public class ObeliskSwitcher : NetworkBehaviour
{
    [Serializable]
    public class FlyingInformation
    {
        public float FlyingSpeed;

        [HideInInspector] public bool FlyingTowardSomething = false;
        [HideInInspector] public Vector3 StartPos, EndPos;
        [HideInInspector] public float PercentToEnd = 0;
        [HideInInspector] public GameObject NewObelisk;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPos"></param>
        /// <returns>Whether or not the destination has been reached</returns>
        public bool GetNextPosition(out Vector3 newPos)
        {
            PercentToEnd = PercentToEnd + (FlyingSpeed / Vector3.Distance(StartPos, EndPos)) * Time.deltaTime;

            if (PercentToEnd >= 1)
            {
                newPos = EndPos;
                FlyingTowardSomething = false;
                return true;
            }

            newPos = Vector3.Lerp(StartPos, EndPos, PercentToEnd);
            return false;
        }

        public void Init(Vector3 newPos, Vector3 oldPos)
        {
            FlyingTowardSomething = true;
            StartPos = oldPos;
            EndPos = newPos;
            PercentToEnd = 0;
        }
    }


    public float MaxDestroyDistance = 5;

    public GameObject ObeliskPrefab;
    public FlyingInformation FlyingInfo = new FlyingInformation();
    public UnityEvent OnObeliskSwitchShot, OnObeliskSwitchedTo;

    private List<Obelisk> ownedObelisks = new List<Obelisk>();

    private GameObject flyingTowards;
    private Obelisk thisObelisk;

    public void Start()
    {
        thisObelisk = GetComponent<Obelisk>();
    }

    void Update()
    {
        if (FlyingInfo.FlyingTowardSomething)
        {
            Vector3 newPos;
            bool isDone = FlyingInfo.GetNextPosition(out newPos);

            if (isDone)
            {
                OnObeliskSwitchedTo.Invoke();
                CmdDestroyObelisk(flyingTowards);
            }

            transform.position = newPos;
        }
        else if (Input.GetAxis("SwitchObelisk") != 0)
        {
            CheckSwitchObelisk();
        }
    }



    private void CheckSwitchObelisk()
    {
        RaycastHit hit;

        Vector3 start = transform.position;
        Vector3 dir = GetComponentInChildren<Camera>().transform.forward;

        Physics.Raycast(start, dir, out hit);

        if (!hit.Equals(default(RaycastHit)))
        {
            Obelisk ob = hit.transform.GetComponent<Obelisk>();
            if (ob != null)
            {
                if ((ob.PlayerOwner == thisObelisk.PlayerOwner || ob.PlayerOwner == Obelisk.NO_OWNER) && !ob.Occupied)
                {
                    OnObeliskSwitchShot.Invoke();
                    flyingTowards = hit.transform.gameObject;
                    CmdSpawnObelisk();

                    FlyingInfo.Init(ob.transform.position, transform.position);
                }
                else if (ob.Occupied)
                {
                    CmdTellShitToRespawn(ob.gameObject);
                }
            }
        }
    }

    [Command]
    void CmdDestroyObelisk(GameObject ob)
    {
        Destroy(ob);
    }

    [Command]
    void CmdSpawnObelisk()
    {
        var obelisk = Instantiate(ObeliskPrefab, transform.position, transform.rotation);
        obelisk.GetComponent<Obelisk>().PlayerOwner = thisObelisk.PlayerOwner;
        ownedObelisks.Add(obelisk.GetComponent<Obelisk>());
        NetworkServer.Spawn(obelisk);
    }

    [Command]
    private void CmdTellShitToRespawn(GameObject other)
    {
        other.GetComponent<ObeliskSwitcher>().RpcDieAndSwitch();
    }

    [ClientRpc]
    private void RpcDieAndSwitch()
    {
        if (!isLocalPlayer) return;

        // new pos
        Obelisk closestObelisk = null;
        float minDistance = float.MaxValue;
        foreach (Obelisk ob in ownedObelisks)
        {
            if (ob == null) continue;
            if (Vector3.Distance(transform.position, ob.transform.position) < minDistance)
            {
                closestObelisk = ob;
            }
        }

        if (closestObelisk == null)
        {
            print("PLAYER " + (thisObelisk.PlayerOwner + 1) + "DIEEED");
            return;
        }
        flyingTowards = closestObelisk.gameObject;
        FlyingInfo.Init(closestObelisk.transform.position, transform.position);
    }
}
