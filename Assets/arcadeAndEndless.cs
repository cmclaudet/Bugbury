using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class arcadeAndEndless : MonoBehaviour {
	public Button arcade;
	public Button endless;
	// Use this for initialization
	void Start () {
		string activeScene = SceneManager.GetActiveScene ().name;

		if (activeScene == "levelSelect") {
			arcade.enabled = false;
			endless.enabled = true;
		} else {
			arcade.enabled = true;
			endless.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
