using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*stores number of lives player has. When life has been lost lifeLost function in camera script is triggeres
 *stores whether player can control slingshot or not
 */
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


	public new GameObject camera{ get; set; }

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
