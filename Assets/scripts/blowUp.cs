using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sets scale up on complete message
//instaniates stars if player has reached certain score
//sets up time delay between text and star instantiation
//activates score number after its time delay has passed. score number must be inactive to start with or player will see it.
public class blowUp : MonoBehaviour {
	public float textDelay;		//delay time between appearance of text: max streak, far shots, score
//	public float postScoreDelay;	//delays between score and stars appearing
//	public float starDelay;		//delay time between appearance of each star

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
//		scoreNumber = transform.Find ("scoreNumber").gameObject;
		timePassed = 0;
		scoreNumInstantiated = false;
		calcScoreDelay ();
//		setStarTimeDelays ();
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

//		needScaling = GetComponent<scaleSetup> ().needScaling;
//		if (needScaling == false) {
//			scoreNumber.SetActive (true);
//		}
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

//		scoreNumDelay = scoreNumber.GetComponent<scaleSetup>().timeDelay;
//		Debug.Log (scoreNumber.GetComponent<scaleSetup> ().timeDelay);
	}

	//set the time delays between instantiation of level complete message and star objects
/*	void setStarTimeDelays() {
		Transform[] stars = {star1, star2, star3};

		for (int i = 0; i < stars.Length; i++) {
			stars [i].GetComponent<scaleSetup> ().timeDelay = textDelay * 6 + postScoreDelay + starDelay * (i + 1);
			stars [i].GetComponent<RectTransform> ().SetAsLastSibling ();
		}
	}
*/
	void setStarThresholds() {
		star1score = manager.GetComponent<starThreshold> ().threshold1;
		star2score = manager.GetComponent<starThreshold> ().threshold2;
		star3score = manager.GetComponent<starThreshold> ().threshold3;
	}

	//if player does not have sufficient score stars are inactivated
	void inactivateStars() {
		if (playerScore < star1score) {
			starfill1.gameObject.SetActive (false);
		}
		if (playerScore < star2score) {
			starfill2.gameObject.SetActive (false);
		}
		if (playerScore < star3score) {
			starfill3.gameObject.SetActive (false);
		}
	}


}
