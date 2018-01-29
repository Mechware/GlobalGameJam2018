using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ServerDude : MonoBehaviour {

    public GameObject ob;

    public void setOb(GameObject ob) {
        this.ob = ob;
    }

    public void respwnThem() {
        ob.GetComponent<ObeliskSwitcher>().moveThem();
    }



}
