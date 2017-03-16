using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sets caterpillar position onto one of six lanes randomely and finds y co-ordinate for caterpillar spawn point based on screen height and caterpillar body lenght
//sets caterpillar speed based on spawn number and min max speeds set in inspector

public class moveEndless : MonoBehaviour {
	private int laneNumber;	//must be EVEN
	private float velocity;

	private float yOffset;	//size of caterpillar
	private float ySize;	//offset of caterpillar
	private float yheadPos;	//position of caterpillar head from body in world co-ordinates

//	public float yMin { get; set; }	//minimum y value for caterpillar to be active. must be calculated here as makes use of caterpillar body length values.

	private int totalCaterpillars;
	private float finishLine;

	// Use this for initialization
	void Awake () {
		//grab caterpillar manager values
		laneNumber = caterpillarManagerEndless.Instance.lanes;
		velocity = caterpillarManagerEndless.Instance.minVel + caterpillarManagerEndless.Instance.currentSpawn*caterpillarManagerEndless.Instance.velocityIncrement;
		finishLine = caterpillarManagerEndless.Instance.finishLine;

		setupPosition ();
		GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -velocity, 0);
//		setIncreasedSpeed ();
		yheadPos = transform.TransformPoint (new Vector3 (0, yOffset - ySize, 0)).y - ScreenVariables.worldHeight;	//must subtract screenheight as the caterpillar spawns above screen
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
		yOffset = GetComponent<BoxCollider2D> ().offset.y;
		ySize = GetComponent<BoxCollider2D> ().size.y/2;

		//finding x and y pos for caterpillar
		float yWorldDim = transform.TransformPoint (new Vector3 (0, yOffset + ySize, 0)).y;	//offset from screenheight spawn pos to make caterpillar spawn off the screen
		int lane = Random.Range (-laneNumber/2, laneNumber/2);	//find lane
		float laneWidth = 2*ScreenVariables.worldWidth / laneNumber;
		float xPos = (lane + 0.5f) * laneWidth; 
		float yPos = ScreenVariables.worldHeight + yWorldDim;

		transform.position = new Vector3 (xPos, yPos, 0);
	}

}
