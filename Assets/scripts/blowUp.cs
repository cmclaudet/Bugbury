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
	public float starDelay;		//delay time between appearance of each star
	public Transform starfill1;		//star filling object for each star
	public Transform starfill2;
	public Transform starfill3;

	//values for scaling of message itself
	private float maxScale;
	public float initScale;
	public float acc;
	public float vel;

	private bool needScaling;	//once false scaling no longer occurs
	private int playerScore;

	private bool star1NotInstantiated;	//bool values ensure stars not repeatedly instantiated
	private bool star2NotInstantiated;
	private bool star3NotInstantiated;

	private float timeTillDelay1;		//timers from instantiation of each star
	private float timeTillDelay2;
	private float timeTillDelay3;

	public blowUpGeneral endMessage;

	void Awake() {
		playerScore = GameObject.Find ("game manager").GetComponent<scoreCount> ().calcScore();
	}

	// Use this for initialization
	void Start () {
		needScaling = true;
		star1NotInstantiated = true;
		star2NotInstantiated = true;
		star3NotInstantiated = true;

		timeTillDelay1 = 0;
		timeTillDelay2 = 0;
		timeTillDelay3 = 0;

		maxScale = GetComponent<RectTransform> ().localScale.x;
		GetComponent<RectTransform> ().localScale = new Vector3(initScale, initScale);

		endMessage = new blowUpGeneral (vel, acc, initScale);
	}
	
	// Update is called once per frame
	void Update () {
		if (needScaling) {
			endMessage.updateVelocity();
			endMessage.updateScale ();
			GetComponent<RectTransform> ().localScale = new Vector3 (endMessage.scale, endMessage.scale);
		}

		if (endMessage.scale >= maxScale) {
			needScaling = false;

			//updates timers for each star. Min value necessary to instantiate increases with star so that star 1 appears first and star 3 appears last
			timeTillDelay1 = updatedTime (timeTillDelay1);
			if (timeTillDelay1 >= starDelay) {
				launchStar (star1score, star1NotInstantiated, starfill1);
				star1NotInstantiated = false;
			}
			timeTillDelay2 = updatedTime (timeTillDelay2);
			if (timeTillDelay2 >= 2 * starDelay) {
				launchStar (star2score, star2NotInstantiated, starfill2);
				star2NotInstantiated = false;
			}
			timeTillDelay3 = updatedTime (timeTillDelay3);
			if (timeTillDelay3 >= 3 * starDelay) {
				launchStar (star3score, star3NotInstantiated, starfill3);
				star3NotInstantiated = false;
			}
		}
	}

	void launchStar(int requiredScore, bool notInstantiated, Transform star) {
		//only launch star if player has reached appropriate goal
		if (playerScore >= requiredScore && notInstantiated) {
			instantiateStar (star);
		}
	}

	//updates timer values
	float updatedTime(float time) {
		float newTime = time + Time.deltaTime;
		return newTime;
	}

	void instantiateStar(Transform star) {
		Transform newStar = Instantiate (star);
		newStar.transform.SetParent (GetComponent<RectTransform>(), false);
	}
}
