using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//public functions which are attached to buttons to load different scenes
//ensures button sound effects are played and background music is destroyed/not destroyed when appropriate
public class loadMain : MonoBehaviour {
	public AudioSource music;

	private AudioSource click;
	private GameObject loadedMusic;

	private string[] allLevels;

	void Awake() {
		click = GameObject.Find ("click").GetComponent<AudioSource>();
		allLevels = new string[] { "level 1", "level 2", "level 3", "level 4", "level 5" };
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
		click.Play ();
	}

	public void loadFirstlvl () {
		destroyMusic ();
		SceneManager.LoadScene ("level 1");
		click.Play ();
	}

	public void loadSecondlvl() {
		destroyMusic ();
		SceneManager.LoadScene ("level 2");
		click.Play ();
	}

	public void loadThirdlvl () {
		destroyMusic ();
		SceneManager.LoadScene ("level 3");
		click.Play ();
	}

	public void loadFourthlvl() {
		destroyMusic ();
		SceneManager.LoadScene ("level 4");
		click.Play ();
	}

	public void loadFifthlvl() {
		destroyMusic ();
		SceneManager.LoadScene ("level 5");
		click.Play ();
	}

	public void loadChooselvl() {
		if (musicManager.Instance.isPlaying) {
			DontDestroyOnLoad (musicManager.Instance.music);
		} else {
			AudioSource newMusic = Instantiate (music);
			newMusic.Play ();
			musicManager.Instance.isPlaying = true;
			musicManager.Instance.music = newMusic.gameObject;
			DontDestroyOnLoad (musicManager.Instance.music);
		}

		Time.timeScale = 1.0f;
		SceneManager.LoadScene ("levelSelect");
		click.Play ();
	}

	public void loadMainMenu() {
		Time.timeScale = 1.0f;
		SceneManager.LoadScene ("title");
		click.enabled = true;
		click.Play ();
	}

	public void loadCredits() {
		if (musicManager.Instance.isPlaying) {
			DontDestroyOnLoad (musicManager.Instance.music);
		} else {
			AudioSource newMusic = Instantiate (music);
			newMusic.Play ();
			musicManager.Instance.isPlaying = true;
			musicManager.Instance.music = newMusic.gameObject;
			DontDestroyOnLoad (musicManager.Instance.music);
		}

		SceneManager.LoadScene ("credits");
		click.enabled = true;
		click.Play ();
	}

	void destroyMusic() {
		Destroy (musicManager.Instance.music);
		musicManager.Instance.isPlaying = false;
	}
}
