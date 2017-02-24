using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setFarBonus : MonoBehaviour {
	public int farBonus;
	public float minVelocity;
	public float maxVelocity;
	public int totalCaterpillars;
	// Use this for initialization
	void Awake () {
		scoreCount.Instance.farShotBonus = farBonus;
		caterpillarManager.Instance.minVel = minVelocity;
		caterpillarManager.Instance.maxVel = maxVelocity;
		caterpillarManager.Instance.totalCaterpillars = totalCaterpillars;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
