using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMusic : MonoBehaviour {
	public AudioSource music;
	// Use this for initialization
	void Start () {
		if (!musicManager.Instance.isPlaying) {
			AudioSource newMusic = Instantiate (music);
			newMusic.Play ();
			musicManager.Instance.isPlaying = true;
			musicManager.Instance.music = newMusic.gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
