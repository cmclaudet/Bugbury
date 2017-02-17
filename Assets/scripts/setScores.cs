﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//rewrites text on level complete message to display correct scores
public class setScores : MonoBehaviour {

	private GameObject manager;
	private int maxStreak;
	private int totfarShots;
	private int playerScore;

	// Use this for initialization
	void Start () {
		//finds all values needed to display score
		manager = GameObject.Find ("game manager");
		findMaxStreakFarShots();
		setupText ();
	}

	void findMaxStreakFarShots() {
		maxStreak = manager.GetComponent<scoreCount> ().maxPlayerStreak;
		totfarShots = manager.GetComponent<scoreCount> ().farShots;
		playerScore = manager.GetComponent<scoreCount> ().playerScore;
	}

	void setupText() {
		Transform maxStreakNum = this.transform.Find ("maxStreakNumber");
		Transform farShotNum = this.transform.Find ("farShotNumber");

		maxStreakNum.GetComponent<Text> ().text = maxStreak.ToString();
		farShotNum.GetComponent<Text> ().text = totfarShots.ToString();
	}

	public void setScoreNum(Transform scoreNumber) {
		scoreNumber.GetComponent<Text> ().text = playerScore.ToString();
	}
}
