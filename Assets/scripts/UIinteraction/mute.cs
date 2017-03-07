using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mute : MonoBehaviour {
	public Sprite muteOn;
	public Sprite muteOff;
	private bool isMuted;

	void Start() {
		isMuted = musicManager.Instance.isMuted;
		updateImageAndSound ();
	}

	public void toggleMute() {
		if (isMuted) {
			musicManager.Instance.isMuted = false;
		} else {
			musicManager.Instance.isMuted = true;
		}
		isMuted = musicManager.Instance.isMuted;

		updateImageAndSound ();
	}

	void updateImageAndSound() {
		if (isMuted) {
			GetComponent<Image> ().sprite = muteOn;
			AudioListener.volume = 0;
		} else {
			GetComponent<Image> ().sprite = muteOff;
			AudioListener.volume = 1.0f;
		}
	}
}
