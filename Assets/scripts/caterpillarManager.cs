using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script manages caterpillar spawn frequency changing over time, keeps count of caterpillar deaths and triggers end level message when all caterpillars are inactivated
//removed life from player if player misses caterpillar
public class caterpillarManager : MonoBehaviour {
	public float interbugDistance;	//desired distance between subsequently spawning bugs
	public float finishLine;	//finish line y co-ordinated in world space
	public float endDelay;		//time between final caterpillar being inactivated and level complete message appears
	public float cameraScoreNumShakeDuration;	//camera shake duration for landing of score number

	public Rigidbody2D caterpillars;	//caterpillar prefab
	public int totalCaterpillars;	//total caterpillars for this level
	public int currentSpawn{ get; set; }	//number of the most recent caterpillar
	public bool control{ get; set; }		//whether play can control the slingshot or not
	public bool levelEnd{ get; set; }		//triggers end menu when true
	public int caterpillarsInactivated{ get; set; }		//equals caterpillars killed + caterpillars passed over finish line
	public int caterpillarsKilled{ get; set; }			//total caterpillars player successfully killed

	public Transform completeMessage;	//UI menu displaying end of level scores
	public Transform canvas;
	public GameObject camera;
	public Button pauseButton;

	private float spawnFrequency;	//changes depending on speed of caterpillar to keep interbugDistance constant
	private float timeSinceSpawn;
	private GameObject[] allCaterpillars;
	private float minimunY;			//minimum possible y value for caterpillars before going inactive

	private bool levelOngoing = true;
	private bool setupNotDone = true;	//ensures end menu is not repeatedley instantiated

	private float timeAfterEnd;		//counts time passed since final caterpillar is inactivated

	void Start() {
		caterpillarsInactivated = 0;
		caterpillarsKilled = 0;
		levelEnd = false;
		control = false;
		findAllBugs ();		//finds all caterpillars currently in heirarchy

		currentSpawn = 0;
		timeSinceSpawn = spawnFrequency;	//set so that caterpillar spawns right away
		timeAfterEnd = 0;
	}

	// Update is called once per frame
	void Update () {
		if (control) {
			timeSinceSpawn += Time.deltaTime;
		}

		//instantiate caterpillar when time has elapsed over spawn frequency
		if (timeSinceSpawn > spawnFrequency && levelOngoing) {
			if (currentSpawn < totalCaterpillars) {
				Instantiate (caterpillars);
				timeSinceSpawn = 0;
				currentSpawn += 1;
				calculateSpawnTime ();	//find spawn time for next caterpillar
			} else {
				levelOngoing = false;
			}
		}

		if (control) {	//can only be done when control is true as before this there are no caterpillars to grab minimum y value from
			findMinY ();
		}

		for (int i = 0; i < allCaterpillars.Length; i++) {
			//inactivates caterpillars if they have surpassed finish line
			if (allCaterpillars [i].transform.position.y < minimunY) {
				allCaterpillars [i].gameObject.SetActive (false);
				caterpillarsInactivated += 1;
				GetComponent<lifeManager> ().lifeLost = true;	//triggers removal of one of player's lives

				resetMaxStreak ();

				GetComponent<scoreCount> ().playerCombo = 0;
				findAllBugs ();		//recount caterpillars after inactivation

				//ends level once all caterpillars are inactivated
				if (caterpillarsInactivated == totalCaterpillars) {
					control = false;
					levelEnd = true;
				}
			}
		}


		if (levelEnd && setupNotDone) {
			timeAfterEnd += Time.deltaTime;

			if (timeAfterEnd >= endDelay) {
				setupEnd ();
			}
		}
	}

	void calculateSpawnTime() {
		findAllBugs ();

		//get velocity of most recently spawned caterpillar
		float currVel = -allCaterpillars [allCaterpillars.Length - 1].GetComponent<Rigidbody2D> ().velocity.y;
		//time=distance/speed
		spawnFrequency = interbugDistance / currVel;
	}

	void findAllBugs() {
		allCaterpillars = GameObject.FindGameObjectsWithTag ("caterpillar");
	}

	//displays player scores
	void setupEnd() {
		camera.GetComponent<CameraShake> ().shakeDuration = cameraScoreNumShakeDuration;
		control = false;
		pauseButton.interactable = false;

		resetMaxStreak ();

		Transform levelDone = Instantiate (completeMessage);
		levelDone.transform.SetParent (canvas, false);
		setupNotDone = false;
	}

	void findMinY() {
		float screenHeight = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, 0)).y;
		minimunY = allCaterpillars [0].GetComponent<move> ().yMin;
		minimunY += (screenHeight + finishLine);
	}

	void resetMaxStreak() {
		if (GetComponent<scoreCount> ().playerCombo > GetComponent<scoreCount> ().maxPlayerStreak) {
			GetComponent<scoreCount> ().maxPlayerStreak = GetComponent<scoreCount> ().playerCombo;
		}
	}
}
