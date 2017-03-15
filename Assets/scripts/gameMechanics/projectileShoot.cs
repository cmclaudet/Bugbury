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
	public Rigidbody2D rockSimulation;	//gameObject which will simulate rock being shot to generate line for pointer
	public int shotsWithPointer;
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
	private GameObject activePointer;

	void Awake() {
		spring = GetComponent<SpringJoint2D> ();
		radius = GetComponent<CircleCollider2D> ().radius;
		middleLine = GetComponent<LineRenderer> ();
	}

	// Use this for initialization
	void Start () {
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

		//if player is able to control and touches the screen trigger rock dragging function
		//rockgen necessary here to ensure rocks do not move back to shooting area once already shot
		if (lifeManager.Instance.control && Input.touchCount > 0 && rockGen) {
			drag ();
		}

		if (drawPointer) {
			GetComponent<getPointerVertices> ().updatePointer ();
		}

		//when player releases their finger after having put it down through drag function, shooting is triggered
		if (Input.touchCount == 0 && fingerDown) {
			drawPointer = false;
			shoot ();
			//line renderer showing pointer is inactivated

			pointer.gameObject.SetActive (false);
			throwSound.Play ();
		}

		//when rock exits the slingshot shooting zone launch is triggered
		if (fingerDown == false && transform.position.y > leftSlingshot.transform.position.y) {
			launch ();
			makeAnotherRock ();
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

	void drag()
	{
		//make rock follow player finger whilst finger is down inside the shooting space
		//ensures spring is disabled in this time and the rock is kinematic
		//drawPointer becomes true as pointer must be drawn when player drags rock back
		Vector3 fingerPos = Camera.main.ScreenToWorldPoint (Input.GetTouch(0).position);
		Vector3 worldPos = new Vector3 (fingerPos.x, fingerPos.y, 0);

		//can only shoot is player drags rock back through shooting space
		if (shootingSpace.Contains (worldPos)) {
			GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
			spring.enabled = false;
			GetComponent<Rigidbody2D> ().isKinematic = true;
			fingerDown = true;
			transform.position = new Vector3 (fingerPos.x, fingerPos.y, 0);

			//only draw pointer for first 6 rocks
			if (rockManager.Instance.rockNumber <= shotsWithPointer) {
				drawPointer = true;
			}
		}
	}

	void shoot() {
		//spring physics is enabled
		spring.enabled = true;
		GetComponent<SpringJoint2D> ().enabled = true;
		GetComponent<Rigidbody2D> ().isKinematic = false;
		fingerDown = false;
	}

	void launch() {
		//once rock has passed over the slingshot position, spring and line renderers are disabled.
		//Velocity is set to magnitude specified above
		GetComponent<SpringJoint2D> ().enabled = false;
		GetComponent<Rigidbody2D> ().velocity = velocityMagnitude * GetComponent<Rigidbody2D> ().velocity.normalized;
		middleLine.enabled = false;
	}

	void makeAnotherRock() {
		if (rockGen) {
			rockManager.Instance.makeRockNow = true;	//changes value in rock manager to instantiate another rock
		}
		rockGen = false;	//set to false to differentiate between launched rocks and not launched rocks
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
			if (caterpillarManager.Instance.caterpillarsInactivated == caterpillarManager.Instance.totalCaterpillars) {
				caterpillarManager.Instance.levelEnd = true;
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

	public void resetRock() {
		transform.position = spawnPosition;
		fingerDown = false;
		updateLineRenderer ();
		drawPointer = false;
		pointer.gameObject.SetActive (false);

		Color faded = new Color (fadedColor, fadedColor, fadedColor);
		setNewColor (faded);
	}

	void setNewColor(Color newColor) {
		leftSlingshot.GetComponent<SpriteRenderer> ().color = newColor;
		rightSlingshot.GetComponent<SpriteRenderer> ().color = newColor;
		GetComponent<SpriteRenderer> ().color = newColor;
		middleLine.startColor = newColor;
		middleLine.endColor = newColor;
	}

	public void reactivateRock() {
		pointer.gameObject.SetActive (true);
		setNewColor (Color.white);
	}

}
