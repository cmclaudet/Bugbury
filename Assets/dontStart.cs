using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Animator> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
