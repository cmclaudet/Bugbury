using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileShoot : MonoBehaviour {
	public float velocityMagnitude;
	public Vector3 spawnPosition;

	private GameObject leftSlingshot;
	private GameObject rightSlingshot;
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
		if (mouseDown == false && transform.position.y > leftSlingshot.transform.position.y) {
			GetComponent<SpringJoint2D> ().enabled = false;
			GetComponent<Rigidbody2D> ().velocity = velocityMagnitude * GetComponent<Rigidbody2D> ().velocity.normalized;
			middleLine.enabled = false;

			if (rockGen) {
				manager.GetComponent<rockManager> ().makeRockNow = true;
			}
			rockGen = false;
		}
	}

	void setupLineRenderer() {
		middleLine.sortingLayerName = "Foreground";
		middleLine.SetPosition (0, leftSlingshot.transform.position);
		updateLineRenderer ();
		middleLine.SetPosition (3, rightSlingshot.transform.position);
		middleLine.sortingOrder = 3;
	}

	void setupShootingSpace() {
		screenDim = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, 0));
		float screenWidth = screenDim.x;
		float screenHeight = screenDim.y;
		float boundHeight = (screenHeight + leftSlingshot.transform.position.y) / 2;
		float yCenter = leftSlingshot.transform.position.y - boundHeight;
		shootingSpace = new Bounds (new Vector3 (0, yCenter, 0), new Vector3 (2*screenWidth, 2*boundHeight, 0));
	}

	void updateLineRenderer() {
		Vector3 leftPos = new Vector3 (transform.position.x - radius, transform.position.y, transform.position.z);
		Vector3 rightPos = new Vector3 (transform.position.x + radius, transform.position.y, transform.position.z);

		middleLine.SetPosition (1, leftPos);
		middleLine.SetPosition (2, rightPos);
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.CompareTag("caterpillar") && (transform.position.y > spawnPosition.y)) {
			col.gameObject.SetActive(false);
			this.gameObject.SetActive(false);
		}
	}

}
