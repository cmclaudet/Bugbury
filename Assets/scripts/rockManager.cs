using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//instantiates new rock when current rock is launched. Inactivates rocks which have passed off the screen.
//updates player's max player streak
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

	public Rigidbody2D rocks;
	public bool makeRockNow = false;	//ensures rocks do not infinitely instantiate

//	private ScreenVariables screen;		//grab screen dimensions
	private GameObject[] allRocks;
	private float rockRadius;

	void Awake() {
		_instance = this;
//		screen = new ScreenVariables ();
	}

	void Start() {
		rockRadius = rocks.GetComponent<CircleCollider2D> ().radius;
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
			}
		}
		
	}

	void resetMaxPlayerStreak() {
		if (scoreCount.Instance.playerCombo > scoreCount.Instance.maxPlayerStreak) {
			scoreCount.Instance.maxPlayerStreak = scoreCount.Instance.playerCombo;
		}
	}
}
