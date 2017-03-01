using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*add to object which requires scaling upon instantiation. A time delay and sound effect can be set for this scaling
  An instance of the custom class blowUpGeneral is made to find how the object should scale with time with a given velocity and acceleration (as non linear scaling is better than linear)
  Time delay is time in seconds before object appears. It is equal to: order*timeDelay. Order value is used here so that object appearance can easily be ordered
  Useful for level complete menu which has many objects that require scaling with different time delays */
public class scaleSetup : MonoBehaviour {
	public blowUpGeneral scalingObj;		//create instance of scaling object with blowUpGeneral script to calculate all values of scale

	//total delay for object to appear = timeDelay * order (see below)
	public float timeDelay;
	public int order;				//the order which the text comes up. max streak = 1, max streak num = 2, etc. set to 0 for object to scale immediately
	public float vel;				//velocity and acceleration of scaling
	public float acc;
	public float startScale;		//start scale, ie value which object starts at
	public bool lowTimeStep;		//set to true if scaling needs many checks per second to be precise
	public string desiredSoundEffect;	//input name of desired sound effect in hierarchy. If no name is placed there will be no sound effect
	public string downscaleSound;

	private AudioSource soundEffect;
	private AudioSource soundEffectDownscale;
	public float finalScale{ get; set; }	//final value object scales to
	private bool scalingUp;		//is true if object is growing from small to big. false otherwise.
	private float timePassed;
	private bool countTime;		//checks when timeDelay has passsed
	public bool needScaling{ get; set; }	//ensures object is only scaled when it needs to be
	public bool doneScaling{ get; set; }	//ensures fixed delta time is reverted to its original number only once (right after scaling is done), not repeatedly
	private bool scalingDown;				//set to true when scaling down. allows gameobject to inactivate once scaling down is done

	void Awake() {
		needScaling = false;	//set to false so that object only scales after time delay
		doneScaling = false;
		scalingDown = false;
	}

	// Use this for initialization
	void Start () {
		timePassed = 0;
		countTime = true;	//set to true as it is necessary to count the time until time delay has passed
		finalScale = GetComponent<RectTransform> ().localScale.x;	//final scale should always be object default value (before initial scale is set)
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

		if (!doneScaling) {
			checkScalingDone ();
		}

		//if object scales down afterwards must be set to inactive once downscaling is complete
		if (doneScaling && scalingDown) {
			gameObject.SetActive (false);
		}
	}

	void setSoundEffect() {
		//if no name placed no sound effect is set
		if (desiredSoundEffect != "") {
			soundEffect = GameObject.Find (desiredSoundEffect).GetComponent<AudioSource> ();
		}
		if (downscaleSound != "") {
			soundEffectDownscale = GameObject.Find (downscaleSound).GetComponent<AudioSource> ();
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
		//finds new value of object scale through blowUpGeneral functions and sets these to the object
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

	public void startScalingDown() {
		countTime = true;
		timePassed = 0;
		timeDelay = 0;
		soundEffect = soundEffectDownscale;
		scalingObj.velocity = -vel;
		scalingObj.acceleration = acc;
		finalScale = 0;
		scalingUp = checkScalingDir ();
		doneScaling = false;
		scalingDown = true;
		highScoreManager.Instance.askedForRating = true;
	}
}
