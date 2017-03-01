using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadRateApp : MonoBehaviour {
	public Transform rateAppMessage;
	public Transform canvas;
	// Use this for initialization
	void Start () {
		//ask player to rate the app if they have beaten level 5 and if they have not already been asked
		if (highScoreManager.Instance.beatenLevel5 && highScoreManager.Instance.askedForRating == false) {
			Transform rateApp = Instantiate (rateAppMessage);
			rateApp.SetParent (canvas, false);
			highScoreManager.Instance.askedForRating = true;
		}
	}

}
