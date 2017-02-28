﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//placed on empty gameobject in level select scene. Rewrites high score and star appearances if necessary so player sees updated version of their score
public class setHighScores : MonoBehaviour {
	public Transform level1;
	public Transform level2;
	public Transform level3;
	public Transform level4;
	public Transform level5;

	public Transform canvas;
	public Transform starFill;

	// Use this for initialization
	void Start () {
		highScore ();
		checkLevels ();
	}

	//updates visible highscore
	void highScore() {
		level1.transform.Find("highScore").GetComponent<Text>().text = "High Score: " + highScoreManager.Instance.One.highScore;
		level2.transform.Find("highScore").GetComponent<Text>().text = "High Score: " + highScoreManager.Instance.Two.highScore;
		level3.transform.Find("highScore").GetComponent<Text>().text = "High Score: " + highScoreManager.Instance.Three.highScore;
		level4.transform.Find("highScore").GetComponent<Text>().text = "High Score: " + highScoreManager.Instance.Four.highScore;
		level5.transform.Find("highScore").GetComponent<Text>().text = "High Score: " + highScoreManager.Instance.Five.highScore;
	}

	//fills stars in correct place if player has acquired them in levels
	void checkLevels() {
		checkStars (highScoreManager.Instance.One, level1);
		checkStars (highScoreManager.Instance.Two, level2);
		checkStars (highScoreManager.Instance.Three, level3);
		checkStars (highScoreManager.Instance.Four, level4);
		checkStars (highScoreManager.Instance.Five, level5);

	}

	void checkStars(highScoreManager.level level, Transform levelSign) {
		if (level.star1) {
			fillStars (levelSign, "star1");
		}
		if (level.star2) {
			fillStars (levelSign, "star2");
		}
		if (level.star3) {
			fillStars (levelSign, "star3");
		}
	}

	void fillStars(Transform levelSign, string star) {
		Transform desiredStar = levelSign.transform.Find (star);
		Transform newStarFill = Instantiate (starFill);
		newStarFill.transform.SetParent (levelSign, false);
		newStarFill.gameObject.GetComponent<setToStar> ().star = desiredStar;
	}

}