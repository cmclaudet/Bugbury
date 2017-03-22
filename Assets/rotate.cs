using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

	private float rotationSpeed = 15.0f;
	private Vector3 rotationEuler;

	// Update is called once per frame
	void Update () {
		rotationEuler+= Vector3.forward*rotationSpeed*Time.deltaTime; //increment 30 degrees every second
		transform.localRotation = Quaternion.Euler(rotationEuler);
	}
}
