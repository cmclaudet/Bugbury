using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifeManager : MonoBehaviour {

	private static lifeManager _instance;

	public static lifeManager Instance {
		get {
			if (_instance == null) {
				GameObject go = new GameObject ("lifeManager");
				go.AddComponent<lifeManager> ();
			}
			return _instance;
		}
	}


	public GameObject camera{ get; set; }

	public bool lifeLost{ get; set; }
	private int lives;
	public bool control{ get; set; }

	void Awake() {
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		lives = 3;
		lifeLost = false;
		control = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (lifeLost) {
			lives -= 1;
			camera.GetComponent<detectLifeLoss> ().lifeLost (lives);
			lifeLost = false;
		}
	}

}
