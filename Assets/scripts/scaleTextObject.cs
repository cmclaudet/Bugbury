using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add to object which requires scaling upon instantiation
public class scaleTextObject : MonoBehaviour {
	public float timePresent;
	public blowUpGeneral scaleUp;
	public float vel;
	public float acc;
	public float scale;		//start scale, ie small value which object starts at

	private float maxScale;	//max value object scales up to
	private bool needScaling;

	private float timePassed;

	// Use this for initialization
	void Start () {
		needScaling = true;

		maxScale = GetComponent<Transform> ().localScale.x;
		GetComponent<Transform> ().localScale = new Vector3(scale, scale);
		scaleUp = new blowUpGeneral (vel, acc, scale);

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

		if (scaleUp.scale < scale) {
			this.gameObject.SetActive(false);
		}
	}
}
