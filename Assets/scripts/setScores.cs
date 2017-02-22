using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
		resetHighScore ();
	}

	void resetHighScore() {
		Scene currentScene = SceneManager.GetActiveScene ();
		switch (currentScene.name) 
		{
		case "level 1":
			if (playerScore > highScoreManager.Instance.level1HighScore) {
				highScoreManager.Instance.level1HighScore = playerScore;
			}
			break;
		case "level 2":
			if (playerScore > highScoreManager.Instance.level2HighScore) {
				highScoreManager.Instance.level2HighScore = playerScore;
			}
			break;
		case "level 3":
			if (playerScore > highScoreManager.Instance.level3HighScore) {
				highScoreManager.Instance.level3HighScore = playerScore;
			}
			break;
		}
	}
}
