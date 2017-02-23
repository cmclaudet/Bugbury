﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class detectLifeLoss : MonoBehaviour {
	public GameObject lifeImages;
	public Transform gameOverMessage;
	public Button pauseButton;
	public Transform canvas;
	public AudioSource hurtSound;

	private Image life1;
	private Image life2;
	private Image life3;

	private bool gameOverNotDone;
	private bool checkWhenScalingIsDone;

	private Transform gameOverSign;
	private float cameraLifeShakeDuration = 0.2f;

	void Awake() {
		lifeManager.Instance.camera = gameObject;
	}

	// Use this for initialization
	void Start () {
		Time.timeScale = 1.0f;
		gameOverNotDone = true;

		//becomes true when game over screen is instantiated. When this happens initial scaling is repeatedly checked to see when it is done. Must be done so time can be stopped after this.
		checkWhenScalingIsDone = false;
		getLifeImages ();
	}
	
	//when life is lost camera shakes and one life image is disabled
	//if no lives are left game over screen is instantiated
	public void lifeLost (int lives) {
		hurtSound.Play ();
		if (lives == 2) {
			life1.gameObject.SetActive (false);
			GetComponent<CameraShake> ().shakeDuration = cameraLifeShakeDuration;
			GetComponent<CameraShake> ().enabled = true;
		} else if (lives == 1) {
			life2.gameObject.SetActive (false);
			GetComponent<CameraShake> ().enabled = true;
		} else {
			life3.gameObject.SetActive (false);
			GetComponent<CameraShake> ().enabled = true;
			if (gameOverNotDone) {
				gameOver ();
			}
		}

	}

	void Update() {
		if (checkWhenScalingIsDone) {
			if (gameOverSign.GetComponent<scaleSetup> ().doneScaling) {
				GetComponent<CameraShake> ().stopAndReset ();	//ensures camera does not keep shaking after game over screen appears
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
		lifeManager.Instance.control = false;
		pauseButton.interactable = false;

		gameOverSign = Instantiate (gameOverMessage);
		gameOverSign.transform.SetParent (canvas, false); 
		gameOverNotDone = false;
		checkWhenScalingIsDone = true;
	}
}