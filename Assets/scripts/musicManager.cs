using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*manages when music should stop and start.
  when music should be playing this script is checked to be sure there is not already music playing
*/
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