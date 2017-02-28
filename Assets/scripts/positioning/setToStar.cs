using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sets star fill to star position. if there is no scaling up sets star fill scale to star silhouette scale
public class setToStar : MonoBehaviour {
	public Transform star;

	void Awake() {
		GetComponent<RectTransform> ().SetAsLastSibling();
		star.GetComponent<RectTransform> ().SetAsFirstSibling ();
	}

	void Start() {
		setFilltoOutline ();
	}

	void setFilltoOutline() {
		GetComponent<RectTransform> ().anchoredPosition = star.GetComponent<RectTransform> ().anchoredPosition;
		GetComponent<RectTransform> ().anchorMax = star.GetComponent<RectTransform> ().anchorMax;
		GetComponent<RectTransform> ().anchorMin = star.GetComponent<RectTransform> ().anchorMin;
		GetComponent<RectTransform> ().offsetMax = star.GetComponent<RectTransform> ().offsetMax;
		GetComponent<RectTransform> ().offsetMin = star.GetComponent<RectTransform> ().offsetMin;

		if (GetComponent<scaleSetup> () == null) {
			GetComponent<RectTransform> ().localScale = star.GetComponent<RectTransform> ().localScale;
		}
	}
}
