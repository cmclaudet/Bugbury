using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*defines slingshot shooting mechanics including dragging projectile on hold, launching on release and instantiation of new projectiles.
manages events which occur due to caterpillar projectile collision including inactivation of the gameobjects,
updating player score and combo and displaying bonuses on screen */
public class projectileShoot : MonoBehaviour {
	public float velocityMagnitude;	//insert desired speed for rocks
	public Vector3 spawnPosition;	//rock spawn position
	public GameObject splatters;	//blood splatter of caterpillars
	public int shotsWithPointer;	//number of initial shots with pointer displayed
	public float fadedColor;		//color slingshot fades to when not useable

	private AudioSource throwSound;	//sound when rock launches
	private AudioSource splatSound; //sound when caterpillar dies
	private AudioSource tinkSound;	//sound when rock hits a wall
	private GameObject leftSlingshot;
	private GameObject rightSlingshot;
	private GameObject springAnchor;

	private LineRenderer middleLine;
	private LineRenderer pointer;
	private SpringJoint2D spring;
	public float radius{ get; set; }
	private bool fingerDown = false;	//becomes true when screen touched in active shooting area

	private Bounds shootingSpace;		//space player is allowed to draw slingshot back into

	private bool rockGen = true;
	private bool drawPointer = false;	//becomes true when pointer needs to be drawn (when player drags rock back)
	private GameObject[] allCaterpillars;
	public float catMinYvalue;	//minimum y value caterpillar will get down to before being shot
	public Vector3[] rockLaunchPositions;	//positions necessary for bot to launch rocks from for each lane

	void Awake() {
		spring = GetComponent<SpringJoint2D> ();
		radius = GetComponent<CircleCollider2D> ().radius;
		middleLine = GetComponent<LineRenderer> ();
	}

	// Use this for initialization
	void Start () {
		catMinYvalue = 3.5f;

		setupSounds();

		//find relevant gameobjects stored in rock manager singleton
		leftSlingshot = rockManager.Instance.slingshotLeft;
		rightSlingshot = rockManager.Instance.slingshotRight;
		springAnchor = rockManager.Instance.springAnchor;
		rockManager.Instance.rockNumber += 1;
		rockManager.Instance.activeRock = gameObject;

		transform.position = spawnPosition;
		setupLineRenderer ();
		setupShootingSpace ();
		GetComponent<Rigidbody2D> ().mass = 0.0001f;
		GetComponent<SpringJoint2D> ().connectedBody = springAnchor.GetComponent<Rigidbody2D>();
		LineRenderer[] renderers = GetComponentsInChildren<LineRenderer> ();
		pointer = renderers [1];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		updateLineRenderer ();
		allCaterpillars = GameObject.FindGameObjectsWithTag("caterpillar");


		foreach (GameObject caterpillar in allCaterpillars) {
			if (caterpillar.transform.position.y < catMinYvalue && rockGen && lifeManager.Instance.control) {
				int totalLanes = caterpillar.GetComponent<move>().laneNumber;
				int caterpillarLane = caterpillar.GetComponent<move>().chosenLane;
				Vector3 rockLaunchPos = rockLaunchPositions[caterpillarLane + totalLanes/2];
				launchRock(rockLaunchPos);
			}
		}

		//when rock exits the slingshot shooting zone launch is triggered
		if (transform.position.y > leftSlingshot.transform.position.y && rockGen) {
			launch ();
			fadeSlingshot ();
			rockGen = false;
			rockManager.Instance.startCoolDown = true;
		}

	}

	void setupSounds() {
		//vary pitch and source of sounds to make them less repetitive
		//3 different splat sounds and the throw sound are stored in rock manager singleton
		throwSound = rockManager.Instance.throwSound.GetComponent<AudioSource> ();
		throwSound.pitch = Random.Range (0.8f, 1.2f);

		tinkSound = rockManager.Instance.tinkSound.GetComponent<AudioSource> ();

		GameObject splatSoundsObj = rockManager.Instance.splatSounds;
		AudioSource[] splatSounds = splatSoundsObj.GetComponentsInChildren<AudioSource> ();
		splatSound = splatSounds [Random.Range (0, splatSounds.Length - 1)];
		splatSound.pitch = Random.Range (0.8f, 1.2f);

	}

//	void setRock

	void launchRock(Vector3 launchPos) {
		//move rock back and disable spring
		GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
		spring.enabled = false;
		GetComponent<Rigidbody2D> ().isKinematic = true;
		transform.position = launchPos;

		//disable spring to let rock go
		spring.enabled = true;
		GetComponent<SpringJoint2D> ().enabled = true;
		GetComponent<Rigidbody2D> ().isKinematic = false;
		lifeManager.Instance.control = false;
	}

	void launch() {
		//once rock has passed over the slingshot position, spring and line renderers are disabled.
		//Velocity is set to magnitude specified above
		GetComponent<SpringJoint2D> ().enabled = false;
		GetComponent<Rigidbody2D> ().velocity = velocityMagnitude * GetComponent<Rigidbody2D> ().velocity.normalized;
		middleLine.enabled = false;
	}

	//set line renderer's 4 points
	void setupLineRenderer() {
		middleLine.sortingLayerName = "Foreground";
		middleLine.SetPosition (0, leftSlingshot.transform.position);
		updateLineRenderer ();
		middleLine.SetPosition (3, rightSlingshot.transform.position);
		middleLine.sortingOrder = 3;
	}

	//Define area which player can shoot from
	//Defined from position of slingshot
	void setupShootingSpace() {
		float boundHeight = (ScreenVariables.worldHeight + leftSlingshot.transform.position.y) / 2;
		float yCenter = leftSlingshot.transform.position.y - boundHeight;
		shootingSpace = new Bounds (new Vector3 (0, yCenter, 0), new Vector3 (2*ScreenVariables.worldWidth, 2*boundHeight, 0));
	}

	//constantly updates 2nd and 3rd line renderer point to be attached to rock edge
	void updateLineRenderer() {
		Vector3 leftPos = new Vector3 (transform.position.x - radius, transform.position.y, transform.position.z);
		Vector3 rightPos = new Vector3 (transform.position.x + radius, transform.position.y, transform.position.z);

		middleLine.SetPosition (1, leftPos);
		middleLine.SetPosition (2, rightPos);
	}


	//on collision with caterpillar rock is inactivated, blood splatter is placed and player score + streak number updated
	//rock is inactivated
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag ("caterpillar") && (transform.position.y > spawnPosition.y)) {
			splatSound.Play ();
			col.gameObject.GetComponent<showBonuses> ().dead = true;

			GameObject splatter = Instantiate (splatters);
			splatter.transform.position = col.transform.position;

			caterpillarManager.Instance.caterpillarsInactivated += 1;	//caterpillars inactivated includes both killed caterpillars and those which pass off the screen
			caterpillarManager.Instance.caterpillarsKilled += 1;

			updateScores (col);
			this.gameObject.SetActive (false);
			//if player kills final caterpillar level ends
			if (!caterpillarManager.Instance.endlessLevel) {
				if (caterpillarManager.Instance.caterpillarsInactivated == caterpillarManager.Instance.totalCaterpillars) {
					caterpillarManager.Instance.levelEnd = true;
				}
			}
		//if rock hits wall make tink sound
		} 
	}

	void updateScores(Collider2D col) {
		//add 1 to player streak
		scoreCount.Instance.playerCombo += 1;
		int currentCombo = scoreCount.Instance.playerCombo;

		//update manager if the shot is far, ie if user hits caterpillar over the 70% point
		updateIfFarShot(col);
		bool farShot = scoreCount.Instance.far;

		//update score
		int newScore = getNewScore(currentCombo, farShot);
		scoreCount.Instance.changeScore (newScore);
	}

	void updateIfFarShot(Collider2D col) {
		float arenaFarpoint = getFarPoint (col);

		if (col.transform.position.y > arenaFarpoint) {
			scoreCount.Instance.farShots += 1;		//add 1 to total far shots
			scoreCount.Instance.far = true;
		} else {
			scoreCount.Instance.far = false;
		}
	}

	//get point 70% of the way up from the finish line. Here upwards it will be considered a far shot
	float getFarPoint(Collider2D col) {
		float finishLine = caterpillarManager.Instance.finishLine;
		float farPoint = finishLine + (ScreenVariables.worldHeight - finishLine) * 0.7f;
		return farPoint;
	}

	//new score is current streak number + far shot bonus if applicable
	int getNewScore(int currentCombo, bool farShot) {
		int newScore = currentCombo;
		if (farShot) {
			newScore += scoreCount.Instance.farShotBonus;
		}
		return newScore;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.CompareTag ("wall")) {
			tinkSound.pitch = Random.Range (1.2f, 1.6f);
			tinkSound.Play ();
		}
	}

	void fadeSlingshot() {
		Color faded = new Color (fadedColor, fadedColor, fadedColor);
		leftSlingshot.GetComponent<SpriteRenderer> ().color = faded;
		rightSlingshot.GetComponent<SpriteRenderer> ().color = faded;
	}

}
