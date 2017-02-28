using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//rewrites text for max streak number, far shots number and score number on level complete message to display correct scores
public class setScores : MonoBehaviour {

	private int maxStreak;
	private int totfarShots;
	private int playerScore;

	// Use this for initialization
	void Start () {
		//finds all values needed to display score
		findMaxStreakFarShots();
		setupText ();
	}

	void findMaxStreakFarShots() {
		maxStreak = scoreCount.Instance.maxPlayerStreak;
		totfarShots = scoreCount.Instance.farShots;
		playerScore = scoreCount.Instance.playerScore;
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
