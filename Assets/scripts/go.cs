using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//delays starting level as soon as scene is loaded to introduce level and tell player to shoot
//like a countdown
public class go : MonoBehaviour {
	public float startTime;	//defines how long "level" message is up. Actual start time is startTime*1.5
	public Button pauseButton;

	private float timetoStart;
	public GameObject manager;
	// Use this for initialization
	void Start () {
		timetoStart = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timetoStart += Time.deltaTime;

		if (timetoStart >= startTime) {
			Transform lvl2text = transform.Find ("levelStart");
			lvl2text.GetComponent<Text> ().text = "SHOOT!";
		}
		if (timetoStart >= 1.5f * startTime) {
			GetComponent<RectTransform> ().gameObject.SetActive (false);
			manager.GetComponent<caterpillarManager> ().control = true;
			pauseButton.interactable = true;
		}
	}
}
