using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour {

	private static musicManager _instance;

	public static musicManager Instance {
		get {
			if (_instance == null) {
				GameObject go = new GameObject ("musicManager");
				go.AddComponent<musicManager> ();
			}
			return _instance;
		}
	}

	public bool isPlaying;
	public GameObject music;

	void Awake() {
		_instance = this;
		isPlaying = false;
		DontDestroyOnLoad (this.gameObject);
	}

}