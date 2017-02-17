using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showBonuses : MonoBehaviour {
	public GameObject bonusTextRight;
	public GameObject bonusTextLeft;

	public bool dead{ get; set; }

	private int comboNumber;
	private bool isFar;

	private bool bonusNotInstantiated = true;
	private GameObject manager;

	// Use this for initialization
	void Start () {
	//	Debug.Log (transform.position);
		dead = false;
		manager = GameObject.Find ("game manager");
		comboNumber = 0;
		isFar = false;
	}
	
	// Update is called once per frame
	void Update () {
		comboNumber = manager.GetComponent<scoreCount> ().playerCombo;
		isFar = manager.GetComponent<scoreCount> ().far;
		if ((comboNumber > 1 || isFar)) {
			if (bonusNotInstantiated && dead) {
				instantiateBonus ();
				bonusNotInstantiated = false;
			}
		}
	}

	void instantiateBonus() {
		GameObject thisBonusText;
		if (isOnRight ()) {
			thisBonusText = Instantiate (bonusTextLeft);
		} else {
			thisBonusText = Instantiate (bonusTextRight);
		}

		thisBonusText.transform.position = new Vector3(transform.position.x - 0.55f, transform.position.y + 0.05f, transform.position.z);
		Transform comboNumText = thisBonusText.transform.Find ("comboText");
		if (comboNumber > 2) {
			comboNumText.gameObject.GetComponent<TextMesh> ().text = comboNumber + " STREAK!";
		} else {
			comboNumText.gameObject.GetComponent<TextMesh> ().text = " ";
		}

		Transform isFarText = thisBonusText.transform.Find ("farText");
		if (isFar) {
			isFarText.gameObject.GetComponent<TextMesh> ().text = "FAR X2";
		} else {
			isFarText.gameObject.GetComponent<TextMesh> ().text = " ";
		}

		gameObject.SetActive (false);

	}

	bool isOnRight() {
		if (transform.position.x > 0) {
			return true;
		} else {
			return false;
		}
	}
}
