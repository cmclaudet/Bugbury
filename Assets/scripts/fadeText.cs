using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeText : MonoBehaviour {
	public float fadeTime;

	private float timePassed;
	private float alphaValue;
	private float redValue;
	private float greenValue;
	private float blueValue;

	// Use this for initialization
	void Start () {
		timePassed = 0;
		alphaValue = GetComponent<MeshRenderer> ().material.color.a;
		redValue = GetComponent<MeshRenderer> ().material.color.r;
		greenValue = GetComponent<MeshRenderer> ().material.color.g;
		blueValue = GetComponent<MeshRenderer> ().material.color.b;
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;

		float factor = 1 - timePassed / fadeTime;
		float newAlpha = factor * alphaValue;

		GetComponent<MeshRenderer> ().material.SetColor("_Color", new Color(redValue, greenValue, blueValue, newAlpha));
		if (timePassed >= fadeTime) {
			gameObject.SetActive (false);
		}
	}
}
