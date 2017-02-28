using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//set up side walls so that they match the screen size
public class setWallPosition : MonoBehaviour {
	public Transform leftWall;
	public Transform rightWall;

	// Use this for initialization
	void Start () {
		fixWallSizePosition (leftWall, -ScreenVariables.worldWidth);
		fixWallSizePosition (rightWall, ScreenVariables.worldWidth);

	}

	void fixWallSizePosition(Transform wall, float xPos) {
		wall.GetComponent<EdgeCollider2D>().points [0].y = ScreenVariables.worldHeight;
		wall.GetComponent<EdgeCollider2D>().points [1].y = - ScreenVariables.worldHeight;
		wall.transform.position = new Vector3(xPos, 0, 0);

	}

}
