using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinning : MonoBehaviour {
    public int OrbitSpeed;
    public GameObject middle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(middle.transform.position, Vector3.up, OrbitSpeed * Time.deltaTime);
	}
}
