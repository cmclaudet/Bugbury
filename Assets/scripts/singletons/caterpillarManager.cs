using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script manages caterpillar spawn frequency changing over time, keeps count of caterpillar deaths and triggers end level message when all caterpillars are inactivated
//takes minimum and maximum caterpillar speeds as arguments. caterpillar speed increases linearly over spawn number up to total caterpillar number
//removed life from player if player misses caterpillar
public class caterpillarManager : MonoBehaviour {

	private static caterpillarManager _instance;

	public static caterpillarManager Instance {
		get {
			if (_instance == null) {
				GameObject go = new GameObject ("caterpillarManager");
				go.AddComponent<caterpillarManager> ();
			}
			return _instance;
		}
	}

	public float interbugDistance = 3;	//desired distance between subsequently spawning bugs
	public int lanes = 6;			//number of lanes caterpillars spawn on
	public float finishLine = -3.0f;	//finish line y co-ordinated in world space
	public float endDelay = 2.0f;		//time between final caterpillar being inactivated and level complete message appears
	public float cameraScoreNumShakeDuration = 0.5f;	//camera shake duration for landing of score number

	//values are set in the inspector and set to this class as they are scene specific
	public float minVel { get; set; }		//minimum possible velocity for caterpillar
	public float maxVel { get; set; }		//max possible velocity for caterpillar
	public float velocityIncrement{get;set;}	//used for endless mode. Amount caterpillar speed increases by each spawn.
	public int totalCaterpillars{ get; set; }	//total caterpillars for this level
	public Rigidbody2D caterpillars{ get; set; }	//caterpillar prefab

	public int currentSpawn{ get; set; }	//number of the most recent caterpillar
	public bool levelEnd{ get; set; }		//triggers end menu when true
	public bool levelStart{get;set;}
	public int caterpillarsInactivated{ get; set; }		//equals caterpillars killed + caterpillars passed over finish line
	public int caterpillarsKilled{ get; set; }			//total caterpillars player successfully killed
	private float minimunY;			//minimum possible y value for caterpillars before going inactive

	public GameObject levelComplete{ get; set; }

	private float spawnFrequency;	//changes depending on speed of caterpillar to keep interbugDistance constant
	private float timeSinceSpawn;
	private GameObject[] allCaterpillars;

	private bool levelOngoing = true;
	private bool setupNotDone = true;	//ensures end menu is not repeatedley instantiated

	private float timeAfterEnd;		//counts time passed since final caterpillar is inactivated

	public bool endlessLevel;

	void Awake() {
		_instance = this;
	}

	void Start() {
		currentSpawn = 0;
		levelEnd = false;
		levelStart = false;
		caterpillarsInactivated = 0;
		caterpillarsKilled = 0;
		findAllBugs ();		//finds all caterpillars currently in heirarchy
		minimunY = findMinY();

		timeSinceSpawn = spawnFrequency;	//set so that caterpillar spawns right away
		timeAfterEnd = 0;
	}

	// Update is called once per frame
	void Update () {
		if (levelStart) {
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


		for (int i = 0; i < allCaterpillars.Length; i++) {
			//inactivates caterpillars if they have surpassed finish line
			if (allCaterpillars [i].transform.position.y < minimunY) {
				allCaterpillars [i].gameObject.SetActive (false);
				caterpillarsInactivated += 1;
				lifeManager.Instance.lifeLost = true;	//triggers removal of one of player's lives

				//resets player combo and checks if max streak has been overtaken
				resetMaxStreak ();
				scoreCount.Instance.playerCombo = 0;
				findAllBugs ();		//recount caterpillars after inactivation

				//ends level once all caterpillars are inactivated
				if (!endlessLevel) {
					if (caterpillarsInactivated == totalCaterpillars) {
						lifeManager.Instance.control = false;
						levelEnd = true;
					}
				}
			}
		}


		if (levelEnd && setupNotDone) {
			timeAfterEnd += Time.deltaTime;

			if (timeAfterEnd >= endDelay) {
				levelComplete.GetComponent<triggerLevelComplete> ().setupEnd ();
				setupNotDone = false;
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

	float findMinY() {
		float yMin = finishLine - 0.5f;
		return yMin;
	}

	public void resetMaxStreak() {
		if (scoreCount.Instance.playerCombo > scoreCount.Instance.maxPlayerStreak) {
			scoreCount.Instance.maxPlayerStreak = scoreCount.Instance.playerCombo;
		}
	}


}
