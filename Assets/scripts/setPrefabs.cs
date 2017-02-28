using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//place on gameobject in hierarchy so that singletons can have access to prefabs
public class setPrefabs : MonoBehaviour {
	public Rigidbody2D rocks;
	public Rigidbody2D caterpillars;

	// Use this for initialization
	void Awake () {
		rockManager.Instance.rocks = rocks;
		caterpillarManager.Instance.caterpillars = caterpillars;
	}
}
