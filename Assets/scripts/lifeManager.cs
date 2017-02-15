using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifeManager : MonoBehaviour {
	public Text livesText;
	public Transform gameOverMessage;
	public Button pauseButton;
	public Transform canvas;
//	public int livesLeft{ get; set; }
	public bool lifeLost{ get; set; }
	private int lives;
	private bool gameOverNotDone;
	private bool checkWhenScalingIsDone;

	private Transform gameOverSign;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1.0f;
		gameOverNotDone = true;

		//becomes true when game over screen is instantiated. When this happens initial scaling is repeatedly checked to see when it is done. Must be done so time can be stopped after this.
		checkWhenScalingIsDone = false;
		lives = 3;
		lifeLost = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (lifeLost) {
			lives -= 1;
			if (lives == 2) {
				livesText.text = "Lives: l l";
			} else if (lives == 1) {
				livesText.text = "Lives: l";
			} else {
				livesText.text = "Lives:";
				if (gameOverNotDone) {
					gameOver ();
				}
			}
			lifeLost = false;
		}

		if (checkWhenScalingIsDone) {
			if (gameOverSign.GetComponent<scaleSetup> ().needScaling == false) {
				Time.timeScale = 0;		//only pause time after scaling is done, or object won't scale
			}
				
		}
	}

	void gameOver() {
		GetComponent<caterpillarManager> ().control = false;
		pauseButton.interactable = false;

		gameOverSign = Instantiate (gameOverMessage);
		gameOverSign.transform.SetParent (canvas, false); 
		gameOverNotDone = false;
		checkWhenScalingIsDone = true;
	}
}
