using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add to object which requires scaling upon instantiation
public class scaleSetup : MonoBehaviour {
	public blowUpGeneral scaleUp;
	public float vel;
	public float acc;
	public float scale;		//start scale, ie small value which object starts at

	private float maxScale;	//max value object scales up to
	private bool needScaling;

	// Use this for initialization
	void Start () {
		GetComponent<RectTransform> ().SetAsFirstSibling();
		needScaling = true;

		maxScale = GetComponent<RectTransform> ().localScale.x;
		GetComponent<RectTransform> ().localScale = new Vector3(scale, scale);
		scaleUp = new blowUpGeneral (vel, acc, scale);
	}
	
	// Update is called once per frame
	void Update () {
		if (needScaling) {
			scaleUp.updateVelocity ();
			scaleUp.updateScale ();
			GetComponent<RectTransform> ().localScale = new Vector3 (scaleUp.scale, scaleUp.scale);
		}

		if (scaleUp.scale >= maxScale) {
			needScaling = false;
		}
	}
}
