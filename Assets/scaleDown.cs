using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleDown : MonoBehaviour {
	public blowUpGeneral scaleDownTxt;

	public float vel;	//initial downscaling velocity. should be negatiev for downscaling
	public float acc;	//acceleration. Should be positive
	public float startScale;	//initial scale

	private float endScale;		//final scale
	private bool needScaling;
	// Use this for initialization
	void Start () {
		scaleDownTxt = new blowUpGeneral (vel, acc, startScale);
		endScale = GetComponent<RectTransform> ().localScale.x;
		needScaling = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (needScaling) {
			scaleDownTxt.updateVelocity ();
			scaleDownTxt.updateScale ();
		}
	}
}
