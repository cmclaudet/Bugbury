using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unPause : MonoBehaviour {
	private AudioSource click;
	private Button pauseButton;

	void Awake() {
		click = GameObject.Find ("click").GetComponent<AudioSource> ();
		pauseButton = GameObject.Find ("pauseButton").GetComponent<Button> ();
	}

	public void unpause() {
		pauseButton.interactable = true;
		click.Play ();
		Time.timeScale = 1.0f;
		gameObject.SetActive (false);
		GameObject manager = GameObject.Find ("game manager");
		manager.GetComponent<caterpillarManager> ().control = true;
	}
}
