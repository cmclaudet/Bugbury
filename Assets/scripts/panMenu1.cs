using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//triggers panning animation when player presses forward button to focus on next level
public class panMenu1 : MonoBehaviour {
	public Button backButton;
	public Animator lvl1;
	public Animator lvl2;
	public Animator lvl3;
	public AudioSource woosh;

	public bool onLevel2{ get; set; }

	// Use this for initialization
	void Start () {
		backButton.interactable = false;
		lvl1.speed = 3.0f;
		lvl2.speed = 3.0f;
		lvl3.speed = 3.0f;

		//set to false to ensure default animation is only played when player presses the button, not on scene load
		lvl1.enabled = false;
		lvl2.enabled = false;

		onLevel2 = false;
	}

	public void Press() {
		if (onLevel2) {
			From2To3 ();
		} else {
			From1To2 ();
		}
	}

	void From1To2() {
		woosh.Play ();
		lvl1.enabled = true;
		lvl2.enabled = true;
		lvl1.Play ("panCenterLeft");
		lvl2.Play ("panRightCenter");

		backButton.interactable = true;
		onLevel2 = true;
	}

	void From2To3() {
		lvl3.gameObject.SetActive (true);
		lvl1.gameObject.SetActive (false);

		woosh.Play ();
		lvl2.Play ("panCenterLeft");
		lvl3.Play ("panRightCenter");

		GetComponent<Button> ().interactable = false;
		onLevel2 = false;
	}
}
