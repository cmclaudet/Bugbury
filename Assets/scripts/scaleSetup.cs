using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add to object which requires scaling upon instantiation
public class scaleSetup : MonoBehaviour {
	public blowUpGeneral scalingObj;		//create instance of scaling object with blowUpGeneral script to calculate all values of scale
	public float timeDelay;	//total delay for object to appear = timeDelay * order (see below)
	public int order;				//the order which the text comes up. max streak = 1, max streak num = 2, etc. set to 0 for object to scale immediately
	public float vel;				//velocity and acceleration of scaling
	public float acc;
	public float startScale;		//start scale, ie value which object starts at
	public bool lowTimeStep;		//set to true if scaling needs many checks per second to be precise
	public string desiredSoundEffect;	//input name of desired sound effect in hierarchy. If no name is placed there will be no sound effect

	private AudioSource soundEffect;
	private float finalScale;	//final value object scales to
	private bool scalingUp;		//is true is object is growing from small to big. false otherwise.
	private float timePassed;
	private bool countTime;		//checks when timeDelay has passsed
	public bool needScaling{ get; set; }	//ensures object is only scaled when it needs to be
	public bool doneScaling{ get; set; }	//ensures fixed delta time is reverted to its original number only once (right after scaling is done), not repeatedly

	void Awake() {
		needScaling = false;	//set to false so that object only scales after time delay
		doneScaling = false;
	}

	// Use this for initialization
	void Start () {
		timePassed = 0;
		countTime = true;	//set to true as it is necessary to count the time until time delay has passed
		finalScale = GetComponent<RectTransform> ().localScale.x;
		GetComponent<RectTransform> ().localScale = new Vector3(startScale, startScale);
		scalingObj= new blowUpGeneral (vel, acc, startScale);

		scalingUp = checkScalingDir ();
		timeDelay *= order;

		setSoundEffect ();
	}
	
	void FixedUpdate () {
		if (countTime) {
			addTime ();
		}

		if (timePassed > timeDelay) {
			stopCount ();
			playSound ();
			setTimeStep ();
		}

		if (needScaling) {
			updateObjScale ();
		}

		if (doneScaling == false) {
			checkScalingDone ();
		}
	}

	void setSoundEffect() {
		//if no name placed no sound effect is set
		if (desiredSoundEffect != "") {
			soundEffect = GameObject.Find (desiredSoundEffect).GetComponent<AudioSource> ();
		}
	}

	void setTimeStep() {
		if (lowTimeStep) {
			Time.fixedDeltaTime = 0.001f;
		}
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

	}

	void playSound() {
		if (soundEffect != null) {
			soundEffect.Play();
		}
	}

	void updateObjScale() {
		scalingObj.updateVelocity ();
		scalingObj.updateScale ();
		GetComponent<RectTransform> ().localScale = new Vector3 (scalingObj.scale, scalingObj.scale);
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
		Time.fixedDeltaTime = 0.02f;
		doneScaling = true;
	}
}
