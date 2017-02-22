using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//sets scale up on complete message
//instaniates stars if player has reached certain score
//sets up time delay between text and star instantiation
//activates score number after its time delay has passed. score number must be inactive to start with or player will see it.
//updates max attained stars on high score singleton
public class blowUp : MonoBehaviour {
	public float textDelay;		//delay time between appearance of text: max streak, far shots, score

	public Transform starfill1;
	public Transform starfill2;
	public Transform starfill3;

	//obtain from manager
	private int star1score;	//necessary score for 1 star
	private int star2score;	//necessary score for 2 stars
	private int star3score;	//necessary score for 3 stars

	private int playerScore;
	private GameObject manager;
	private bool needScaling;

	public Transform scoreNumber;		//score number object. must define because this is activated in the script
	private float scoreNumDelay;		//score number time delay. score number is activated once this has passed
	private float timePassed;			//time passed before score number delay time is reached
	private bool scoreNumInstantiated;

	public blowUpGeneral endMessage;

	void Awake() {
		manager = GameObject.Find ("game manager");
		playerScore = manager.GetComponent<scoreCount> ().playerScore;
		setTextTimeDelays ();
	}

	// Use this for initialization
	void Start () {
		timePassed = 0;
		scoreNumInstantiated = false;
		calcScoreDelay ();
		setStarThresholds ();
		inactivateStars ();
	}
	
	// Update is called once per frame
	void Update () {
		if (scoreNumInstantiated == false) {
			timePassed += Time.deltaTime;
		}
		if (timePassed > scoreNumDelay) {
			Transform scoreNum = Instantiate (scoreNumber);
			scoreNum.SetParent (this.transform, false);
			GetComponent<setScores> ().setScoreNum (scoreNum);
			timePassed = 0;
			scoreNumInstantiated = true;
		}
	}

	void calcScoreDelay() {
		scoreNumDelay = 6 * textDelay;
	}

	//set time delay between instantiation of lvl complete message and text objects
	void setTextTimeDelays() {
		Component[] scaleSetups = GetComponentsInChildren (typeof(scaleSetup), true);

		for (int i = 0; i < scaleSetups.Length; i++) {
			scaleSetups [i].GetComponent<scaleSetup>().timeDelay = textDelay;
		}
	}

	void setStarThresholds() {
		star1score = manager.GetComponent<starThreshold> ().threshold1;
		star2score = manager.GetComponent<starThreshold> ().threshold2;
		star3score = manager.GetComponent<starThreshold> ().threshold3;
	}

	//if player does not have sufficient score stars are inactivated
	void inactivateStars() {
		string currentScene = SceneManager.GetActiveScene ().name;
		switch (currentScene) {
		case "level 1":
			highScoreManager.Instance.One = resetAllScores (highScoreManager.Instance.One);
			break;
		case "level 2":
			highScoreManager.Instance.Two = resetAllScores (highScoreManager.Instance.Two);
			break;
		case "level 3":
			highScoreManager.Instance.Three = resetAllScores (highScoreManager.Instance.Three);
			break;
		}

	}

	//resets player high score and player's top star
	highScoreManager.level resetAllScores(highScoreManager.level level) {
		if (playerScore > level.highScore) {
			level.highScore = playerScore;
		}

		if (playerScore < star1score) {
			starfill1.gameObject.SetActive (false);
		} else {
			level.star1 = true;
		}

		if (playerScore < star2score) {
			starfill2.gameObject.SetActive (false);
		} else {
			level.star2 = true;
		}

		if (playerScore < star3score) {
			starfill3.gameObject.SetActive (false);
		} else {
			level.star3 = true;
		}

		return level;
	}

}
