using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//brings up pause menu when player presses pause button
public class pauseGame : MonoBehaviour {
	public Transform pauseMenu;
	public Transform canvas;

	private Transform menu;
	private AudioSource click;

	void Awake() {
		click = GameObject.Find ("click").GetComponent<AudioSource> ();
	}

	public void setupPause() {
		click.Play ();
		GameObject manager = GameObject.Find ("game manager");
		manager.GetComponent<caterpillarManager> ().control = false;
		Time.timeScale = 0;
		menu = Instantiate (pauseMenu);
		menu.SetParent (canvas, false);
	}
}
