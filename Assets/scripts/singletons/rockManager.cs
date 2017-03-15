using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//instantiates new rock when current rock is launched. Inactivates rocks which have passed off the screen.
//updates player's max player streak (as this can only be changed when player misses a shot, completes the level or misses a caterpillar)
public class rockManager : MonoBehaviour {

	private static rockManager _instance;

	public static rockManager Instance {
		get {
			if (_instance == null) {
				GameObject go = new GameObject ("rockManager");
				go.AddComponent<rockManager> ();
			}
			return _instance;
		}
	}

	//values are set from setSceneVars gameobject and grabbed from projectileShoot script on rock prefab
	public Rigidbody2D rocks;
	public Transform missedText;
	public GameObject slingshotLeft;
	public GameObject slingshotRight;
	public GameObject springAnchor;
	public GameObject throwSound;
	public GameObject tinkSound;
	public GameObject splatSounds;
	public GameObject missedSound;
	public GameObject activeRock;		//rock that player has slung on slingshot
	public float coolDownOnMiss;
	public bool makeRockNow = false;	//ensures rocks do not infinitely instantiate
	public int rockNumber = 0;

	private GameObject[] allRocks;
	private float rockRadius;
	public bool startCoolDown;

	void Awake() {
		_instance = this;
	}

	void Start() {
		rockRadius = rocks.GetComponent<CircleCollider2D> ().radius;
		startCoolDown = false;
	}

	// Update is called once per frame
	void Update () {
		if (makeRockNow) {
			Instantiate (rocks);
			makeRockNow = false;
		}

		allRocks = GameObject.FindGameObjectsWithTag ("rock");

		//checks if rocks have gone off screen
		for (int i = 0; i < allRocks.Length; i++) {
			if ((allRocks [i].transform.position.y > (ScreenVariables.worldHeight + rockRadius)) || (allRocks[i].transform.position.y < (-ScreenVariables.worldHeight - rockRadius))) {
				allRocks [i].SetActive (false);
//				instantiateMissText (allRocks[i]);
//				missedSound.GetComponent<AudioSource> ().Play ();
				resetMaxPlayerStreak ();
				scoreCount.Instance.playerCombo = 0;

				//reset time since player missed and reset active rock's position
//				timeSinceMiss = 0;
//				missedShot = true;
//				lifeManager.Instance.control = false;
//				activeRock.GetComponent<projectileShoot> ().resetRock ();
			}
		}

		if (startCoolDown) {

		}

	}

	void resetMaxPlayerStreak() {
		if (scoreCount.Instance.playerCombo > scoreCount.Instance.maxPlayerStreak) {
			scoreCount.Instance.maxPlayerStreak = scoreCount.Instance.playerCombo;
		}
	}

	void recolorSlingshot() {
		slingshotLeft.GetComponent<SpriteRenderer> ().color = Color.white;
		slingshotRight.GetComponent<SpriteRenderer> ().color = Color.white;
		lifeManager.Instance.control = true;
	}
/*
	void startCoolDown() {
		timeSinceMiss += Time.deltaTime;

		//once cooldown has passed player can again control the slingshot
		if (timeSinceMiss >= coolDownOnMiss) {
			lifeManager.Instance.control = true;
			missedShot = false;
			activeRock.GetComponent<projectileShoot> ().reactivateRock ();
		}
	}

	void instantiateMissText(GameObject missedRock) {
		float textYpos = 0;
		float textXpos = 0;
		if (missedRock.transform.position.y > 0) {
			textYpos = (ScreenVariables.worldHeight - 0.3f);
		} else {
			textYpos = (-ScreenVariables.worldHeight + 0.3f);
		}
		if (missedRock.transform.position.x > 0) {
			textXpos = (missedRock.transform.position.x - 0.5f);
		} else {
			textXpos = (missedRock.transform.position.x + 0.5f);
		}

		Vector3 missTextPos = new Vector3 (textXpos, textYpos);
		Instantiate (missedText, missTextPos, Quaternion.identity);
	}*/


}
