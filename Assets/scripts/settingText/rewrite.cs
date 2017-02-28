using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//rewrites scores on the top of screen for players to see
public class rewrite : MonoBehaviour {

	void Start() {
		scoreCount.Instance.scoreObject = gameObject;
		rewriteScore ("0");
	}

	//called from score count singleton when change score function is triggered
	public void rewriteScore(string newScore) {
		Component[] text = GetComponentsInChildren<Text> ();
		//there are 9 text components for the score (1 for white text, 8 for the outline)
		foreach (Component textComp in text) {
			textComp.GetComponent<Text> ().text = newScore;
		}
	}
}
