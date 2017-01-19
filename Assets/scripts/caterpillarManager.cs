using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caterpillarManager : MonoBehaviour {
	public float interbugDistance;

	public Rigidbody2D caterpillars;
	public int totalCaterpillars;
	public int currentSpawn{ get; set; }
	public bool control{ get; set; }
	public bool levelEnd{ get; set; }
	public int caterpillarsKilled{ get; set; }

	public Transform completeMessage;
	public Transform canvas;

	private float spawnFrequency;
	private float timeSinceSpawn;
	private GameObject[] allCaterpillars;
	private float minimunY;

	private bool levelOngoing = true;
	private bool setupNotDone = true;

	void Start() {
		caterpillarsKilled = 0;
		levelEnd = false;
		control = true;
		findAllBugs ();
		float screenHeight = allCaterpillars [0].GetComponent<move> ().screenHeight;
		float finishLine = allCaterpillars [0].GetComponent<move> ().finishLine;
		float currentVel = allCaterpillars [0].GetComponent<move> ().minVelocity;

		spawnFrequency = interbugDistance / currentVel;
		minimunY = allCaterpillars [0].GetComponent<move> ().yMin;
		minimunY += (screenHeight + finishLine);
		currentSpawn = 1;
	}

	// Update is called once per frame
	void Update () {
		timeSinceSpawn += Time.deltaTime;
		findAllBugs ();

		if (timeSinceSpawn > spawnFrequency && levelOngoing) {
			if (currentSpawn < totalCaterpillars) {
				Instantiate (caterpillars);
				timeSinceSpawn = 0;
				currentSpawn += 1;
				calculateSpawnTime ();
			} else {
				levelOngoing = false;
			}
		}

		for (int i = 0; i < allCaterpillars.Length; i++) {
			if (allCaterpillars [i].transform.position.y < minimunY) {
				allCaterpillars [i].gameObject.SetActive (false);
				caterpillarsKilled += 1;
				GetComponent<scoreCount> ().playerCombo = 0;

				if (caterpillarsKilled == totalCaterpillars) {
					levelEnd = true;
				}
			}
		}

		if (levelEnd && setupNotDone) {
			setupEnd ();
		}
	}

	void calculateSpawnTime() {
		findAllBugs ();

		float currVel = -allCaterpillars [allCaterpillars.Length - 1].GetComponent<Rigidbody2D> ().velocity.y;
		spawnFrequency = interbugDistance / currVel;
	}

	void findAllBugs() {
		allCaterpillars = GameObject.FindGameObjectsWithTag ("caterpillar");
	}

	void setupEnd() {
		control = false;
		Transform levelDone = Instantiate (completeMessage);
		levelDone.transform.SetParent (canvas, false);
		setupNotDone = false;
	}
}
