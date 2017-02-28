using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//delays starting level as soon as scene is loaded to introduce level and tell player to shoot
//like a countdown
public class go : MonoBehaviour {
	public float startTime;	//defines how long "level" message is up. Actual start time is startTime*1.5 (need more time for player to read SHOOT! message)
	public Button pauseButton;

	private float timetoStart;
	public AudioSource whistle;	//whistle sound for starting

	private bool whistlePlayed;
	// Use this for initialization
	void Start () {
		timetoStart = 0;
		whistlePlayed = false;
	}
	
	// Update is called once per frame
	void Update () {
		timetoStart += Time.deltaTime;

		if (timetoStart >= startTime && whistlePlayed == false) {
			Text lvl2text = GetComponentInChildren<Text> ();
			lvl2text.GetComponent<Text> ().text = "SHOOT!";
			whistle.Play ();
			whistlePlayed = true;
		}
		if (timetoStart >= 1.5f * startTime) {
			GetComponent<RectTransform> ().gameObject.SetActive (false);
			lifeManager.Instance.control = true;
			pauseButton.interactable = true;
		}
	}
}
