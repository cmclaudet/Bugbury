using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add to combo text to make text scale up then scale down before disappearing
//text will scale up to a larger maximum value the larger the combo number is
//An instance of custom class blowUpGeneral is created to find how object scales over time with a given velocity and acceleration
public class scaleBonusText : MonoBehaviour {
	public float timePresent;		//time text is present
	public blowUpGeneral scaleUp;
	public float vel;
	public float acc;
	public float minScale;		//start scale, ie small value which object starts at

	public float minMaxScale;		//smallest max value object scales up to
	public float maxMaxScale;		//largest max value object scales up to

	private float maxScale;			//max value this particular object will scale up to. depends on current combo number. 
	private bool needScaling;

	private float timePassed;		//time since text appeared
	private bool endless;

	// Use this for initialization
	void Start () {
		findMaxScale ();
		needScaling = true;
		GetComponent<Transform> ().localScale = new Vector3(minScale, minScale);
		scaleUp = new blowUpGeneral (vel, acc, minScale);

		timePassed = 0;
		endless = caterpillarManager.Instance.endlessLevel;
	}

	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;

		if (needScaling) {
			scaleUp.updateVelocity ();
			scaleUp.updateScale ();
			GetComponent<Transform> ().localScale = new Vector3 (scaleUp.scale, scaleUp.scale);
		}

		if (scaleUp.scale >= maxScale) {
			needScaling = false;
		}

		//start scaling down once text has remained for the designated time
		if (timePassed >= timePresent) {
			scaleUp.velocity = -vel;
			scaleUp.acceleration = acc;
			needScaling = true;
		}

		//inactivate gamobject once it has become small enough
		if (scaleUp.scale < minScale) {
			this.gameObject.SetActive(false);
		}
	}

	//finds max scale this particular combo text should scale up to
	void findMaxScale() {
		int currentCombo = scoreCount.Instance.playerCombo;
		int numOfCaterpillars = caterpillarManager.Instance.totalCaterpillars;
		int caterpillarsToMaxSpeed = caterpillarManager.Instance.caterpillarNumToMaxSpeed;

		//if level is endless text enlargement depends on caterpillar number to max speed (number of caterpillars needed for caterpillars to get to max speed) and not total caterpillars
		//if current combo is larger than caterpillar number to max speed max scale no longer increases
		if (endless) {
			if (currentCombo > caterpillarsToMaxSpeed) {
				maxScale = maxMaxScale;
			} else {
				maxScale = minMaxScale + currentCombo * (maxMaxScale - minMaxScale) / caterpillarsToMaxSpeed;
			}
		} else {
			maxScale = minMaxScale + currentCombo * (maxMaxScale - minMaxScale) / numOfCaterpillars;
		}
	}
}
