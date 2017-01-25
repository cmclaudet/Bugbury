using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setOrder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<RectTransform> ().SetAsLastSibling ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
