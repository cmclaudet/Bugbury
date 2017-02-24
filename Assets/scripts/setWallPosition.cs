using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//set up walls so that they match the screen size
public class setWallPosition : MonoBehaviour {
	public Transform leftWall;
	public Transform rightWall;

	// Use this for initialization
	void Start () {
		fixWallSizePosition (leftWall, -ScreenVariables.worldWidth);
		fixWallSizePosition (rightWall, ScreenVariables.worldWidth);
/*
		leftWall.GetComponent<EdgeCollider2D>().points [0].y = screenHeight;
		leftWall.GetComponent<EdgeCollider2D>().points [1].y = - screenHeight;
		leftWall.transform.position = new Vector3(-screenWidth, 0, 0);
		float leftWallWidth = leftWall.GetComponent<SpriteRenderer> ().bounds.size.x;
		leftWall.GetComponent<SpriteRenderer> ().bounds.size = new Vector3 (2 * screenHeight, leftWallWidth, 0);

		rightWall.GetComponent<EdgeCollider2D>().points [0].y = screenHeight;
		rightWall.GetComponent<EdgeCollider2D>().points [1].y = -screenHeight;
		rightWall.transform.position = new Vector3(screenWidth, 0, 0);
		float rightWallWidth = rightWall.GetComponent<SpriteRenderer> ().bounds.size.x;
		rightWall.GetComponent<SpriteRenderer> ().bounds.size = new Vector3 (2 * screenHeight, rightWallWidth, 0);
*/
	}

	void fixWallSizePosition(Transform wall, float xPos) {
		wall.GetComponent<EdgeCollider2D>().points [0].y = ScreenVariables.worldHeight;
		wall.GetComponent<EdgeCollider2D>().points [1].y = - ScreenVariables.worldHeight;
		wall.transform.position = new Vector3(xPos, 0, 0);

/*
		float minX = wall.GetComponent<SpriteRenderer> ().bounds.min.x;
		float maxX = wall.GetComponent<SpriteRenderer> ().bounds.max.x;

		Debug.Log (wall.GetComponent<SpriteRenderer> ().bounds.min.y);
		wall.GetComponent<SpriteRenderer> ().bounds.SetMinMax (new Vector3 (minX, -screenHeight), new Vector3 (maxX, screenHeight));
		Debug.Log (wall.GetComponent<SpriteRenderer> ().bounds.min.y);
*/
	}

}
