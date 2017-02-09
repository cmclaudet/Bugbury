using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeText : MonoBehaviour {
	//makes alpha value of text reduce over time
	public float fadeTime = 1;	//time taken to reduce to zero

	private float timePassed;
	private float alphaValue;	//max alpha value
	private float redValue;		//text red value
	private float greenValue;	//text green value
	private float blueValue;	//text blue value

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer> ().sortingLayerName = "UI";

		timePassed = 0;
		alphaValue = GetComponent<MeshRenderer> ().material.color.a;
		redValue = GetComponent<MeshRenderer> ().material.color.r;
		greenValue = GetComponent<MeshRenderer> ().material.color.g;
		blueValue = GetComponent<MeshRenderer> ().material.color.b;
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;

		float factor = 1 - timePassed / fadeTime;	//factor to multiply by max alpha to obtain new alpha value
		float newAlpha = factor * alphaValue;

		GetComponent<MeshRenderer> ().material.SetColor("_Color", new Color(redValue, greenValue, blueValue, newAlpha));
		if (timePassed >= fadeTime) {
			gameObject.SetActive (false);
		}
	}
}
