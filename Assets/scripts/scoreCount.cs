﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreCount : MonoBehaviour {
	//score, player combo and far values stored here
	public Text scoreText;
	public int playerScore { get; set; }
	public int playerCombo { get; set; }
	public bool far { get; set; }

	// Use this for initialization
	void Start () {
		playerScore = 0;
		playerCombo = 0;
		far = false;
	}
	
	// Update is called once per frame
	void Update () {
		//display player score
		scoreText.text = "Score: " + playerScore;
	}
}
