using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreCount : MonoBehaviour {
	//score, player combo and far values stored here
	public int farShotBonus;
	public int playerScore { get; set; }

	public int actualScore{ get; set; }
	public int playerCombo { get; set; }
	public int maxPlayerStreak { get; set; }
	public int farShots{ get; set; }
	public bool far { get; set; }

	public GameObject scoreObject;

	// Use this for initialization
	void Start () {
		playerScore = 0;
		actualScore = 0;
		playerCombo = 0;
		maxPlayerStreak = 0;
		farShots = 0;
		far = false;
	}

	public void changeScore(int newScore) {
		playerScore += newScore;
		scoreObject.GetComponent<rewrite> ().rewriteScore (playerScore.ToString ());
	}

}
