using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//instantiates new rock when current rock is launched. Inactivates rocks which have passed off the screen.
//updates player's max player streak
public class rockManager : MonoBehaviour {
	public Rigidbody2D rocks;
	public bool makeRockNow = false;	//ensures rocks do not infinitely instantiate
	private float screenLength;
	private GameObject[] allRocks;

	void Awake() {
		screenLength = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height, 0)).y;
	}

	void Start() {
		Instantiate (rocks);
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
			float rockRadius = allRocks [i].GetComponent<CircleCollider2D> ().radius;
			if ((allRocks [i].transform.position.y > (screenLength + rockRadius)) || (allRocks[i].transform.position.y < (-screenLength - rockRadius))) {
				allRocks [i].SetActive (false);
				resetMaxPlayerStreak ();
				GetComponent<scoreCount> ().playerCombo = 0;
			}
		}
		
	}

	void resetMaxPlayerStreak() {
		if (GetComponent<scoreCount> ().playerCombo > GetComponent<scoreCount> ().maxPlayerStreak) {
			GetComponent<scoreCount> ().maxPlayerStreak = GetComponent<scoreCount> ().playerCombo;
		}
	}
}
