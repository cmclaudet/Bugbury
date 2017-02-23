using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rewrite : MonoBehaviour {

	void Start() {
		scoreCount.Instance.scoreObject = gameObject;
		rewriteScore ("0");
	}

	public void rewriteScore(string newScore) {
		Component[] text = GetComponentsInChildren<Text> ();
		foreach (Component textComp in text) {
			textComp.GetComponent<Text> ().text = newScore;
		}
	}
}
