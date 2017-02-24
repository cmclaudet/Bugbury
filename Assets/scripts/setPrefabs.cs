using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setPrefabs : MonoBehaviour {
	public Rigidbody2D rocks;
	public Rigidbody2D caterpillars;
	public Transform gameOverMessage;
	// Use this for initialization
	void Awake () {
		rockManager.Instance.rocks = rocks;
		caterpillarManager.Instance.caterpillars = caterpillars;
	}
}
