using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class detectAndroidBackButton : MonoBehaviour {
	private string thisScene;

	void Start() {
		thisScene = SceneManager.GetActiveScene ().name;
	}

	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				switch (thisScene) {
				case "title":
					Application.Quit ();
					break;
				case "levelSelect":
					SceneManager.LoadScene ("title");
					break;
				case "credits":
					SceneManager.LoadScene ("title");
					break;
				}

				return;
			}
		}
	}
}
