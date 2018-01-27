using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class ObeliskSwitcher : MonoBehaviour
{

    public int PlayerNumber;
    
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

    
    public GameObject ObeliskPrefab;

    public FlyingInformation FlyingInfo = new FlyingInformation();

    private GameObject flyingTowards;
    
    
	// Update is called once per frame
	void Update () {

        

        if (FlyingInfo.FlyingTowardSomething)
        {
            Vector3 newPos;
            bool isDone = FlyingInfo.GetNextPosition(out newPos);
            
            if (isDone)
            {
                Destroy(flyingTowards);
            }
            
            transform.position = newPos;
        }
	    else if (Input.GetAxis("SwitchObelisk") != 0)
        {
            SwitchToObelisk();
        }
	}

    private void SwitchToObelisk()
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
                flyingTowards = hit.transform.gameObject;
                Instantiate(ObeliskPrefab, transform.position, transform.rotation);
                FlyingInfo.Init(ob.transform.position, transform.position);
            }
        }
    }
}
