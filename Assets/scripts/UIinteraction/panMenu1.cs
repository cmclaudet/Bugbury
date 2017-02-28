﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//triggers panning animation when player presses forward or back button to focus on different level
//disables forward button if player focuses on last level or back button if player focuses on first level
public class panMenu1 : MonoBehaviour {
	public Button backButton;
	public Button forwardButton;
	public Animator lvl1;
	public Animator lvl2;
	public Animator lvl3;
	public Animator lvl4;
	public Animator lvl5;
	public AudioSource woosh;

	private int currentLevel;
	private Animator[] levelAnimators;

	// Use this for initialization
	void Start () {
		backButton.interactable = false;
		currentLevel = 1;		//keeps track of current level player is looking at
		levelAnimators = new Animator[] { lvl1, lvl2, lvl3, lvl4, lvl5};

		foreach (Animator level in levelAnimators) { 
			level.speed = 3.0f;		//increase animation speed
			level.enabled = false;	//ensures default animation does not play on runtime
		}

	}

	//focus on next level
	public void toNextLevel() {
		Animator thisLevel = levelAnimators [currentLevel - 1];
		Animator nextLevel = levelAnimators [currentLevel];

		foreach (Animator level in levelAnimators) {
			//set all level select gameobjects false besides the ones being animated so the player cannot see other levels underneath
			if (level == thisLevel || level == nextLevel) {
				level.gameObject.SetActive (true);
			} else {
				level.gameObject.SetActive (false);
			}
		}

		//all necessary animations are on every level select object
		thisLevel.enabled = true;
		nextLevel.enabled = true;
		thisLevel.Play ("panCenterLeft");
		nextLevel.Play ("panRightCenter");

		backButton.interactable = true;
		//disable button if necessary
		if (currentLevel == (levelAnimators.Length - 1)) {
			forwardButton.interactable = false;
		}

		woosh.Play ();

		//update current level
		currentLevel += 1;

	}

	//focus on previous level
	public void toLastLevel() {
		Animator thisLevel = levelAnimators [currentLevel - 1];
		Animator lastLevel = levelAnimators [currentLevel - 2];

		foreach (Animator level in levelAnimators) {
			if (level == thisLevel || level == lastLevel) {
				level.gameObject.SetActive (true);
			} else {
				level.gameObject.SetActive (false);
			}
		}

		lastLevel.enabled = true;
		thisLevel.enabled = true;
		lastLevel.Play ("panLeftCenter");
		thisLevel.Play ("panCenterRight");

		forwardButton.interactable = true;
		if (currentLevel == 2) {
			backButton.interactable = false;
		}

		woosh.Play ();
		currentLevel -= 1;

	}

}