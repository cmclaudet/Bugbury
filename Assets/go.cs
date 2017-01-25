using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class go : MonoBehaviour {
	public float startTime;
	private float timetoStart;
	private GameObject manager;
	// Use this for initialization
	void Start () {
		timetoStart = 0;
		manager = GameObject.Find ("game manager");
	}
	
	// Update is called once per frame
	void Update () {
		timetoStart += Time.deltaTime;

		if (timetoStart >= startTime) {
			GetComponent<Text> ().text = "SHOOT!";
		}
		if (timetoStart >= 1.5f * startTime) {
			GetComponent<RectTransform> ().gameObject.SetActive (false);
			manager.GetComponent<caterpillarManager> ().control = true;
		}
	}
}
