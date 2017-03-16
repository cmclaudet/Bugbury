using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeText : MonoBehaviour {
	//makes alpha value of text increase over time
	public float fadeTime = 1;	//time taken to reduce to zero

	private float timePassed;

	private float startAlphaValue; 
	private float alphaValue;	//max alpha value
	private float redValue;		//text red value
	private float greenValue;	//text green value
	private float blueValue;	//text blue value

	private bool needScaling;
	// Use this for initialization
	void Start () {
		timePassed = 0;
		needScaling = true;

		startAlphaValue = 0;
		alphaValue = GetComponent<Text> ().color.a;
		redValue = GetComponent<Text>().color.r;
		greenValue = GetComponent<Text>().color.g;
		blueValue = GetComponent<Text>().color.b;

		GetComponent<Text> ().color = new Color (redValue, greenValue, blueValue, startAlphaValue);
	}
	
	// Update is called once per frame
	void Update () {
		if (needScaling) {
			timePassed += Time.deltaTime;

			float factor = timePassed / fadeTime;	//factor to multiply by max alpha to obtain new alpha value
			float newAlpha = factor * alphaValue;

			GetComponent<Text> ().color = new Color (redValue, greenValue, blueValue, newAlpha);
		}
		if (timePassed >= fadeTime) {
			needScaling = false;
		}
	}
}
