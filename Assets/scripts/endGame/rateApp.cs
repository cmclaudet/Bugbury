using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rateApp : MonoBehaviour {

	public void rateThisApp() {
		Application.OpenURL ("market://details?id=com.cmclaudet.Bugbury");
		gameObject.SetActive (false);
		highScoreManager.Instance.askedForRating = true;
	}
}
