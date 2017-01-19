using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreCount : MonoBehaviour {
	public Text scoreText;
	public Text comboText;
	public Text farText;
	public int playerScore { get; set; }
	public int playerCombo { get; set; }
	public bool far { get; set; }

	// Use this for initialization
	void Start () {
		playerScore = 0;
		playerCombo = 0;
		comboText.text = " ";
		farText.text = " ";
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Score: " + playerScore;

		if (playerCombo > 2) {
			comboText.text = "COMBO x" + playerCombo;
		} else {
			comboText.text = " ";
		} 

		if (far) {
			farText.text = "FAR x2";
		} else {
			farText.text = " ";
		}
	}
}
