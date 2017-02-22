using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setHighScores : MonoBehaviour {
	public Text level1Score;
	public Text level2Score;
	public Text level3Score;

	// Use this for initialization
	void Start () {
		level1Score.text = "High Score: " + highScoreManager.Instance.level1HighScore;
		level2Score.text = "High Score: " + highScoreManager.Instance.level2HighScore;
		level3Score.text = "High Score: " + highScoreManager.Instance.level3HighScore;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
