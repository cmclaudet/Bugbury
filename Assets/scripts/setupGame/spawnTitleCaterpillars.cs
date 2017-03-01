using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spriter2UnityDX;

//spawns 3 caterpillars every 10 seconds. Checks if caterpillars have gone off screen every 10 seconds as well
public class spawnTitleCaterpillars : MonoBehaviour {
	public Rigidbody2D caterpillarTitle;
	public float spawnFrequency;
	public GameObject splats;

	private AudioSource[] splatSounds;
	private float timeSinceSpawn;
	private Bounds screen;
	private int currentSortingOrder;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1.0f;

		timeSinceSpawn = spawnFrequency;
		splatSounds = splats.GetComponentsInChildren<AudioSource> ();
		//define screen as the bound that caterpillars must be in to prevent being inactivated
		//width + 1 ensures all caterpillar body is off screen before inactivation
		screen = new Bounds (Vector3.zero, new Vector3 (2.0f * ScreenVariables.worldWidth + 1, 2.0f * ScreenVariables.worldHeight));
		currentSortingOrder = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceSpawn += Time.deltaTime;

		//creates 3 caterpillars at a time every 10 seconds
		if (timeSinceSpawn > spawnFrequency) {
			Rigidbody2D caterpillar1 = Instantiate (caterpillarTitle);
			Rigidbody2D caterpillar2 = Instantiate (caterpillarTitle);
			Rigidbody2D caterpillar3 = Instantiate (caterpillarTitle);
			setSortingOrder (new Rigidbody2D[] {caterpillar1, caterpillar2, caterpillar3});
			setSplatSounds (new Rigidbody2D[] {caterpillar1, caterpillar2, caterpillar3});
			timeSinceSpawn = 0;

			inactivateCaterpillars ();
		}
	}

	//set caterpillars on different sorting orders so the different components of their sprites do not overlap eachother
	void setSortingOrder (Rigidbody2D[] caterpillars) {
		foreach (Rigidbody2D caterpillar in caterpillars) {
			caterpillar.GetComponent<EntityRenderer> ().SortingLayerName = "caterpillar";
		}
		caterpillars [0].GetComponent<EntityRenderer> ().SortingOrder = currentSortingOrder;
		caterpillars [1].GetComponent<EntityRenderer> ().SortingOrder = currentSortingOrder + 1;
		caterpillars [2].GetComponent<EntityRenderer> ().SortingOrder = currentSortingOrder + 2;
		currentSortingOrder += 3;
	}

	void setSplatSounds(Rigidbody2D[] caterpillars) {
		foreach (Rigidbody2D caterpillar in caterpillars) {
			caterpillar.GetComponent<splatOnTouch> ().splats = splatSounds;
		}
	}

	//inactivates caterpillars that have gone off screen
	void inactivateCaterpillars() {
		GameObject[] allCaterpillars = GameObject.FindGameObjectsWithTag ("caterpillar");
		foreach (GameObject caterpillar in allCaterpillars) {
			if (!screen.Contains (caterpillar.transform.position)) {
				caterpillar.gameObject.SetActive (false);
			}
		}
	}
}
