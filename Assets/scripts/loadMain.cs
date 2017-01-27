using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//public functions which are attached to buttons to load different scenes
public class loadMain : MonoBehaviour {

	public void loadMainGame () {
		SceneManager.LoadScene ("level 1");
	}

	public void loadSecondlvl () {
		SceneManager.LoadScene ("level 2");
	}

	public void loadChooselvl() {
		SceneManager.LoadScene ("levelSelect");
	}

	public void loadMainMenu() {
		Time.timeScale = 1.0f;
		SceneManager.LoadScene ("title");
	}
}
