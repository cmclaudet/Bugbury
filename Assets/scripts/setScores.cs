using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//rewrites text on level complete message to display correct scores
public class setScores : MonoBehaviour {

	private GameObject manager;
	private int caterpillarsKilled;
	private int totalCaterpillars;
	private int playerScore;

	// Use this for initialization
	void Start () {
		//finds all values needed to display score
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
