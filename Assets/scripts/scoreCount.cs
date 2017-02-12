using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreCount : MonoBehaviour {
	//score, player combo and far values stored here
	public int playerScore { get; set; }
	public int playerCombo { get; set; }
	public int maxPlayerStreak { get; set; }
	public int farShots{ get; set; }
	public bool far { get; set; }

	// Use this for initialization
	void Start () {
		playerScore = 0;
		playerCombo = 0;
		maxPlayerStreak = 0;
		farShots = 0;
		far = false;
	}

}
