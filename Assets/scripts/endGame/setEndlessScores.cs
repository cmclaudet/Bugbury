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
		textComponents [textComponents.Length - 1].text = levelInstance.HSEndless.ToString ();
//		level1.GetComponentsInChildren<Text>()[1].text = "High Score: " + highScoreManager.Instance.One.HSEndless;
	}


}
