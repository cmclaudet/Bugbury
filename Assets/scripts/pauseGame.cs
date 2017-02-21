using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//brings up pause menu when player presses pause button
public class pauseGame : MonoBehaviour {
	public Transform pauseMenu;
	public Transform canvas;
	public GameObject manager;

	private Transform menu;
	private AudioSource click;

	void Awake() {
		GetComponent<Button> ().interactable = false;
		click = GameObject.Find ("click").GetComponent<AudioSource> ();
	}

	public void setupPause() {
		GetComponent<Button> ().interactable = false;
		click.Play ();
		manager.GetComponent<caterpillarManager> ().control = false;
		Time.timeScale = 0;
		menu = Instantiate (pauseMenu);
		menu.SetParent (canvas, false);
	}
}
