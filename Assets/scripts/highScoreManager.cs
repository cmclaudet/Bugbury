using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highScoreManager : MonoBehaviour {

	private static highScoreManager _instance;

	public static highScoreManager Instance {
		get {
			if (_instance == null) {
				GameObject go = new GameObject ("highScoreManager");
				go.AddComponent<highScoreManager> ();
			}
			return _instance;
		}
	}

	public int level1HighScore;
	public int level2HighScore;
	public int level3HighScore;

	void Awake() {
		DontDestroyOnLoad (this);
		_instance = this;
	}
}
