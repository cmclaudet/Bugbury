using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mute : MonoBehaviour {
	public bool isMuted{get;set;}
	public Sprite muteOn;
	public Sprite muteOff;

	void Start() {
		isMuted = false;
	}

	public void toggleMute() {
		if (isMuted) {
			GetComponent<Image> ().sprite = muteOn;
			AudioListener.volume = 1.0f;
			isMuted = false;
		} else {
			GetComponent<Image> ().sprite = muteOff;
			AudioListener.volume = 0;
			isMuted = true;
		}
	}
}
