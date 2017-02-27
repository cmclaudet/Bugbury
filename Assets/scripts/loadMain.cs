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

	private string[] allLevels;

	void Awake() {
		click = GameObject.Find ("click").GetComponent<AudioSource>();
		allLevels = new string[] { "level 1", "level 2", "level 3", "level 4" };
	}

	public void replay() {
		Scene currentScene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (currentScene.name);
		click.Play ();
	}

	public void nextLevel() {
		for (int i = 0; i < allLevels.Length; i++ ) {
			if (SceneManager.GetActiveScene ().name == allLevels [i]) {
				SceneManager.LoadScene (allLevels [i + 1]);
			}
		}

/*
		if (SceneManager.GetActiveScene ().name == "level 1") {
			SceneManager.LoadScene ("level 2");
		} else {
			SceneManager.LoadScene ("level 3");
		}*/
		click.Play ();
	}

	public void loadFirstlvl () {
		resetMusic ();
		SceneManager.LoadScene ("level 1");
		click.Play ();
	}

	public void loadSecondlvl() {
		resetMusic ();
		SceneManager.LoadScene ("level 2");
		click.Play ();
	}

	public void loadThirdlvl () {
		resetMusic ();
		SceneManager.LoadScene ("level 3");
		click.Play ();
	}

	public void loadFourthlvl() {
		resetMusic ();
		SceneManager.LoadScene ("level 4");
	}

	public void loadChooselvl() {
		DontDestroyOnLoad (backgroundmusic.transform.gameObject);
		backgroundmusic.transform.gameObject.tag = "Untagged";
		Time.timeScale = 1.0f;
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

	public void backToMenuFromSelect() {
		SceneManager.LoadScene ("title");
		GameObject initialMusic = GameObject.FindGameObjectWithTag ("initialMusic");
		Destroy (initialMusic);
		click.Play ();
	}

	void resetMusic() {
		loadedMusic = GameObject.Find ("music");
		if (loadedMusic != null) {
			Destroy (loadedMusic);
		}
	}
}
