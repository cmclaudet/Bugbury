using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//triggers level complete message when level is done
//is triggered from caterpillar manager when all caterpillars have been inactivated
public class triggerLevelComplete : MonoBehaviour {
	public new GameObject camera;
	public Button pauseButton;
	public Transform completeMessage;
	public Transform canvas;
	public AudioSource bam;

	void Awake() {
		caterpillarManager.Instance.levelComplete = gameObject;
	}

	//triggered from caterpillar manager singleton
	public void setupEnd() {
		camera.GetComponent<CameraShake> ().shakeDuration = caterpillarManager.Instance.cameraScoreNumShakeDuration;
		lifeManager.Instance.control = false;
		pauseButton.interactable = false;

		caterpillarManager.Instance.resetMaxStreak ();
		Transform levelDone = Instantiate (completeMessage);
		levelDone.transform.SetParent (canvas, false);
	}
	

}
