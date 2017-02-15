using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sets scale up on complete message
//instaniates stars if player has reached certain score
//sets up time delay between star instantiation
public class blowUp : MonoBehaviour {
	public int star1score;	//necessary score for 1 star
	public int star2score;	//necessary score for 2 stars
	public int star3score;	//necessary score for 3 stars

	public float textDelay;		//delay time between appearance of text: max streak, far shots, score
	public float postScoreDelay;	//delays between score and stars appearing
	public float starDelay;		//delay time between appearance of each star

	private Transform star1;
	private Transform star2;
	private Transform star3;

	private int playerScore;
	private bool needScaling;

	private GameObject scoreNumber;

	public blowUpGeneral endMessage;

	void Awake() {
		playerScore = GameObject.Find ("game manager").GetComponent<scoreCount> ().calcScore();

	}

	// Use this for initialization
	void Start () {
		scoreNumber = transform.Find ("scoreNumber").gameObject;
		scoreNumber.gameObject.SetActive (false);

		setTextTimeDelays ();
		setStarTimeDelays ();
		inactivateStars ();
	}
	
	// Update is called once per frame
	void Update () {
		needScaling = GetComponent<scaleSetup> ().needScaling;

		if (needScaling == false) {
			scoreNumber.SetActive (true);
		}
	}

	//set time delay between instantiation of lvl complete message and text objects
	void setTextTimeDelays() {
		Transform[] textObjects = {
			transform.Find ("maxStreak"),
			transform.Find ("maxStreakNumber"),
			transform.Find ("farShots"),
			transform.Find ("farShotNumber"),
			transform.Find ("score"),
			transform.Find ("scoreNumber")
		};

		for (int i = 0; i < textObjects.Length; i++) {
			textObjects [i].GetComponent<scaleSetup> ().timeDelay = textDelay * (i + 1);
		}

	}

	//set the time delays between instantiation of level complete message and star objects
	void setStarTimeDelays() {
		star1 = transform.Find ("starFill 1");
		star2 = transform.Find ("starFill 2");
		star3 = transform.Find ("starFill 3");

		Transform[] stars = {star1, star2, star3};

		for (int i = 0; i < stars.Length; i++) {
			stars [i].GetComponent<scaleSetup> ().timeDelay = textDelay * 6 + postScoreDelay + starDelay * (i + 1);
			stars [i].GetComponent<RectTransform> ().SetAsLastSibling ();
		}
	}

	void inactivateStars() {
		if (playerScore < star1score) {
			star1.gameObject.SetActive (false);
		}
		if (playerScore < star2score) {
			star2.gameObject.SetActive (false);
		}
		if (playerScore < star3score) {
			star3.gameObject.SetActive (false);
		}
	}
}
