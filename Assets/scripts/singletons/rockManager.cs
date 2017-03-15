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
	public GameObject slingshotLeft;
	public GameObject slingshotRight;
	public GameObject springAnchor;
	public GameObject throwSound;
	public GameObject tinkSound;
	public GameObject splatSounds;
	public GameObject activeRock;		//rock that player has slung on slingshot
	public float coolDownOnMiss;
	public bool makeRockNow = false;	//ensures rocks do not infinitely instantiate
	public int rockNumber = 0;

	private GameObject[] allRocks;
	private float rockRadius;
	private float timeSinceMiss;
	private bool missedShot;

	void Awake() {
		_instance = this;
	}

	void Start() {
		rockRadius = rocks.GetComponent<CircleCollider2D> ().radius;
		timeSinceMiss = 0;
		missedShot = false;
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
				resetMaxPlayerStreak ();
				scoreCount.Instance.playerCombo = 0;

				timeSinceMiss = 0;
				missedShot = true;
				lifeManager.Instance.control = false;
			}
		}

		if (missedShot) {
			startCoolDown ();
		}
	}

	void resetMaxPlayerStreak() {
		if (scoreCount.Instance.playerCombo > scoreCount.Instance.maxPlayerStreak) {
			scoreCount.Instance.maxPlayerStreak = scoreCount.Instance.playerCombo;
		}
	}

	void startCoolDown() {
		timeSinceMiss += Time.deltaTime;

		if (timeSinceMiss >= coolDownOnMiss) {
			lifeManager.Instance.control = true;
			missedShot = false;
		}
	}
}
