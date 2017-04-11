using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//placed on empty gameobject in level select scene. Rewrites high score, star appearances and perfect status if necessary so player sees updated version of their score
public class setHighScores : MonoBehaviour {
	public GameObject levelSelectMenus;
	private Component[] levelMenus;
	public Transform canvas;
	public Transform starFill;
	public Transform perfectText;

	// Use this for initialization
	void Start () {
		levelMenus = levelSelectMenus.GetComponentsInChildren(typeof(Animator), true);
		for (int i = 0; i < levelMenus.Length; i++) {
			writeHighScore (levelMenus[i].GetComponent<Animator>(), highScoreManager.Instance.arcadeLevels[i]);
			checkStarsAndPerfect (levelMenus[i].GetComponent<Animator>(), highScoreManager.Instance.arcadeLevels[i]);
		}
	}

	//updates visible highscore
	void writeHighScore(Animator level, highScoreManager.levelArcade levelInstance) {		
		level.transform.GetComponentsInChildren<Text>()[1].text = "High Score: " + levelInstance.highScore;
	}

	//fills stars in correct place if player has acquired them in levels

	void checkStarsAndPerfect(Animator level, highScoreManager.levelArcade levelInstance) {
		if (levelInstance.star1) {
			fillStars (level.transform, "star1");
		}
		if (levelInstance.star2) {
			fillStars (level.transform, "star2");
		}
		if (levelInstance.star3) {
			fillStars (level.transform, "star3");
		}
		if (levelInstance.perfect) {
			Transform perfect = Instantiate (perfectText);
			perfect.transform.SetParent (level.transform, false);
		}
	}

	void fillStars(Transform levelSign, string star) {
		Transform desiredStar = levelSign.transform.Find (star);
		Transform newStarFill = Instantiate (starFill);
		newStarFill.transform.SetParent (levelSign, false);
		newStarFill.gameObject.GetComponent<setToStar> ().star = desiredStar;
	}

}
