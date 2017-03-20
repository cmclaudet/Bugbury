using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawPointer : MonoBehaviour {
	
	private GameObject springAnchor;
	private SpringJoint2D spring;
	private GameObject slingshot;
	private float startPosition;
	private float velocityMagnitude;

	public GameObject actualRock{get;set;}
	private bool pastSlingshot = false;

	private List<Vector3> wallsInPath = new List<Vector3>();
	// Use this for initialization
	void Start () {
		springAnchor = rockManager.Instance.springAnchor;
		slingshot = rockManager.Instance.slingshotLeft;
		spring = GetComponent<SpringJoint2D> ();
		GetComponent<SpringJoint2D> ().connectedBody = springAnchor.GetComponent<Rigidbody2D> ();

		GetComponent<Rigidbody2D> ().mass = 0.0001f;
		transform.position = actualRock.transform.position;
		velocityMagnitude = 5.0f*actualRock.GetComponent<projectileShoot> ().velocityMagnitude;

		GetComponent<LineRenderer> ().SetPosition (0, transform.position);
		wallsInPath.Add (transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (!pastSlingshot) {
			shoot ();
		}
		if (transform.position.y > slingshot.transform.position.y) {
			launch ();
			pastSlingshot = true;
		}

		GetComponent<LineRenderer> ().numPositions = wallsInPath.Count;
		GetComponent<LineRenderer> ().SetPositions (wallsInPath.ToArray ());

		if (!actualRock.gameObject.activeSelf) {
			gameObject.SetActive (false);
		}
	}

	void shoot() {
		//spring physics is enabled
		spring.enabled = true;
		GetComponent<SpringJoint2D> ().enabled = true;
		GetComponent<Rigidbody2D> ().isKinematic = false;
	}

	void launch() {
		//once rock has passed over the slingshot position, spring and line renderers are disabled.
		//Velocity is set to magnitude specified above
		GetComponent<SpringJoint2D> ().enabled = false;
		GetComponent<Rigidbody2D> ().velocity = velocityMagnitude * GetComponent<Rigidbody2D> ().velocity.normalized;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.CompareTag("wall")) {
			wallsInPath.Add(transform.position);
		}
	}
}
