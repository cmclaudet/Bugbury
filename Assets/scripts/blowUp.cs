using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blowUp : MonoBehaviour {
	private float maxScale;
	private float currScale;
	private float acc;
	private float vel;

	private bool needScaling;

	// Use this for initialization
	void Start () {
		needScaling = true;
		currScale = 0.1f;
		acc = -0.2f;
		vel = 3.0f;

		maxScale = GetComponent<RectTransform> ().localScale.x;
		GetComponent<RectTransform> ().localScale = new Vector3(currScale, currScale);
	}
	
	// Update is called once per frame
	void Update () {
		if (needScaling) {
			vel = vel + acc * Time.deltaTime;
			currScale = currScale + vel*Time.deltaTime + 0.5f*acc*(Time.deltaTime)*(Time.deltaTime);
			GetComponent<RectTransform> ().localScale = new Vector3 (currScale, currScale);
		}

		if (currScale >= maxScale) {
			needScaling = false;
		}
	}
}
