using System.Collections;
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
//		caterpillarsKilled = manager.GetComponent<caterpillarManager> ().caterpillarsKilled;
//		totalCaterpillars = manager.GetComponent<caterpillarManager> ().totalCaterpillars;
		findMaxStreakFarShots();
		setupText ();
	}

	void findMaxStreakFarShots() {
		maxStreak = manager.GetComponent<scoreCount> ().maxPlayerStreak;
		totfarShots = manager.GetComponent<scoreCount> ().farShots;
		playerScore = manager.GetComponent<scoreCount> ().actualScore;
	}

	void setupText() {
		Transform maxStreakObj = this.transform.Find ("maxStreak");
		Transform farShotsObj = this.transform.Find ("farShots");
		Transform totalScore = this.transform.Find ("score");

		maxStreakObj.GetComponent<Text> ().text = "Max Streak: " + maxStreak;
		farShotsObj.GetComponent<Text> ().text = "Far Shots: " + totfarShots;
		totalScore.GetComponent<Text> ().text = "SCORE: " + playerScore;
	}
}
