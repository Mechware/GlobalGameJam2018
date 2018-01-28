using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringMahLAZERRR : MonoBehaviour {
    public ParticleSystem lazer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      
	}

    public void FireLazer() {
        lazer.Play();
    }
    


}
