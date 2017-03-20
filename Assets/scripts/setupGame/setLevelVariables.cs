using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//place on object in hierarchy so that public values can be set according to scene, ie level.
//values are set in singleton manager classes so they can be easily referred to in other scripts
public class setLevelVariables : MonoBehaviour {
	public int farBonus;
	public float minVelocity;
	public float maxVelocity;
	public float coolDownOnMiss;
	public int totalCaterpillars;
	public int caterpillarNumToMaxSpeed;

	public bool endlessLevel;
	// Use this for initialization
	void Awake () {
		scoreCount.Instance.farShotBonus = farBonus;
		caterpillarManager.Instance.minVel = minVelocity;
		caterpillarManager.Instance.maxVel = maxVelocity;
		caterpillarManager.Instance.caterpillarNumToMaxSpeed = caterpillarNumToMaxSpeed;
		rockManager.Instance.coolDownOnMiss = coolDownOnMiss;
		caterpillarManager.Instance.totalCaterpillars = totalCaterpillars;
		caterpillarManager.Instance.endlessLevel = endlessLevel;
	}

}
