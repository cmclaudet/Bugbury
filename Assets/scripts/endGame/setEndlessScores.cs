using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//resets high score for endless levels when player looks at endless level select.
//Attach to empty gameobject in endless level select scene.
public class setEndlessScores : MonoBehaviour {
	public Transform level1;
	public Transform level2;
	public Transform level3;
	public Transform level4;
	public Transform level5;

	// Use this for initialization
	void Start () {
		sethighScore (level1, highScoreManager.Instance.One);
		sethighScore (level2, highScoreManager.Instance.Two);
		sethighScore (level3, highScoreManager.Instance.Three);
		sethighScore (level4, highScoreManager.Instance.Four);
		sethighScore (level5, highScoreManager.Instance.Five);
	}

	//updates visible highscore
	void sethighScore(Transform level, highScoreManager.level levelInstance) {
		Text[] textComponents = level.GetComponentsInChildren<Text> ();

		//Final 3 text components are far shot number, max streak number and score number in that order.
		textComponents [textComponents.Length - 1].text = levelInstance.HSEndless.ToString ();
		textComponents [textComponents.Length - 2].text = levelInstance.MSEndless.ToString ();
		textComponents [textComponents.Length - 3].text = levelInstance.FSEndless.ToString ();

	}


}
