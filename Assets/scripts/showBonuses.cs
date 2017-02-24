using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Instantiates bonus text score when caterpillar is killed in correct position with correct bonus numbers showing
public class showBonuses : MonoBehaviour {
	public GameObject bonusText;

	public bool dead{ get; set; }

	private int comboNumber;
	private bool isFar;

	private bool bonusNotInstantiated = true;

	private GameObject thisBonusText;
	private Transform streakText;
	private Transform streakNum;
	private Transform farText;
	private Transform farNum;

	private int farShotBonus;

	private float yLimit;	//if caterpillar has greater y position than this bonus text must be moved down so player can see it

	// Use this for initialization
	void Start () {
	//	Debug.Log (transform.position);
		dead = false;
		farShotBonus = scoreCount.Instance.farShotBonus;
		comboNumber = 0;
		isFar = false;
		yLimit = 4.0f;
	}
	
	// Update is called once per frame
	void Update () {
		//when caterpillar dies grab updated combo number and far status so that bonus text can be set correctly
		if (bonusNotInstantiated && dead) {
			comboNumber = scoreCount.Instance.playerCombo;
			isFar = scoreCount.Instance.far;
			instantiateBonus ();
			findTextMeshes ();
			setTextMeshes ();
			bonusNotInstantiated = false;
			gameObject.SetActive (false);
		}
	}

	void instantiateBonus() {
		thisBonusText = Instantiate (bonusText);

		//x position depends on which side of the screen caterpillar is on
		if (caterpillarIsOnRight ()) {
			thisBonusText.transform.position = new Vector3(transform.position.x - 0.55f, transform.position.y + 0.05f, transform.position.z);
		} else {
			thisBonusText.transform.position = new Vector3(transform.position.x + 0.55f, transform.position.y + 0.05f, transform.position.z);
		}

		//reset y position if caterpillar is too high up
		if (transform.position.y > yLimit) {
			thisBonusText.transform.position = new Vector3 (thisBonusText.transform.position.x, yLimit);
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
