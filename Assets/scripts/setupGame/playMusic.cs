using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//plays music on title screen. First checks music manager to be sure there is not already music playing
public class playMusic : MonoBehaviour {
	public AudioSource music;
	// Use this for initialization
	void Start () {
		if (!musicManager.Instance.isPlaying) {
			AudioSource newMusic = Instantiate (music);
			newMusic.Play ();
			musicManager.Instance.isPlaying = true;		//music now playing, therefore must set to true
			musicManager.Instance.music = newMusic.gameObject;
		}
	}

}
