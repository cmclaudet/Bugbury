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
			arcade.interactable = false;
			endless.interactable = true;
		} else {
			arcade.interactable = true;
			endless.interactable = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
