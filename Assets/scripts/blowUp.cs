using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blowUp : MonoBehaviour {
	public int star1;
	public int star2;
	public int star3;
	public float starDelay;
	public Transform starfill1;
	public Transform starfill2;
	public Transform starfill3;

	private float maxScale;
	private float currScale;
	private float acc;
	private float vel;

	private bool needScaling;
	private int playerScore;

	private bool star1NotInstantiated;
	private bool star2NotInstantiated;
	private bool star3NotInstantiated;

	private float timeTillDelay1;
	private float timeTillDelay2;
	private float timeTillDelay3;

	public blowUpGeneral endMessage;

	// Use this for initialization
	void Start () {
		needScaling = true;
		currScale = 0.1f;
		acc = -0.2f;
		vel = 2.0f;
		star1NotInstantiated = true;
		star2NotInstantiated = true;
		star3NotInstantiated = true;

		timeTillDelay1 = 0;
		timeTillDelay2 = 0;
		timeTillDelay3 = 0;

		maxScale = GetComponent<RectTransform> ().localScale.x;
		GetComponent<RectTransform> ().localScale = new Vector3(currScale, currScale);
		playerScore = GameObject.Find ("game manager").GetComponent<scoreCount> ().playerScore;

		endMessage = new blowUpGeneral (vel, acc, currScale);
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

			timeTillDelay1 = updatedTime (timeTillDelay1);
			if (timeTillDelay1 >= starDelay) {
				launchStar (star1, star1NotInstantiated, starfill1);
				star1NotInstantiated = false;
			}
			timeTillDelay2 = updatedTime (timeTillDelay2);
			if (timeTillDelay2 >= 2 * starDelay) {
				launchStar (star2, star2NotInstantiated, starfill2);
				star2NotInstantiated = false;
			}
			timeTillDelay3 = updatedTime (timeTillDelay3);
			if (timeTillDelay3 >= 3 * starDelay) {
				launchStar (star3, star3NotInstantiated, starfill3);
				star3NotInstantiated = false;
			}
		}
	}

	void launchStar(int requiredScore, bool notInstantiated, Transform star) {
		if (playerScore >= requiredScore && notInstantiated) {
			instantiateStar (star);
		}
	}

	float updatedTime(float time) {
		float newTime = time + Time.deltaTime;
		return newTime;
	}

	void instantiateStar(Transform star) {
		Transform newStar = Instantiate (star);
		newStar.transform.SetParent (GetComponent<RectTransform>(), false);
	}
}
