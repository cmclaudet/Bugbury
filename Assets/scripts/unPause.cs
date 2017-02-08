using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unPause : MonoBehaviour {
	private AudioSource click;

	void Awake() {
		click = GameObject.Find ("click").GetComponent<AudioSource> ();
	}

	public void unpause() {
		click.Play ();
		Time.timeScale = 1.0f;
		gameObject.SetActive (false);
		GameObject manager = GameObject.Find ("game manager");
		manager.GetComponent<caterpillarManager> ().control = true;
	}
}
