using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add to combo text to make text scale up then scale down before disappearing
//text will scale up to a larger maximum value the larger the combo number is
public class scaleTextObject : MonoBehaviour {
	public float timePresent;
	public blowUpGeneral scaleUp;
	public float vel;
	public float acc;
	public float minScale;		//start scale, ie small value which object starts at

	public float minMaxScale;		//smallest max value object scales up to
	public float maxMaxScale;		//largest max value object scales up to

	private GameObject manager;

	private float maxScale;			//max value this particular object will scale up to. depends on current combo number. 
	private bool needScaling;

	private float timePassed;

	// Use this for initialization
	void Start () {
		findMaxScale ();
		needScaling = true;
		//maxScale = GetComponent<Transform> ().localScale.x;
		GetComponent<Transform> ().localScale = new Vector3(minScale, minScale);
		scaleUp = new blowUpGeneral (vel, acc, minScale);

		timePassed = 0;
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

		if (timePassed >= timePresent) {
			scaleUp.velocity = -vel;
			scaleUp.acceleration = acc;
			needScaling = true;
		}

		if (scaleUp.scale < minScale) {
			this.gameObject.SetActive(false);
		}
	}

	//finds max scale this particular combo text should scale up to
	void findMaxScale() {
		manager = GameObject.Find ("game manager");
		int currentCombo = manager.GetComponent<scoreCount> ().playerCombo;
		int numOfCaterpillars = manager.GetComponent<caterpillarManager>().totalCaterpillars;
		maxScale = minMaxScale + currentCombo*(maxMaxScale - minMaxScale) /numOfCaterpillars;
	}
}
