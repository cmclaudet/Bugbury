using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//public functions which are attached to buttons to load different scenes
//ensures button sound effects are played and background music is destroyed/not destroyed/created when appropriate
public class loadScenes : MonoBehaviour {
	public AudioSource music;

	private AudioSource click;

	private string[] allLevels;
	private string[] allLevelsEndless;

	private bool endless;

	void Awake() {
		click = GameObject.Find ("click").GetComponent<AudioSource>();
		allLevels = new string[] { "level 1", "level 2", "level 3", "level 4", "level 5" };
		allLevelsEndless = new string[] {
			"level 1 endless",
			"level 2 endless",
			"level 3 endless",
			"level 4 endless",
			"level 5 endless"
		};
	}

	void Start() {
		endless = caterpillarManager.Instance.endlessLevel;
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
				if (SceneManager.GetActiveScene ().name == allLevelsEndless [i]) {
					SceneManager.LoadScene (allLevelsEndless [i + 1]);
				}
			}
		} else {
			for (int i = 0; i < allLevels.Length; i++) {
				if (SceneManager.GetActiveScene ().name == allLevels [i]) {
					SceneManager.LoadScene (allLevels [i + 1]);
				}
			}
		}
		click.Play ();
	}

	public void loadLevel() {
		destroyMusic();
		int thisLevel = transform.GetSiblingIndex();
		if (SceneManager.GetActiveScene().name == "levelSelect") {
			SceneManager.LoadScene(highScoreManager.Instance.arcadeLevels[thisLevel].levelName);
		} else {
			SceneManager.LoadScene(highScoreManager.Instance.endlessLevels[thisLevel].levelName);
		}
	}

	//load from level select
	public void loadFirstlvl () {
		destroyMusic ();
		SceneManager.LoadScene ("level 1");
		click.Play ();
	}

	//load from level select
	public void loadSecondlvl() {
		destroyMusic ();
		SceneManager.LoadScene ("level 2");
		click.Play ();
	}

	//load from level select
	public void loadThirdlvl () {
		destroyMusic ();
		SceneManager.LoadScene ("level 3");
		click.Play ();
	}

	//load from level select
	public void loadFourthlvl() {
		destroyMusic ();
		SceneManager.LoadScene ("level 4");
		click.Play ();
	}

	//load from level select
	public void loadFifthlvl() {
		destroyMusic ();
		SceneManager.LoadScene ("level 5");
		click.Play ();
	}

	//load from title
	public void loadChooselvl() {
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
		SceneManager.LoadScene ("levelSelect");
		click.Play ();
	}

	//load from level select
	public void loadFirstlvlEndless () {
		destroyMusic ();
		SceneManager.LoadScene ("level 1 endless");
		click.Play ();
	}

	//load from level select
	public void loadSecondlvlEndless() {
		destroyMusic ();
		SceneManager.LoadScene ("level 2 endless");
		click.Play ();
	}

	//load from level select
	public void loadThirdlvlEndless () {
		destroyMusic ();
		SceneManager.LoadScene ("level 3 endless");
		click.Play ();
	}

	//load from level select
	public void loadFourthlvlEndless() {
		destroyMusic ();
		SceneManager.LoadScene ("level 4 endless");
		click.Play ();
	}

	//load from level select
	public void loadFifthlvlEndless() {
		destroyMusic ();
		SceneManager.LoadScene ("level 5 endless");
		click.Play ();
	}

	public void loadChooseEndlessLevel() {
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
		SceneManager.LoadScene ("levelSelectEndless");
		click.Play ();

	}

	//load from level select, level complete and credits
	public void loadMainMenu() {
		Time.timeScale = 1.0f;
		SceneManager.LoadScene ("title");
		click.enabled = true;
		click.Play ();
	}

	//load from title
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
