using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starSetup : MonoBehaviour {
	public blowUpGeneral starFill;
	private float maxScale;
	private bool needScaling;

	// Use this for initialization
	void Start () {
		GetComponent<RectTransform> ().SetAsFirstSibling();

		float vel = 2.0f;
		float acc = -0.2f;
		float scale = 0.1f;
		needScaling = true;

		maxScale = GetComponent<RectTransform> ().localScale.x;
		GetComponent<RectTransform> ().localScale = new Vector3(scale, scale);
		starFill = new blowUpGeneral (vel, acc, scale);
	}
	
	// Update is called once per frame
	void Update () {
		if (needScaling) {
			starFill.updateVelocity ();
			starFill.updateScale ();
			GetComponent<RectTransform> ().localScale = new Vector3 (starFill.scale, starFill.scale);
		}

		if (starFill.scale >= maxScale) {
			needScaling = false;
		}
	}
}
