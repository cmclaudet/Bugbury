using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockManager : MonoBehaviour {
	public Rigidbody2D rocks;
	public bool makeRockNow = false;

	// Update is called once per frame
	void Update () {
//		Debug.Log (makeRockNow);
		if (makeRockNow) {
			Instantiate (rocks);
			makeRockNow = false;
		}
		
	}
}
