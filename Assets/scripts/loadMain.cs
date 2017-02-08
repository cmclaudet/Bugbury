using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//public functions which are attached to buttons to load different scenes
//ensures button sound effects are played and background music is destroyed/not destroyed when appropriate
public class loadMain : MonoBehaviour {
	public AudioSource backgroundmusic;

	private AudioSource click;
	private GameObject loadedMusic;

	void Awake() {
		click = GameObject.Find ("click").GetComponent<AudioSource>();
	}

	public void loadMainGame () {
		resetMusic ();
		SceneManager.LoadScene ("level 1");
		click.Play ();
	}

	public void loadSecondlvl () {
		resetMusic ();
		SceneManager.LoadScene ("level 2");
		click.Play ();
	}

	public void loadChooselvl() {
		DontDestroyOnLoad (backgroundmusic.transform.gameObject);
		SceneManager.LoadScene ("levelSelect");
		click.Play ();
	}

	public void loadMainMenu() {
		resetMusic ();
		Time.timeScale = 1.0f;
		SceneManager.LoadScene ("title");
		click.enabled = true;
		click.Play ();
	}

	void resetMusic() {
		loadedMusic = GameObject.Find ("music");
		if (loadedMusic != null) {
			Destroy (loadedMusic);
		}
	}
}
