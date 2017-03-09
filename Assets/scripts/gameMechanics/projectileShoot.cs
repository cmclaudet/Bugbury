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

	private AudioSource throwSound;	//sound when rock launches
	private AudioSource splatSound; //sound when caterpillar dies
	private AudioSource tinkSound;	//sound when rock hits a wall
	private GameObject leftSlingshot;
	private GameObject rightSlingshot;
	private GameObject springAnchor;

	private LineRenderer middleLine;
	private SpringJoint2D spring;
	private float radius;
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

		transform.position = spawnPosition;
		setupLineRenderer ();
		setupShootingSpace ();
		GetComponent<Rigidbody2D> ().mass = 0.0001f;
		GetComponent<SpringJoint2D> ().connectedBody = springAnchor.GetComponent<Rigidbody2D>();

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
			updatePointer ();
//			Rigidbody2D invisibleRock = Instantiate(rockSimulation);
//			invisibleRock.GetComponent<drawPointer> ().actualRock = this.gameObject;

		}

		//when player releases their finger after having put it down through drag function, shooting is triggered
		if (Input.touchCount == 0 && fingerDown) {
			drawPointer = false;
			shoot ();
			//line renderer showing pointer is inactivated
			LineRenderer[] renderers = GetComponentsInChildren<LineRenderer> ();
			renderers [1].gameObject.SetActive (false);
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

			//only draw pointer for first 3 rocks
			if (rockManager.Instance.rockNumber < 10) {
				drawPointer = true;
			}
		}
	}

	/* Draws pointer from rock up to top of the screen showing player where their shot will go
	 * Creates raycast to find next collidable object. Adds this object point to the pointer vertices.
	 * Tests whether there is another object to be collided with after this one by finding x position at the top of the screen from following the ray the last object's collision would create
	 * If x position is within the screen width there are no further objects to collide with and a ray is drawn between the last object and the top of the screen
	*/
	void updatePointer() {
		//find direction between rock and spring anchor (pointer direction)
		Vector3 rayDirection = springAnchor.transform.position - transform.position;

		//find colliders that ray intersects with and add initial vertex
		RaycastHit2D[] collidersFound = Physics2D.RaycastAll (transform.position, rayDirection);
		List<Vector3> pointerVertices = new List<Vector3>();
		pointerVertices.Add(collidersFound [0].point);
		bool horizontalReflection = true;

		//if ray finds another collider besides initial rock a second ray must be cast
		if (collidersFound.Length > 1) {
			//add point from last collider found to pointer vertices
			Vector3 lastPoint = collidersFound [1].point;
			horizontalReflection = reflectsInX (collidersFound [1]);
			Vector2 offsets = pointerOffsets (rayDirection, horizontalReflection);
			lastPoint = new Vector3 (lastPoint.x + offsets.x, lastPoint.y + offsets.y);
			pointerVertices.Add (lastPoint);

			//find new colliders from new ray direction
			if (horizontalReflection) {
				rayDirection = new Vector3 (-rayDirection.x, rayDirection.y);
			} else {
				rayDirection = new Vector3 (rayDirection.x, -rayDirection.y);
			}
			RaycastHit2D[] colliders = Physics2D.RaycastAll (lastPoint, new Vector3 (rayDirection.x, rayDirection.y));
			Vector3 nextColliderPoint = setNextColliderPoint (colliders, lastPoint, rayDirection);



			if (horizontalReflection) {
				//if next collider y position is different to last one, need to cast another ray onto another collider
				while (nextColliderPoint.y != lastPoint.y) {
					//add next collider to pointer vertices
					pointerVertices.Add (nextColliderPoint);
					rayDirection = new Vector3 (-rayDirection.x, rayDirection.y);
					colliders = Physics2D.RaycastAll (nextColliderPoint, new Vector3 (rayDirection.x, rayDirection.y));
					lastPoint = nextColliderPoint;

					nextColliderPoint = setNextColliderPoint (colliders, lastPoint, rayDirection);
				}
			} else {
//				pointerVertices.Add (nextColliderPoint);
			}
		}

		if (horizontalReflection) {
			//lastXPos will be inside screen bounds if rock's next destination is off the screen from the top or bottom
			float finalYpos = ScreenVariables.worldHeight;
			if (rayDirection.y < 0) {
				finalYpos = -ScreenVariables.worldHeight;
			}
			float endXPos = pointerXpos (pointerVertices [pointerVertices.Count - 1], rayDirection, finalYpos);
			pointerVertices.Add (new Vector3 (endXPos, finalYpos));
		}
		drawPointerLine (pointerVertices);

	}

	//finds x position at certain y value given the equation of a line
	float pointerXpos(Vector3 point, Vector3 dir, float yPos) {
		Vector3 p1 = point;
		Vector3 p2 = point + dir;
		float m = (p2.y - p1.y) / (p2.x - p1.x);
		float c = p1.y - m * p1.x;
		float XPos = (yPos - c) / m;
		return XPos;
	}

	bool reflectsInX(RaycastHit2D collider) {
		if (collider.normal.y == 0) {
			return true;
		} else {
			return false;
		}
	}

	//find point of next collider
	Vector3 setNextColliderPoint(RaycastHit2D[] colliders, Vector3 lastPoint, Vector3 rayDirection) {
		Vector3 nextColliderPoint;
		switch (colliders.Length) {
		case 0:
			nextColliderPoint = lastPoint;
			break;
		case 1:
			if (colliders [0].point.y != lastPoint.y) {
				bool reflectsHorizontally = reflectsInX (colliders [0]);
				Vector2 offsets = pointerOffsets (rayDirection, reflectsHorizontally);
				nextColliderPoint = colliders [0].point;
				nextColliderPoint = new Vector3 (nextColliderPoint.x + offsets.x, nextColliderPoint.y + offsets.y);
			} else {
				nextColliderPoint = lastPoint;
			}
			break;
		default:
			if (colliders [0].point.y != lastPoint.y) {
				bool reflectsHorizontally = reflectsInX (colliders [0]);
				Vector2 offsets = pointerOffsets (rayDirection, reflectsHorizontally);

				nextColliderPoint = colliders [0].point;
				nextColliderPoint = new Vector3 (nextColliderPoint.x + offsets.x, nextColliderPoint.y + offsets.y);
			} else {
				bool reflectsHorizontally = reflectsInX (colliders [0]);
				Vector2 offsets = pointerOffsets (rayDirection, reflectsHorizontally);

				nextColliderPoint = colliders [1].point;
				nextColliderPoint = new Vector3 (nextColliderPoint.x + offsets.x, nextColliderPoint.y + offsets.y);
			}
			break;
		}
		return nextColliderPoint;
	}



	Vector2 pointerOffsets(Vector3 rayDirection, bool reflectsInX) {
		float deltax = radius;
		float deltay = -radius;
		float tanAngle = Mathf.Abs (rayDirection.x) / rayDirection.y;

		switch (reflectsInX) {
		case true:
			if (rayDirection.x > 0) {
				deltax = -radius;
			}
			if (rayDirection.y > 0) {
				deltay = -radius / tanAngle;
			} else {
				deltay = radius / tanAngle;
			}
			break;
		case false:
			tanAngle = rayDirection.y / Mathf.Abs (rayDirection.x);
			deltax = radius / tanAngle;
			if (rayDirection.x > 0) {
				deltax = -radius / tanAngle;
			}
			if (rayDirection.y < 0) {
				deltay = radius;
			}
			break;
		}
		return new Vector2 (deltax, deltay);
	}

	//adds points on pointer to line renderer so they can be drawn out
	void drawPointerLine(List<Vector3> pointerVertices) {
		//set pointer indices equal to points at which ray intersects walls
		LineRenderer[] allRenderers = GetComponentsInChildren<LineRenderer> ();
		LineRenderer pointer;
		foreach (LineRenderer renderer in allRenderers) {
			if (renderer.gameObject != this.gameObject) {
				pointer = renderer;
				pointer.numPositions = pointerVertices.Count;
				pointer.SetPositions (pointerVertices.ToArray());

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
		} else if (col.gameObject.CompareTag ("wall")) {
			tinkSound.pitch = Random.Range (1.2f, 1.6f);
			tinkSound.Play ();
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
}
