using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//triggers level complete message when level is done
//is triggered from caterpillar manager when all caterpillars have been inactivated
public class triggerLevelComplete : MonoBehaviour {
	public GameObject camera;
	public Button pauseButton;
	public Transform completeMessage;
	public Transform completeMessageEndless;
	public Transform canvas;
	public AudioSource bam;

	private bool endless;
	private GameObject[] caterpillars;
	private GameObject[] rocks;

	void Awake() {
		caterpillarManager.Instance.levelComplete = gameObject;
	}

	void Start() {
		endless = caterpillarManager.Instance.endlessLevel;
	}

	//triggered from caterpillar manager singleton
	public void setupEnd() {
		camera.GetComponent<CameraShake> ().shakeDuration = caterpillarManager.Instance.cameraScoreNumShakeDuration;
		lifeManager.Instance.control = false;
		pauseButton.interactable = false;
		caterpillarManager.Instance.resetMaxStreak ();
		caterpillars = GameObject.FindGameObjectsWithTag ("caterpillar");
		rocks = GameObject.FindGameObjectsWithTag ("rock");

		if (endless) {
			foreach (GameObject caterpillar in caterpillars) {
				caterpillar.GetComponent<Animator>().Stop();
				caterpillar.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
			}
			foreach (GameObject rock in rocks) {
				Component pointer = rock.GetComponentsInChildren (typeof(LineRenderer), true) [1];
				pointer.gameObject.SetActive (false);
			}
			Transform levelEnded = Instantiate (completeMessageEndless);
			levelEnded.transform.SetParent (canvas, false);
		} else {
//			camera.GetComponent<CameraShake> ().shakeDuration = caterpillarManager.Instance.cameraScoreNumShakeDuration;
//			lifeManager.Instance.control = false;
//			pauseButton.interactable = false;

//			caterpillarManager.Instance.resetMaxStreak ();
			Transform levelDone = Instantiate (completeMessage);
			levelDone.transform.SetParent (canvas, false);

			//with beatenLevel5 set to true players can be prompted to rate the app in the credits scene
			//players will not be prompted more than once
			if (SceneManager.GetActiveScene ().name == "level 5") {
				highScoreManager.Instance.beatenLevel5 = true;
			}
		}
	}
	

}
