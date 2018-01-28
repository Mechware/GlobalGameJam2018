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
    public int PlayerNumber;
    public GameObject ObeliskPrefab;
    public FlyingInformation FlyingInfo = new FlyingInformation();
    public UnityEvent OnObeliskSwitchShot, OnObeliskSwitchedTo;

    private List<Obelisk> ownedObelisks = new List<Obelisk>();

    private GameObject flyingTowards;

    public override void OnStartLocalPlayer() {
        GetComponent<Obelisk>().Occupied = true;
        PlayerNumber = GameObject.Find("NetworkPlayerWatchNugget").GetComponent<NetPlayer>().assignPlayer();
        transform.Find("PlayerID").gameObject.GetComponentInChildren<Text>().text = PlayerNumber.ToString();

    }

  /*  void Start()
    {
        GetComponent<Obelisk>().Occupied = true;
    }*/
    
	// Update is called once per frame
	void Update () {


        string player = PlayerNumber == 99 ? "" : PlayerNumber.ToString();

        if (FlyingInfo.FlyingTowardSomething)
        {
            Vector3 newPos;
            bool isDone = FlyingInfo.GetNextPosition(out newPos);


            if (isDone)
            {
                OnObeliskSwitchedTo.Invoke();
                Destroy(flyingTowards);
            }
            
            transform.position = newPos;
        }
	    else if (Input.GetAxis("SwitchObelisk" + player) != 0)
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

        Debug.DrawRay(start, dir);

        if(!hit.Equals(default(RaycastHit)))
        {
            Obelisk ob = hit.transform.GetComponent<Obelisk>();
            if(ob != null)
            {
                if (ob.PlayerOwner != PlayerNumber && ob.PlayerOwner != Obelisk.NO_OWNER) {
                    if (ob.Occupied) {
                        ob.GetComponent<ObeliskSwitcher>().DieAndSwitch();
                        CmdDestroyObelisk(ob.gameObject);
                    } else {
                        //Play error sound???
                    }
                } else {
                    //The object is yours or nobodies, therefore you can transmitt your STDs to it
                    OnObeliskSwitchShot.Invoke();
                    flyingTowards = hit.transform.gameObject;
                    CmdSpawnObelisk();

                    FlyingInfo.Init(ob.transform.position, transform.position);
                    CmdDestroyObelisk(ob.gameObject);

                }

            }
        }
    }

    [Command]
    void CmdDestroyObelisk(GameObject ob) {
        Destroy(ob);
    }

    [Command]
    void CmdSpawnObelisk() {
        var obelisk = Instantiate(ObeliskPrefab, transform.position, transform.rotation);
        NetworkServer.Spawn(obelisk);
        obelisk.GetComponent<Obelisk>().SetPlayerOwnership(PlayerNumber);
        ownedObelisks.Add(obelisk.GetComponent<Obelisk>());

    }


    private void DieAndSwitch()
    {
        // new pos
        Obelisk closestObelisk = null;
        float minDistance = float.MaxValue;
        foreach(Obelisk ob in ownedObelisks)
        {
            if (ob == null) continue;
            if(Vector3.Distance(transform.position, ob.transform.position) < minDistance)
            {
                closestObelisk = ob;
            }
        }

        if(closestObelisk == null)
        {
            print("PLAYER " + (PlayerNumber + 1) + "DIEEED");
            return;
        }
        flyingTowards = closestObelisk.gameObject;
        FlyingInfo.Init(closestObelisk.transform.position, transform.position);
    }
}
