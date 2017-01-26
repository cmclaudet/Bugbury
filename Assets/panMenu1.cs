using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panMenu1 : MonoBehaviour {
	public Animator pan;
	public Transform canvas;
	public Transform level2Select;

	// Use this for initialization
	void Start () {
		pan.enabled = false;
	}

	public void Press() {
		pan.enabled = true;
		Transform lvl2Sel = Instantiate (level2Select);
		lvl2Sel.SetParent (canvas, false);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
