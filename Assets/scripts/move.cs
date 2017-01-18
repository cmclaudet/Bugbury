using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {
	public int laneNumber;	//must be EVEN
	public float minVelocity;
	public float maxVelocity;
	public float finishLine;

	public float screenHeight { get; set; }
	private float screenWidth;

	private float yOffset;
	private float ySize;
	private float yheadPos;

	public float yMin { get; set; }

	private GameObject manager;
	private int totalCaterpillars;

	// Use this for initialization
	void Awake () {
		manager = GameObject.Find ("game manager");
		totalCaterpillars = manager.GetComponent<caterpillarManager> ().totalCaterpillars;

		setupPosition ();
		setIncreasedSpeed ();
		yheadPos = transform.TransformPoint (new Vector3 (0, yOffset - ySize, 0)).y - screenHeight;
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.y + yheadPos < finishLine) {
			GetComponent<BoxCollider2D> ().isTrigger = true;
		}
	}

	void setupPosition() {
		screenHeight = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, 0)).y;
		screenWidth = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, 0)).x;
		yOffset = GetComponent<BoxCollider2D> ().offset.y;
		ySize = GetComponent<BoxCollider2D> ().size.y/2;

		yMin = transform.TransformPoint (new Vector3 (0, ySize - yOffset, 0)).y;
		yMin = - screenHeight - yMin;

		float yWorldDim = transform.TransformPoint (new Vector3 (0, yOffset + ySize, 0)).y;
		int lane = Random.Range (-laneNumber/2, laneNumber/2);
		float laneWidth = 2*screenWidth / laneNumber;
		float xPos = (lane + 0.5f) * laneWidth; 
		float yPos = screenHeight + yWorldDim;

		transform.position = new Vector3 (xPos, yPos, 0);
	}

	void setSpeed() {
		GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -minVelocity, 0);
	}

	void setIncreasedSpeed() {
		float deltaVelocity = (maxVelocity - minVelocity) / totalCaterpillars;
		int currentCaterpillar = manager.GetComponent<caterpillarManager> ().currentSpawn;
		float speed = minVelocity + currentCaterpillar * deltaVelocity;
		GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -speed, 0);
	}
}
