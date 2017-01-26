using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panMenu1 : MonoBehaviour {
	public Animator pan;
	// Use this for initialization
	void Start () {
		pan.enabled = false;
	}

	public void Press() {
		pan.enabled = true;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
