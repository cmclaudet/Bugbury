using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifeManager : MonoBehaviour {
	public GameObject lifeImages;
	public Transform gameOverMessage;
	public Button pauseButton;
	public Transform canvas;
	public new GameObject camera;
	public float cameraLifeShakeDuration;
	public AudioSource hurtSound;
//	public int livesLeft{ get; set; }

	private Image life1;
	private Image life2;
	private Image life3;

	public bool lifeLost{ get; set; }
	private int lives;
	private bool gameOverNotDone;
	private bool checkWhenScalingIsDone;

	private Transform gameOverSign;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1.0f;
		gameOverNotDone = true;

		//becomes true when game over screen is instantiated. When this happens initial scaling is repeatedly checked to see when it is done. Must be done so time can be stopped after this.
		checkWhenScalingIsDone = false;
		lives = 3;
		lifeLost = false;

		getLifeImages ();
	}
	
	// Update is called once per frame
	void Update () {
		if (lifeLost) {
			lives -= 1;
			hurtSound.Play ();
			if (lives == 2) {
				life1.gameObject.SetActive (false);
				camera.GetComponent<CameraShake> ().shakeDuration = cameraLifeShakeDuration;
				camera.GetComponent<CameraShake> ().enabled = true;
			} else if (lives == 1) {
				life2.gameObject.SetActive (false);
				camera.GetComponent<CameraShake> ().enabled = true;
			} else {
				life3.gameObject.SetActive (false);
				camera.GetComponent<CameraShake> ().enabled = true;
				if (gameOverNotDone) {
					gameOver ();
				}
			}
			lifeLost = false;
		}

		if (checkWhenScalingIsDone) {
			if (gameOverSign.GetComponent<scaleSetup> ().doneScaling) {
				camera.GetComponent<CameraShake> ().stopAndReset ();	//ensures camera does not keep shaking after game over screen appears
				Time.timeScale = 0;		//only pause time after scaling is done, or object won't scale
			}
				
		}
	}

	void getLifeImages() {
		Image[] allLives = lifeImages.GetComponentsInChildren<Image> ();
		life1 = allLives [0];
		life2 = allLives [1];
		life3 = allLives [2];
	}

	void gameOver() {
		GetComponent<caterpillarManager> ().control = false;
		pauseButton.interactable = false;

		gameOverSign = Instantiate (gameOverMessage);
		gameOverSign.transform.SetParent (canvas, false); 
		gameOverNotDone = false;
		checkWhenScalingIsDone = true;
	}
}
