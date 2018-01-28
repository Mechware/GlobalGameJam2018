using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {
    public GameObject Play;
    public GameObject Forest;
    public GameObject Desert;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void chooseMap() {
        Play.SetActive(false);
        Forest.SetActive(true);
        Desert.SetActive(true);
    }
    public void startGameForest() {
        SceneManager.LoadScene("The Actual Scene");
    }
    public void startGameDesert() {
        SceneManager.LoadScene("Maddie's Scene");
    }
}

