using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//triggers panning animation when player presses forward or back button to focus on different level
//disables forward button if player focuses on last level or back button if player focuses on first level
public class panMenu : MonoBehaviour {
	public Button backButton;
	public Button forwardButton;
	public Animator lvl1;
	public Animator lvl2;
	public Animator lvl3;
	public Animator lvl4;
	public Animator lvl5;
	public GameObject dots;
	public AudioSource woosh;

	public int currentLevel{get;set;}
	private Animator[] levelAnimators;

	// Use this for initialization
	void Start () {
		currentLevel = currentLevelSelectLevel.currentLevel;		//keeps track of current level player is looking at
		levelAnimators = new Animator[] { lvl1, lvl2, lvl3, lvl4, lvl5};

		foreach (Animator level in levelAnimators) { 
			level.speed = 3.0f;		//increase animation speed
			level.enabled = false;	//ensures default animation does not play on runtime
			
			//set all level select menus inactive besides the one the player is looking at
			if (level == levelAnimators[currentLevel - 1]) {
				level.gameObject.SetActive(true);
			} else {
				level.gameObject.SetActive(false);
			}
		}

		//make sure back button is disabled on level 1 and forward button on level 5
		setupButtons();
	}

	void setupButtons() {
		if (currentLevel == 1) {
			forwardButton.interactable = true;
			backButton.interactable = false;
		} else if (currentLevel == levelAnimators.Length) {
			forwardButton.interactable = false;
			backButton.interactable = true;
		} else {
			forwardButton.interactable = true;
			backButton.interactable = true;
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
		dots.GetComponent<changeDot>().switchDotImage(currentLevel, currentLevel - 1);

		//update current level on persistent object
		currentLevelSelectLevel.currentLevel = currentLevel;
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
		dots.GetComponent<changeDot>().switchDotImage(currentLevel, currentLevel + 1);

		//update current level on persistent object
		currentLevelSelectLevel.currentLevel = currentLevel;
	}

}
