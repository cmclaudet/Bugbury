using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//resets high score for endless levels when player looks at endless level select.
//Attach to empty gameobject in endless level select scene.
public class setEndlessScores : MonoBehaviour {
	public GameObject levelSelectMenus;
	private Component[] levelMenus;

	// Use this for initialization
	void Start () {
		levelMenus = levelSelectMenus.GetComponentsInChildren(typeof(Animator), true);
		for (int i = 0; i < levelMenus.Length; i++) {
			sethighScore(levelMenus[i].GetComponent<Animator>(), highScoreManager.Instance.endlessLevels[i]);
		}
	}

	//updates visible highscore
	void sethighScore(Animator level, highScoreManager.levelEndless levelInstance) {
		Text[] textComponents = level.GetComponentsInChildren<Text> ();

		//Final 3 text components are far shot number, max streak number and score number in that order.
		textComponents [textComponents.Length - 1].text = levelInstance.highScore.ToString();
		textComponents [textComponents.Length - 2].text = levelInstance.maxStreak.ToString();
		textComponents [textComponents.Length - 3].text = levelInstance.farShots.ToString();

	}


}
