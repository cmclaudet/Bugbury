using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blowUp : MonoBehaviour {
	public float blowUpTime;

	private float timePassed;
	private float maxScale;
	private float minScale;

	private bool needScaling;

	// Use this for initialization
	void Start () {
		needScaling = true;
		minScale = 0.1f;
		maxScale = GetComponent<RectTransform> ().localScale.x;
		GetComponent<RectTransform> ().localScale = new Vector3(minScale, minScale);
		timePassed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (needScaling) {
			timePassed += Time.deltaTime;
			float newScale = minScale + (maxScale - minScale) * (timePassed / blowUpTime);
			GetComponent<RectTransform> ().localScale = new Vector3 (newScale, newScale);
		}

		if (timePassed >= blowUpTime) {
			needScaling = false;
		}

	}
}
