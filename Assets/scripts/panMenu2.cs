using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panMenu2 : MonoBehaviour {
	public Button forwardButton;
	public Animator lvl1;
	public Animator lvl2;

	// Use this for initialization
	void Start () {
		GetComponent<Button> ().interactable = false;
	}

	public void PressBack() {
		lvl1.Play ("panb");
		lvl2.Play ("pan 1b");
		GetComponent<Button> ().interactable = false;
		forwardButton.interactable = true;
	}
}
