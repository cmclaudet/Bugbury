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

	private GameObject thisBonusText;
	private Transform streakText;
	private Transform streakNum;
	private Transform farText;
	private Transform farNum;

	private int farShotBonus;

	// Use this for initialization
	void Start () {
	//	Debug.Log (transform.position);
		dead = false;
		manager = GameObject.Find ("game manager");
		farShotBonus = manager.GetComponent<scoreCount> ().farShotBonus;
		comboNumber = 0;
		isFar = false;
	}
	
	// Update is called once per frame
	void Update () {
		//when caterpillar dies grab updated combo number and far status so that bonus text can be set correctly
		if (bonusNotInstantiated && dead) {
			comboNumber = manager.GetComponent<scoreCount> ().playerCombo;
			isFar = manager.GetComponent<scoreCount> ().far;
			instantiateBonus ();
			findTextMeshes ();
			setTextMeshes ();
			bonusNotInstantiated = false;
			gameObject.SetActive (false);
		}
	}

	void instantiateBonus() {
		thisBonusText = Instantiate (bonusText);
		if (caterpillarIsOnRight ()) {
			thisBonusText.transform.position = new Vector3(transform.position.x - 0.55f, transform.position.y + 0.05f, transform.position.z);
		} else {
			thisBonusText.transform.position = new Vector3(transform.position.x + 0.55f, transform.position.y + 0.05f, transform.position.z);
		}
	}

	bool caterpillarIsOnRight() {
		if (transform.position.x > 0) {
			return true;
		} else {
			return false;
		}
	}

	void findTextMeshes() {
		Transform streak = thisBonusText.transform.Find ("streak");
		streakText = streak.Find ("streakText");
		streakNum = streak.Find ("streakNum");

		Transform far = thisBonusText.transform.Find ("far");
		farText = far.transform.Find ("farText");
		farNum = far.transform.Find ("farNum");
	}

	void setTextMeshes() {
		if (comboNumber > 1) {
			streakText.gameObject.GetComponent<TextMesh> ().text = comboNumber + " STREAK!";
			streakNum.gameObject.GetComponent<TextMesh> ().text = "+" + comboNumber;
		} else {
			streakText.gameObject.GetComponent<TextMesh> ().text = "";
			streakNum.gameObject.GetComponent<TextMesh> ().text = "+1";
		}

		if (isFar) {
			farText.gameObject.GetComponent<TextMesh> ().text = "FAR SHOT!";
			farNum.gameObject.GetComponent<TextMesh> ().text = "+" + farShotBonus;
		} else {
			farText.gameObject.GetComponent<TextMesh> ().text = "";
			farNum.gameObject.GetComponent<TextMesh> ().text = "";
		}
	}
}
