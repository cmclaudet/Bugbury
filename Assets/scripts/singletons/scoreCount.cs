using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//stores player score, player streak number, number of far shots and whether the last shot was a far shot or not
//stores star thresholds. these are set in star threshold script on setSceneVars gameobject in hierarchy
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

	//score thresholds for getting the first, second and third star
	public int star1threshold{ get; set; }
	public int star2threshold{ get; set; }
	public int star3threshold{ get; set; }

	public GameObject scoreObject;

	void Awake() {
		_instance = this;

	}

	// Use this for initialization
	void Start () {
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
