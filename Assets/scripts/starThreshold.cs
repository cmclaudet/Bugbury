using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//calculates the score thresholds for getting stars
public class starThreshold : MonoBehaviour {
	/*fractions are the necessary fraction of caterpillars out of the total which player needs to get a streak from
	ie, star1Frac = 3, player (on average!!) needs to get at least 3 straight streaks. Thus average top streak will be 1/3 of total caterpillars.
	number of far shots necessary will also depend on this, eg: frac = 3, need 1/3 of the total to be far shots
	Therefore frac cannot be less than 1 (otherwise one would need an impossible score to get the star)
	frac is set such that one cannot obtain all stars without any far shots or any streak shots, thus player must try to obtain both */
	public float star1Frac;
	public float star2Frac;
	public float star3Frac;

	private int totalCaterpillars;
	private int farShotBonus;

	private int threshold1;
	private int threshold2;
	private int threshold3;

	// Use this for initialization
	void Start () {
		totalCaterpillars = caterpillarManager.Instance.totalCaterpillars;
		farShotBonus = scoreCount.Instance.farShotBonus;

		//ensure threhsolds are only set if they are attainable
		if (checkFrac ()) {
			threshold1 = calcThreshold (star1Frac);
			threshold2 = calcThreshold (star2Frac);
			threshold3 = calcThreshold (star3Frac);

		} else {
			Debug.Log ("Required score threshold is too high for star!");
		}

		setStarThresholds ();
	}
		
	bool checkFrac() {
		if (star1Frac < 1 || star2Frac < 1 || star3Frac < 1) {
			return false;
		} else {
			return true;
		}
	}

	//calculates what score one would get from average combo (AC) of total caterpillars/frac and with 1/frac hits being far shots
	int calcThreshold(float frac) {
		float AC = (float)totalCaterpillars / frac;
		float apxScore = (AC * (AC + 1.0f) / 2.0f) * frac + AC*farShotBonus;
		int scoreThreshold = Mathf.FloorToInt(apxScore);
		return scoreThreshold;
	}

	//set star thresholds in singleton so that other scripts have easy access to it
	void setStarThresholds() {
		scoreCount.Instance.star1threshold = threshold1;
		scoreCount.Instance.star2threshold = threshold2;
		scoreCount.Instance.star3threshold = threshold3;
	}
}
