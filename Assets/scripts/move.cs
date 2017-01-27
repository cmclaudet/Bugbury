using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sets caterpillar position onto one of six lanes randomely and finds y co-ordinate for caterpillar spawn point based on screen height and caterpillar body lenght
//sets caterpillar speed based on spawn number and min max speeds set in inspector

public class move : MonoBehaviour {
	public int laneNumber;	//must be EVEN
	public float minVelocity;
	public float maxVelocity;

	public float screenHeight{ get; set; }
	private float screenWidth;

	private float yOffset;	//size of caterpillar
	private float ySize;	//offset of caterpillar
	private float yheadPos;	//position of caterpillar head from body in world co-ordinates

	public float yMin { get; set; }	//minimum y value for caterpillar to be active. must be calculated here as makes use of caterpillar body length values.

	private GameObject manager;
	private int totalCaterpillars;
	private float finishLine;

	// Use this for initialization
	void Awake () {
		manager = GameObject.Find ("game manager");
		totalCaterpillars = manager.GetComponent<caterpillarManager> ().totalCaterpillars;
		finishLine = manager.GetComponent<caterpillarManager> ().finishLine;

		setupPosition ();
		setIncreasedSpeed ();
		yheadPos = transform.TransformPoint (new Vector3 (0, yOffset - ySize, 0)).y - screenHeight;	//must subtract screenheight as the caterpillar spawns above screen
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.y + yheadPos < finishLine) {
			GetComponent<BoxCollider2D> ().isTrigger = true;
		}
	}

	//makes caterpillar spawn so that its head is just above the screen
	//spawns in one of the lanes
	void setupPosition() {
		screenHeight = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, 0)).y;
		screenWidth = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, 0)).x;
		yOffset = GetComponent<BoxCollider2D> ().offset.y;
		ySize = GetComponent<BoxCollider2D> ().size.y/2;

		yMin = transform.TransformPoint (new Vector3 (0, ySize - yOffset, 0)).y;
		yMin = - screenHeight - yMin;	//used in manage caterpillar script

		//finding x and y pos for caterpillar
		float yWorldDim = transform.TransformPoint (new Vector3 (0, yOffset + ySize, 0)).y;	//offset from screenheight spawn pos to make caterpillar spawn off the screen
		int lane = Random.Range (-laneNumber/2, laneNumber/2);	//find lane
		float laneWidth = 2*screenWidth / laneNumber;
		float xPos = (lane + 0.5f) * laneWidth; 
		float yPos = screenHeight + yWorldDim;

		transform.position = new Vector3 (xPos, yPos, 0);
	}

	//set up speed based on the caterpillar spawn number
	//speed starts at minVel and increases linearly to maxVel with spawn number
	void setIncreasedSpeed() {
		float deltaVelocity = (maxVelocity - minVelocity) / totalCaterpillars;
		int currentCaterpillar = manager.GetComponent<caterpillarManager> ().currentSpawn;
		float speed = minVelocity + currentCaterpillar * deltaVelocity;
		GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -speed, 0);
	}
}
