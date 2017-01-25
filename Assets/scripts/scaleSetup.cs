using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleSetup : MonoBehaviour {
	public blowUpGeneral scaleUp;
	public float vel;
	public float acc;
	public float scale;

	private float maxScale;
	private bool needScaling;

	// Use this for initialization
	void Start () {
		GetComponent<RectTransform> ().SetAsFirstSibling();
/*
		float vel = 2.0f;
		float acc = -0.2f;
		float scale = 0.1f;*/
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
