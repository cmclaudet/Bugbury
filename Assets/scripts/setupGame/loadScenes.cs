using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//public functions which are attached to buttons to load different scenes
//ensures button sound effects are played and background music is destroyed/not destroyed/created when appropriate
public class loadScenes : MonoBehaviour {
	public AudioSource music;

	private AudioSource click;

	private highScoreManager.levelArcade[] allLevels;
	private highScoreManager.levelEndless[] allLevelsEndless;

	private bool endless;

	void Awake() {
		click = GameObject.Find ("click").GetComponent<AudioSource>();
		allLevels = highScoreManager.Instance.arcadeLevels;
		allLevelsEndless = highScoreManager.Instance.endlessLevels;
	}

	void Start() {
		endless = caterpillarManager.Instance.endlessLevel;
	}

	//use only for play button on title screen
	public void loadFirstLevel() {
		destroyMusic();
		SceneManager.LoadScene ("level 1");
		click.enabled = true;
		click.Play ();
	}

	//load from level select, level complete and credits
	public void loadMainMenu() {
		Time.timeScale = 1.0f;
		SceneManager.LoadScene ("title");
		click.enabled = true;
		click.Play ();
	}

	//load from level complete
	public void replay() {
		Scene currentScene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (currentScene.name);
		click.Play ();
	}

	//load from level complete
	public void nextLevel() {
		if (endless) {
			for (int i = 0; i < allLevelsEndless.Length; i++) {
				if (SceneManager.GetActiveScene ().name == allLevelsEndless [i].levelName) {
					SceneManager.LoadScene (allLevelsEndless [i + 1].levelName);
				}
			}
		} else {
			for (int i = 0; i < allLevels.Length; i++) {
				if (SceneManager.GetActiveScene ().name == allLevels [i].levelName) {
					SceneManager.LoadScene (allLevels [i + 1].levelName);
				}
			}
		}
		click.Play ();
	}

	//load from level select
	public void loadLevel() {
		destroyMusic();
		int thisLevel = transform.GetSiblingIndex();
		if (SceneManager.GetActiveScene().name == "levelSelect") {
			SceneManager.LoadScene(highScoreManager.Instance.arcadeLevels[thisLevel].levelName);
		} else {
			SceneManager.LoadScene(highScoreManager.Instance.endlessLevels[thisLevel].levelName);
		}
	}

	//load from title and level select endless
	public void loadChooselvl() {
		loadNewScene("levelSelect");
	}

	//load from level select
	public void loadChooseEndlessLevel() {
		loadNewScene("levelSelectEndless");
	}

	//load from title
	public void loadCredits() {
		loadNewScene("credits");
	}

	void loadNewScene(string sceneName) {
		//if music is playing already keep music object between scenes
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
		SceneManager.LoadScene (sceneName);
		click.enabled = true;
		click.Play ();
	} 

	void destroyMusic() {
		Destroy (musicManager.Instance.music);
		musicManager.Instance.isPlaying = false;
	}
}
