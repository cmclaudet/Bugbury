using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileShoot : MonoBehaviour {
	public float velocityMagnitude;
	public Vector3 spawnPosition;

	private LineRenderer leftCatapultLine;
	private LineRenderer rightCatapultLine;
	private GameObject springAnchor;
	private GameObject manager;

	private LineRenderer middleLine;
	private SpringJoint2D spring;
	private float radius;
	private bool mouseDown = false;

	private Bounds shootingSpace;
	private Vector3 screenDim;

	private bool rockGen = true;

	void Awake() {
		spring = GetComponent<SpringJoint2D> ();
		radius = GetComponent<CircleCollider2D> ().radius;
		middleLine = GetComponent<LineRenderer> ();

		GameObject leftSlingshot = GameObject.Find ("slingshot left");
		GameObject rightSlingshot = GameObject.Find ("slingshot right");
		leftCatapultLine = leftSlingshot.GetComponent<LineRenderer> ();
		rightCatapultLine = rightSlingshot.GetComponent<LineRenderer> ();
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
		if (Input.GetMouseButton (0) && rockGen) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector3 actualPos = new Vector3 (mousePos.x, mousePos.y, 0);

			if (shootingSpace.Contains(actualPos)) {
				GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
				spring.enabled = false;
				GetComponent<Rigidbody2D> ().isKinematic = true;
				mouseDown = true;
				transform.position = new Vector3(mousePos.x, mousePos.y, 0);
			}
		}
	}

	void shoot() {
		if (Input.GetMouseButtonUp (0) && mouseDown) {
			spring.enabled = true;
			GetComponent<SpringJoint2D>().enabled = true;
			GetComponent<Rigidbody2D> ().isKinematic = false;
			mouseDown = false;
		}
	}

	void launch() {
		if (mouseDown == false && transform.position.y > leftCatapultLine.transform.position.y) {
			GetComponent<SpringJoint2D> ().enabled = false;
			GetComponent<Rigidbody2D> ().velocity = velocityMagnitude * GetComponent<Rigidbody2D> ().velocity.normalized;
			leftCatapultLine.SetPosition (1, rightCatapultLine.transform.position);
			middleLine.enabled = false;
			rightCatapultLine.enabled = false;
			leftCatapultLine.enabled = false;

			if (rockGen) {
				manager.GetComponent<rockManager> ().makeRockNow = true;
			}
			rockGen = false;
		}
	}

	void setupLineRenderer() {
		leftCatapultLine.SetPosition (0, leftCatapultLine.transform.position);
		rightCatapultLine.SetPosition (0, rightCatapultLine.transform.position);

		leftCatapultLine.sortingLayerName = "Foreground";
		rightCatapultLine.sortingLayerName = "Foreground";
		middleLine.sortingLayerName = "Foreground";

		leftCatapultLine.sortingOrder = 1;
		rightCatapultLine.sortingOrder = 1;
		middleLine.sortingOrder = 3;
	}

	void setupShootingSpace() {
		screenDim = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, 0));
		float screenWidth = screenDim.x;
		float screenHeight = screenDim.y;
		float boundHeight = (screenHeight + leftCatapultLine.GetPosition (0).y) / 2;
		float yCenter = leftCatapultLine.GetPosition (0).y - boundHeight;
		shootingSpace = new Bounds (new Vector3 (0, yCenter, 0), new Vector3 (2*screenWidth, 2*boundHeight, 0));
	}

	void updateLineRenderer() {
		Vector3 leftPos = new Vector3 (transform.position.x - radius, transform.position.y, transform.position.z);
		Vector3 rightPos = new Vector3 (transform.position.x + radius, transform.position.y, transform.position.z);

		leftCatapultLine.SetPosition (1, leftPos);
		rightCatapultLine.SetPosition (1, rightPos);

		middleLine.SetPosition (0, leftPos);
		middleLine.SetPosition (1, rightPos);
	}


}
