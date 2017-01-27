using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseGame : MonoBehaviour {
	public Transform pauseMenu;
	public Transform canvas;

	private Transform menu;

	public void setupPause() {
		GameObject manager = GameObject.Find ("game manager");
		manager.GetComponent<caterpillarManager> ().control = false;
		Time.timeScale = 0;
		menu = Instantiate (pauseMenu);
		menu.SetParent (canvas, false);
	}
}
