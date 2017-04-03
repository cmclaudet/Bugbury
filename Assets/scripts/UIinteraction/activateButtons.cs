using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[SerializeField]
public class activateButtons : MonoBehaviour {

	public Button[] buttons;
	void Start() {
		buttons = GetComponentInParent<resetScores>().allButtons;
	}	
	// Update is called once per frame
	void Update () {
		if (GetComponent<scaleSetup>().doneScaling) {
			foreach (Button button in buttons) {
				button.interactable = true;
			}
		}
	}
}
