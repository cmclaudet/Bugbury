using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//trigger pan animations when player presses back arrow to focus on previous level
public class panMenu2 : MonoBehaviour {
	public Button forwardButton;
	public Animator lvl1;
	public Animator lvl2;
	public AudioSource woosh;

	// Use this for initialization
	void Start () {
		lvl1.speed = 2.0f;
		lvl2.speed = 2.0f;
		GetComponent<Button> ().interactable = false;	//set to false as player can only go back once they have gone forward
	}

	public void PressBack() {
		woosh.Play ();
		lvl1.Play ("panb");		//back panning for level 1 select menu
		lvl2.Play ("pan 1b");	//back panning for level 2 select menu
		GetComponent<Button> ().interactable = false;
		forwardButton.interactable = true;	//player can only go forward once they have gone back
	}
}
