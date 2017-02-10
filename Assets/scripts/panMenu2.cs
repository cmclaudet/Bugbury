using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//trigger pan animations when player presses back arrow to focus on previous level
public class panMenu2 : MonoBehaviour {
	public Button forwardButton;
	public Animator lvl1;
	public Animator lvl2;
	public Animator lvl3;
	public AudioSource woosh;

	private bool onLevel2;

	// Use this for initialization
	void Start () {
		lvl1.speed = 2.0f;
		lvl2.speed = 2.0f;
		lvl3.speed = 2.0f;
		GetComponent<Button> ().interactable = false;	//set to false as player can only go back once they have gone forward

		onLevel2 = false;
	}

	public void PressBack() {
		onLevel2 = forwardButton.GetComponent<panMenu1> ().onLevel2;

		if (onLevel2) {
			From2To1 ();
		} else {
			From3To2 ();
		}
	}

	void From2To1() {
		lvl3.gameObject.SetActive (false);
		lvl1.gameObject.SetActive (true);
		woosh.Play ();
		lvl1.Play ("panLeftCenter");		//back panning for level 1 select menu
		lvl2.Play ("panCenterRight");	//back panning for level 2 select menu

		GetComponent<Button> ().interactable = false;
		forwardButton.GetComponent<panMenu1> ().onLevel2 = false;
	}

	void From3To2() {
		woosh.Play ();
		lvl2.Play ("panLeftCenter");
		lvl3.Play ("panCenterRight");

		forwardButton.interactable = true;
		forwardButton.GetComponent<panMenu1> ().onLevel2 = true;
	}
}
