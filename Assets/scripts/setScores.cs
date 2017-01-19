using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setScores : MonoBehaviour {

	private GameObject manager;
	private int caterpillarsKilled;
	private int totalCaterpillars;
	private int playerScore;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("game manager");
		caterpillarsKilled = manager.GetComponent<caterpillarManager> ().caterpillarsKilled;
		totalCaterpillars = manager.GetComponent<caterpillarManager> ().totalCaterpillars;
		playerScore = manager.GetComponent<scoreCount> ().playerScore;

		setupText ();
	}

	void setupText() {
		Transform bugcountObj = this.transform.Find ("bugCount");
		Transform scorecountObj = this.transform.Find ("scoreCount");

		bugcountObj.GetComponent<Text> ().text = "Bugs squashed: " + caterpillarsKilled + "/" + totalCaterpillars;
		scorecountObj.GetComponent<Text> ().text = "Score: " + playerScore;
	}
}
