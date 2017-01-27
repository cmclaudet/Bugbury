using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unPause : MonoBehaviour {

	public void unpause() {
		Time.timeScale = 1.0f;
		gameObject.SetActive (false);
		GameObject manager = GameObject.Find ("game manager");
		manager.GetComponent<caterpillarManager> ().control = true;
	}
}
