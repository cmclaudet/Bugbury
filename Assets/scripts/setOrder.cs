using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used for star outlines to ensure they are drawn above star filling
public class setOrder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<RectTransform> ().SetAsLastSibling ();
	}

}
