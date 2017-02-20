using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCamShake : MonoBehaviour {
	private GameObject Camera;
	private bool doneScaling;
	private bool doneShaking;
	// Use this for initialization
	void Start () {
		Camera = GameObject.Find ("Main Camera");
		doneScaling = false;
		doneShaking = false;
	}
	
	// Update is called once per frame
	void Update () {
		doneScaling = GetComponent<scaleSetup> ().doneScaling;

		if (doneScaling && doneShaking == false) {
			Camera.GetComponent<CameraShake> ().enabled = true;
			doneShaking = true;
		}
	}


}
