using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//triggers panning animation when player presses forward button to focus on next level
public class panMenu1 : MonoBehaviour {
	public Button backButton;
	public Animator lvl1;
	public Animator lvl2;
	public AudioSource woosh;

	// Use this for initialization
	void Start () {
		//set to false to ensure default animation is only played when player presses the button, not on scene load
		lvl1.speed = 2.0f;
		lvl2.speed = 2.0f;
		lvl1.enabled = false;
		lvl2.enabled = false;
	}

	public void Press() {
		woosh.Play ();
		lvl1.enabled = true;
		lvl2.enabled = true;
		lvl1.Play ("pan");
		lvl2.Play ("pan 1");
		GetComponent<Button> ().interactable = false;
		backButton.interactable = true;
	}

}
