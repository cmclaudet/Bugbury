using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add to object which requires scaling upon instantiation
public class scaleSetup : MonoBehaviour {
	public blowUpGeneral scalingObj;
	public float timeDelay;	//delay in seconds for object to appear
	public float vel;
	public float acc;
	public float startScale;		//start scale, ie value which object starts at

	private float finalScale;	//final value object scales to
	private bool scalingUp;		//is true is object is growing from small to big. false otherwise.
	private float timePassed;
	private bool countTime;		//checks when timeDelay has passsed
	public bool needScaling{ get; set; }

	void Awake() {
		needScaling = false;	//set to false so that object only scales after time delay
	}

	// Use this for initialization
	void Start () {
//		GetComponent<RectTransform> ().SetAsFirstSibling ();
		timePassed = 0;
		countTime = true;	//set to true as it is necessary to count the time until time delay has passed
		finalScale = GetComponent<RectTransform> ().localScale.x;
		GetComponent<RectTransform> ().localScale = new Vector3(startScale, startScale);
		scalingObj= new blowUpGeneral (vel, acc, startScale);

		scalingUp = checkScalingDir ();
	}
	
	// Update is called once per frame
	void Update () {
		if (countTime) {
			addTime ();
		}

		if (timePassed > timeDelay) {
			stopCount ();
		}

		if (needScaling) {
			updateObjScale ();
		}

		checkScalingDone ();
	}

	bool checkScalingDir() {
		if (finalScale - startScale > 0) {
			return true;
		} else {
			return false;
		}
	}

	void addTime() {
		timePassed += Time.deltaTime;
	}

	void stopCount() {
		timePassed = 0;
		countTime = false;
		needScaling = true;
		Debug.Log ("start time!");
	}

	void updateObjScale() {
		scalingObj.updateVelocity ();
		scalingObj.updateScale ();
		GetComponent<RectTransform> ().localScale = new Vector3 (scalingObj.scale, scalingObj.scale);
		Debug.Log (GetComponent<RectTransform> ().localScale.x);
	}

	void checkScalingDone() {
		if (scalingUp) {
			if (scalingObj.scale >= finalScale) {
				stopScaling ();
			}
		} else {
			if (scalingObj.scale <= finalScale) {
				stopScaling ();
			}
		}
	}

	void stopScaling() {
		needScaling = false;
	}
}
