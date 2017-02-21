using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCamShake : MonoBehaviour {
	private GameObject Camera;
	private bool doneScaling;
	private bool doneShaking;
	private AudioSource bam;
	// Use this for initialization
	void Start () {
		Camera = GameObject.Find ("Main Camera");
		doneScaling = false;
		doneShaking = false;
		bam = GameObject.Find ("bam").GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		doneScaling = GetComponent<scaleSetup> ().doneScaling;

		if (doneScaling && doneShaking == false) {
			bam.Play ();
			Camera.GetComponent<CameraShake> ().enabled = true;
			doneShaking = true;
		}
	}


}
