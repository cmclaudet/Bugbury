using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//defines slingshot shooting mechanics including dragging projectile on hold, launching on release and instantiation of new projectiles.
//manages events which occur due to caterpillar projectile collision including inactivation of the gameobjects, updating player score and combo and displaying bonuses on screen
public class projectileShoot : MonoBehaviour {
	public float velocityMagnitude;	//insert desired speed for rocks
	public Vector3 spawnPosition;	//rock spawn position
	public GameObject splatters;	//blood splatter of caterpillars

	private AudioSource throwSound;	//sound when rock launches
	private AudioSource splatSound; //sound when caterpillar dies
	private GameObject leftSlingshot;
	private GameObject rightSlingshot;
	private GameObject springAnchor;
	private GameObject manager;

	private LineRenderer middleLine;
	private SpringJoint2D spring;
	private float radius;
	private bool fingerDown = false;	//becomes true when screen touched in active shooting area

	private Bounds shootingSpace;
	private Vector3 screenDim;

	private bool rockGen = true;

	void Awake() {
		throwSound = GameObject.Find ("throw").GetComponent<AudioSource> ();
		splatSound = GameObject.Find ("splat").GetComponent<AudioSource> ();
		spring = GetComponent<SpringJoint2D> ();
		radius = GetComponent<CircleCollider2D> ().radius;
		middleLine = GetComponent<LineRenderer> ();

		leftSlingshot = GameObject.Find ("slingshot left");
		rightSlingshot = GameObject.Find ("slingshot right");
		manager = GameObject.Find ("game manager");

		springAnchor = GameObject.Find ("spring anchor");
	}

	// Use this for initialization
	void Start () {
		transform.position = spawnPosition;
		setupLineRenderer ();
		setupShootingSpace ();
		GetComponent<Rigidbody2D> ().mass = 0.0001f;
		GetComponent<SpringJoint2D> ().connectedBody = springAnchor.GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
		updateLineRenderer ();
		drag ();
		shoot ();
		launch ();
	}

	void drag()
	{
		//make rock follow player finger whilst finger is down inside the shooting space
		//ensures spring is disabled in this time and the rock is kinematic
		if (manager.GetComponent<caterpillarManager>().control) {
			if (Input.touchCount > 0 && rockGen) {	//rockgen necessary here to ensure rocks do not move back to shooting area once already shot
				Vector3 fingerPos = Camera.main.ScreenToWorldPoint (Input.GetTouch(0).position);
				Vector3 worldPos = new Vector3 (fingerPos.x, fingerPos.y, 0);

				if (shootingSpace.Contains (worldPos)) {
					GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
					spring.enabled = false;
					GetComponent<Rigidbody2D> ().isKinematic = true;
					fingerDown = true;
					transform.position = new Vector3 (fingerPos.x, fingerPos.y, 0);
				}
			}
		}
	}

	void shoot() {
		//when player releases finger after touching active shooting area, spring physics is enabled
		//sound effect
		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Ended && fingerDown) {
				throwSound.Play ();
				spring.enabled = true;
				GetComponent<SpringJoint2D> ().enabled = true;
				GetComponent<Rigidbody2D> ().isKinematic = false;
				fingerDown = false;
			}
		}
	}

	void launch() {
		//once rock has passed over the slingshot position, spring and line renderers are disabled.
		//Velocity is set to magnitude specified above
		if (fingerDown == false && transform.position.y > leftSlingshot.transform.position.y) {
			GetComponent<SpringJoint2D> ().enabled = false;
			GetComponent<Rigidbody2D> ().velocity = velocityMagnitude * GetComponent<Rigidbody2D> ().velocity.normalized;
			middleLine.enabled = false;

			if (rockGen) {
				manager.GetComponent<rockManager> ().makeRockNow = true;	//changes value in rock manager to instantiate another rock
			}
			rockGen = false;	//set to false to differentiate between launched rocks and not launched rocks
		}
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
		screenDim = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, 0));
		float screenWidth = screenDim.x;
		float screenHeight = screenDim.y;
		float boundHeight = (screenHeight + leftSlingshot.transform.position.y) / 2;
		float yCenter = leftSlingshot.transform.position.y - boundHeight;
		shootingSpace = new Bounds (new Vector3 (0, yCenter, 0), new Vector3 (2*screenWidth, 2*boundHeight, 0));
	}

	//constantly updates 2nd and 3rd line renderer point to be attached to rock edge
	void updateLineRenderer() {
		Vector3 leftPos = new Vector3 (transform.position.x - radius, transform.position.y, transform.position.z);
		Vector3 rightPos = new Vector3 (transform.position.x + radius, transform.position.y, transform.position.z);

		middleLine.SetPosition (1, leftPos);
		middleLine.SetPosition (2, rightPos);
	}

	//on collision with caterpillar both bodies are inactivated, blood splatter is placed and player score + combo updated
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.CompareTag("caterpillar") && (transform.position.y > spawnPosition.y)) {
			splatSound.Play ();
			col.gameObject.GetComponent<showBonuses> ().dead = true;

			GameObject splatter = Instantiate (splatters);
			splatter.transform.position = col.transform.position;

			manager.GetComponent<caterpillarManager> ().caterpillarsInactivated += 1;	//caterpillars inactivated includes both killed caterpillars and those which pass off the screen
			manager.GetComponent<caterpillarManager> ().caterpillarsKilled += 1;		//to inform player of total number of caterpillars killed

			updateScores (col);

			//if player kills final caterpillar level ends
			if (manager.GetComponent<caterpillarManager> ().caterpillarsInactivated == manager.GetComponent<caterpillarManager> ().totalCaterpillars) {
				manager.GetComponent<caterpillarManager> ().levelEnd = true;
			}
		}
	}

	void updateScores(Collision2D col) {
		//add 1 to player streak
		manager.GetComponent<scoreCount> ().playerCombo += 1;
		int currentCombo = manager.GetComponent<scoreCount> ().playerCombo;

		//update manager if the shot is far, ie if user hits caterpillar over the mid way point
		updateIfFarShot(col);
		bool farShot = manager.GetComponent<scoreCount> ().far;

		//update score
		int newScore = getNewScore(currentCombo, farShot);
		manager.GetComponent<scoreCount> ().changeScore (newScore);

		setInactive (bonusCheck (newScore), col);
	}

	void updateIfFarShot(Collision2D col) {
		float arenaMidpoint = getMidPoint (col);

		if (col.transform.position.y > arenaMidpoint) {
			manager.GetComponent<scoreCount> ().farShots += 1;		//add 1 to total far shots
			manager.GetComponent<scoreCount> ().far = true;
		} else {
			manager.GetComponent<scoreCount> ().far = false;
		}
	}

	float getMidPoint(Collision2D col) {
		float screenHeight = col.gameObject.GetComponent<move> ().screenHeight;
		float finishLine = manager.GetComponent<caterpillarManager>().finishLine;
		//mid-point of actual shooting space
		float arenaMidpoint = (screenHeight + finishLine) / 2;
		return arenaMidpoint;
	}

	int getNewScore(int currentCombo, bool farShot) {
		int newScore = currentCombo;
		if (farShot) {
			newScore += 2;
		}
		return newScore;
	}

	//set caterpillar inactive now if there is no bonus score. If not caterpillar is set inactive once bonus text fades.
	//set projectile game object to false
	void setInactive(bool bonus, Collision2D col) {
		if (bonus == false) {
			col.gameObject.SetActive (false);
		}
		this.gameObject.SetActive(false);
	}

	bool bonusCheck(int scoreMultiplier) {
		if (scoreMultiplier > 1) {
			return true;
		} else {
			return false;
		}
	}

}
