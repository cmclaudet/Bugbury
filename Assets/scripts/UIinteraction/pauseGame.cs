using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//brings up pause menu when player presses pause button
public class pauseGame : MonoBehaviour {
	public Transform pauseMenu;
	public Transform canvas;
	public AudioSource click;

	private Transform menu;

	void Awake() {
		GetComponent<Button> ().interactable = false;
	}

	public void pause() {
		click.Play ();
		lifeManager.Instance.control = false;
		Time.timeScale = 0;
		menu = Instantiate (pauseMenu);
		menu.SetParent (canvas, false);
		menu.GetComponent<unPause> ().click = click;
		menu.GetComponent<unPause> ().pauseButton = gameObject.GetComponent<Button> ();

		GetComponent<Button> ().interactable = false;
	}
}
