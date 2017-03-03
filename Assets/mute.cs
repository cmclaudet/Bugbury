using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mute : MonoBehaviour {
	public bool isMuted{get;set;}

	void Start() {
		isMuted = false;
	}

	public void toggleMute() {
		if (isMuted) {
			AudioListener.volume = 1.0f;
			isMuted = false;
		} else {
			AudioListener.volume = 0;
			isMuted = true;
		}
	}
}
