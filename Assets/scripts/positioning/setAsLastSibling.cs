using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setAsLastSibling : MonoBehaviour {
	

	void Update() {
		if (GetComponent<scaleSetup> ().needScaling) {
			GetComponent<RectTransform> ().SetAsLastSibling();
		}
	}

}
