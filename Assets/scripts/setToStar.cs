using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setToStar : MonoBehaviour {
	public Transform star;

	void Awake() {
		GetComponent<RectTransform> ().SetAsLastSibling();
		star.GetComponent<RectTransform> ().SetAsFirstSibling ();
		setFilltoOutline ();
	}

	void setFilltoOutline() {
		GetComponent<RectTransform> ().anchoredPosition = star.GetComponent<RectTransform> ().anchoredPosition;
		GetComponent<RectTransform> ().anchorMax = star.GetComponent<RectTransform> ().anchorMax;
		GetComponent<RectTransform> ().anchorMin = star.GetComponent<RectTransform> ().anchorMin;
		GetComponent<RectTransform> ().offsetMax = star.GetComponent<RectTransform> ().offsetMax;
		GetComponent<RectTransform> ().offsetMin = star.GetComponent<RectTransform> ().offsetMin;
	}
}
