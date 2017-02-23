using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setFarBonus : MonoBehaviour {
	public int farBonus;
	// Use this for initialization
	void Awake () {
		scoreCount.Instance.farShotBonus = farBonus;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
