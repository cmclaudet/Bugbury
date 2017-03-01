using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setSortingLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MeshRenderer[] allRenderers = GetComponentsInChildren<MeshRenderer> ();
		foreach (MeshRenderer renderer in allRenderers) {
			renderer.GetComponent<MeshRenderer> ().sortingLayerName = "UI";
			renderer.GetComponent<MeshRenderer> ().sortingOrder = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
