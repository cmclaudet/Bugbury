using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showBonuses : MonoBehaviour {
	public GameObject bonusText;

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
		if ((comboNumber > 2 || isFar)) {
			if (bonusNotInstantiated && dead) {
				instantiateBonus ();
				bonusNotInstantiated = false;
			}
		}
	}

	void instantiateBonus() {
		Debug.Log (comboNumber);
		GameObject thisBonusText = Instantiate (bonusText);
		thisBonusText.transform.position = transform.position;
		Transform comboNumText = thisBonusText.transform.Find ("comboText");
		if (comboNumber > 2) {
			Debug.Log (comboNumText.gameObject.GetComponent<TextMesh> ().text);
			comboNumText.gameObject.GetComponent<TextMesh> ().text = "COMBO X" + comboNumber;
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
}
