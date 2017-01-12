using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caterpillarManager : MonoBehaviour {
	public float spawnFrequency;
	public Rigidbody2D caterpillars;

	private float timeSinceSpawn;
	private GameObject[] allCaterpillars;
	
	// Update is called once per frame
	void Update () {
		timeSinceSpawn += Time.deltaTime;
		allCaterpillars = GameObject.FindGameObjectsWithTag ("caterpillar");

		if (timeSinceSpawn > spawnFrequency) {
			Instantiate (caterpillars);
			timeSinceSpawn = 0;
		}

		for (int i = 0; i < allCaterpillars.Length; i++) {
			float minimumY = allCaterpillars [i].GetComponent<move> ().yMin;
			if (allCaterpillars [i].transform.position.y < minimumY) {
				allCaterpillars [i].gameObject.SetActive (false);
			}
		}
	}
}
