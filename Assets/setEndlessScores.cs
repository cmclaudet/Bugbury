using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setEndlessScores : MonoBehaviour {
	public Transform level1;
	public Transform level2;
	public Transform level3;
	public Transform level4;
	public Transform level5;

	// Use this for initialization
	void Start () {
		highScore ();
	}

	//updates visible highscore
	void highScore() {
		level1.GetComponentsInChildren<Text>()[1].text = "High Score: " + highScoreManager.Instance.One.HSEndless;
		level2.GetComponentsInChildren<Text>()[1].text = "High Score: " + highScoreManager.Instance.Two.HSEndless;
		level3.GetComponentsInChildren<Text>()[1].text = "High Score: " + highScoreManager.Instance.Three.HSEndless;
		level4.GetComponentsInChildren<Text>()[1].text = "High Score: " + highScoreManager.Instance.Four.HSEndless;
		level5.GetComponentsInChildren<Text>()[1].text = "High Score: " + highScoreManager.Instance.Five.HSEndless;
	}


}
