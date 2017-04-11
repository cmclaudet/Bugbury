using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*sets stars to inactive if player has not reached star threshold scores
  sets up time delay between text and star instantiation
  activates score number after its time delay has passed. score number must be inactive to start with or player will see it.
  updates max attained stars and high score on high score singleton 
  makes buttons non interactable to start with and makes them interactable once final object has finished scaling*/
public class resetScores : MonoBehaviour {
	public float textDelay;		//delay time between appearance of text: max streak, far shots, score

	//stars which appear at level end
	public Transform starfill1;
	public Transform starfill2;
	public Transform starfill3;

	//perfect text appears if player has perfect score
	public Transform perfectText;

	//obtain from manager
	private int star1score;	//necessary score for 1 star
	private int star2score;	//necessary score for 2 stars
	private int star3score;	//necessary score for 3 stars
	private int perfectScore;	//score that player would get from perfect run

	private int playerScore;
	private int maxStreak;
	private int farShots;
	private bool needScaling;

	public Transform scoreNumber;		//score number prefab object. must define because this is activated in the script
	private Transform scoreNum;			//reference to instantiated score number object
	private float scoreNumDelay;		//score number time delay. score number is activated once this has passed
	private float timePassed;			//time passed before score number delay time is reached
	private bool scoreNumInstantiated;
	private bool noStarsAttained = false;		//if no stars are attained score number must reactivate buttons on scale finish
	private bool activateButtonsSet = false;	//checks if activate buttons script has already been added to something before adding it
	public Button[] allButtons{get;set;}
	public blowUpGeneral endMessage;

	void Awake() {
		playerScore = scoreCount.Instance.playerScore;
		maxStreak = scoreCount.Instance.maxPlayerStreak;
		farShots = scoreCount.Instance.farShots;
		setTextTimeDelays ();
	}

	// Use this for initialization
	void Start () {
		timePassed = 0;
		scoreNumInstantiated = false;
		nonInteractableButtons();
		calcScoreDelay ();
		setStarThresholds ();
		setPerfectScore ();
		inactivateStars ();
	}
	
	// Update is called once per frame
	void Update () {
		if (scoreNumInstantiated == false) {
			timePassed += Time.deltaTime;
		}
		if (timePassed > scoreNumDelay) {
			//instantiate score number when score number time delay has passed
			scoreNum = Instantiate (scoreNumber);
			if (noStarsAttained) {
				addActivateButtonScript(scoreNum);
			}

			scoreNum.SetParent (this.transform, false);
			GetComponent<setScores> ().setScoreNum (scoreNum);	//function rewrites score number to player score
			timePassed = 0;
			scoreNumInstantiated = true;
		}
	}

	void calcScoreDelay() {
		//find order of score text. Score number text order must be one higher than this as it appears staright after socre text.
		Transform scoreText = transform.Find("score");
		scoreNumDelay = (scoreText.GetComponent<scaleSetup>().order + 1) * textDelay;
	}

	//set time delay between instantiation of lvl complete message and text objects
	void setTextTimeDelays() {
		Component[] scaleSetups = GetComponentsInChildren (typeof(scaleSetup), true);

		for (int i = 0; i < scaleSetups.Length; i++) {
			scaleSetups [i].GetComponent<scaleSetup>().timeDelay = textDelay;
		}
	}

	void setStarThresholds() {
		star1score = scoreCount.Instance.star1threshold;
		star2score = scoreCount.Instance.star2threshold;
		star3score = scoreCount.Instance.star3threshold;
	}

	void setPerfectScore() {
		perfectScore = scoreCount.Instance.perfectScore;
	}

	void nonInteractableButtons() {
		allButtons = GetComponentsInChildren<Button>();
		foreach (Button button in allButtons) {
			button.interactable = false;
		}
	}

	//if player does not have sufficient score stars are inactivated
	void inactivateStars() {
		string currentScene = SceneManager.GetActiveScene ().name;
		foreach (highScoreManager.levelArcade level in highScoreManager.Instance.arcadeLevels) {
			if (level.levelName == currentScene) {
				resetAllScores(level);
			}
		}

		foreach (highScoreManager.levelEndless level in highScoreManager.Instance.endlessLevels) {
			if (level.levelName == currentScene) {
				resetEndlessScore(level);
			}
		}

	}

	//resets player high score and player's top star
	//adds activate button component to final displayed object (eg if player doesn't get any stars, the score number object activates buttons)
	void resetAllScores(highScoreManager.levelArcade level) {
		if (playerScore > level.highScore) {
			level.highScore = playerScore;
		}

		if (playerScore == perfectScore) {
			level.perfect = true;
		} else {
			perfectText.gameObject.SetActive (false);
		}

		if (playerScore < star1score) {
			starfill1.gameObject.SetActive (false);
			noStarsAttained = true;
			activateButtonsSet = true;
		} else {
			level.star1 = true;
		}

		if (playerScore < star2score) {
			starfill2.gameObject.SetActive (false);
			if (!activateButtonsSet) {
				addActivateButtonScript(starfill1);
			}
		} else {
			level.star2 = true;
		}

		if (playerScore < star3score) {
			starfill3.gameObject.SetActive (false);
			if (!activateButtonsSet) {
				addActivateButtonScript(starfill2);
			}
		} else {
			level.star3 = true;
			addActivateButtonScript(starfill3);
		}

	}

	void addActivateButtonScript(Transform trans) {
		trans.gameObject.AddComponent<activateButtons>();
		activateButtonsSet = true;
	}

	void resetEndlessScore(highScoreManager.levelEndless level) {
		if (playerScore > level.highScore) {
			level.highScore = playerScore;
		}
		if (farShots > level.farShots) {
			level.farShots = farShots;
		}
		if (maxStreak > level.maxStreak) {
			level.maxStreak = maxStreak;
		}

	}
}
