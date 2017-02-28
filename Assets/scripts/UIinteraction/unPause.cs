using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//unpauses when player presses resume
public class unPause : MonoBehaviour {
	public AudioSource click{ get; set; }
	public Button pauseButton{ get; set; }

	public void unpause() {
		pauseButton.interactable = true;
		click.Play ();
		Time.timeScale = 1.0f;
		gameObject.SetActive (false);
		lifeManager.Instance.control = true;
	}
}
