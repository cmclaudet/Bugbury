using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadMain : MonoBehaviour {

	public void loadMainGame () {
		SceneManager.LoadScene ("level 1");
	}

	public void loadChooselvl2() {
		SceneManager.LoadScene ("levelSelect 2");
	}
}
