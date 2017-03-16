using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//place on object in hierarchy so that public values can be set according to scene, ie level.
//values are set in singleton manager classes so they can be easily referred to in other scripts
public class setLevelVariablesEndless : MonoBehaviour {
	public int farBonus;

	// Use this for initialization
	void Awake () {
		scoreCount.Instance.farShotBonus = farBonus;
	}

}
