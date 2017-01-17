using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockManager : MonoBehaviour {
	public Rigidbody2D rocks;
	public bool makeRockNow = false;
	private float screenLength;
	private GameObject[] allRocks;

	void Awake() {
		screenLength = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height, 0)).y;
	}

	// Update is called once per frame
	void Update () {
//		Debug.Log (makeRockNow);
		if (makeRockNow) {
			Instantiate (rocks);
			makeRockNow = false;
		}

		allRocks = GameObject.FindGameObjectsWithTag ("rock");

		for (int i = 0; i < allRocks.Length; i++) {
			float rockRadius = allRocks [i].GetComponent<CircleCollider2D> ().radius;
			if ((allRocks [i].transform.position.y > (screenLength + rockRadius)) || (allRocks[i].transform.position.y < (-screenLength - rockRadius))) {
				allRocks [i].SetActive (false);
				GetComponent<scoreCount> ().playerCombo = 0;
			}
		}
		
	}
}
