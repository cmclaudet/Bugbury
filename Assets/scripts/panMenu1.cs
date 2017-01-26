using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panMenu1 : MonoBehaviour {
	public Button backButton;
	public Animator lvl1;
	public Animator lvl2;

	// Use this for initialization
	void Start () {
		lvl1.enabled = false;
		lvl2.enabled = false;
	}

	public void Press() {
		lvl1.enabled = true;
		lvl2.enabled = true;
		lvl1.Play ("pan");
		lvl2.Play ("pan 1");
		GetComponent<Button> ().interactable = false;
		backButton.interactable = true;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
