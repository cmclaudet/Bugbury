using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caterpillarManager : MonoBehaviour {
	public float spawnFrequency;
	public Rigidbody2D caterpillars;
	public int totalCaterpillars;
	public int currentSpawn{ get; set; }

	private float timeSinceSpawn;
	private GameObject[] allCaterpillars;
	private float minimunY;

	private bool levelOngoing = true;

	void Start() {
		allCaterpillars = GameObject.FindGameObjectsWithTag ("caterpillar");
		float screenHeight = allCaterpillars [0].GetComponent<move> ().screenHeight;
		float finishLine = allCaterpillars [0].GetComponent<move> ().finishLine;
		minimunY = allCaterpillars [0].GetComponent<move> ().yMin;
		minimunY += (screenHeight + finishLine);
		currentSpawn = 1;
	}

	// Update is called once per frame
	void Update () {
		timeSinceSpawn += Time.deltaTime;
		allCaterpillars = GameObject.FindGameObjectsWithTag ("caterpillar");

		if (timeSinceSpawn > spawnFrequency && levelOngoing) {
			if (currentSpawn < totalCaterpillars) {
				Instantiate (caterpillars);
				timeSinceSpawn = 0;
				currentSpawn += 1;
			} else {
				levelOngoing = false;
			}
		}

		for (int i = 0; i < allCaterpillars.Length; i++) {
			if (allCaterpillars [i].transform.position.y < minimunY) {
				allCaterpillars [i].gameObject.SetActive (false);
				GetComponent<scoreCount> ().playerCombo = 0;
			}
		}
	}
}
