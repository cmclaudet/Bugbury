using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCamShake : MonoBehaviour {
	private GameObject Camera;
	private bool doneScaling;
	// Use this for initialization
	void Start () {
		Camera = GameObject.Find ("Main Camera");
		doneScaling = false;
	}
	
	// Update is called once per frame
	void Update () {
		doneScaling = GetComponent<scaleSetup> ().doneScaling;

		if (doneScaling) {
			Camera.GetComponent<CameraShake> ().enabled = true;
		}
	}


}
