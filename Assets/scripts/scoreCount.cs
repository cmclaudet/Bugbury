﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreCount : MonoBehaviour {

	private static scoreCount _instance;

	public static scoreCount Instance {
		get {
			if (_instance == null) {
				GameObject go = new GameObject ("scoreCount");
				go.AddComponent<scoreCount> ();
			}
			return _instance;
		}
	}

	//score, player combo and far values stored here
	public int farShotBonus { get; set; }
	public int playerScore { get; set; }

	public int playerCombo { get; set; }
	public int maxPlayerStreak { get; set; }
	public int farShots{ get; set; }
	public bool far { get; set; }

	public GameObject scoreObject;

	void Awake() {
		_instance = this;

	}

	// Use this for initialization
	void Start () {
		//far shot bonus is rewritten at beginning of every level
		farShotBonus = 3;
		playerScore = 0;
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
