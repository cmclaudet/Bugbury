using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sets position, direction and speed of caterpillars on the title screen
public class randomWalk : MonoBehaviour {
	public float speed;
	private float caterpillarLength;

	// Use this for initialization
	void Start () {
		caterpillarLength = transform.TransformPoint (new Vector3 (0, GetComponent<BoxCollider2D> ().size.y)).y;;

		int spawnSide = Random.Range (0, 4);

		Vector3 position = genPos (spawnSide);
		transform.position = position;

		//Set velocity
		Vector3 direction = genDir(spawnSide, position);
		GetComponent<Rigidbody2D>().velocity = direction * speed;
		setRotation (direction);
	}

	/*Function generates a random position for body outside one of the four possible edges.
    Random number between 1 and 4 to represent the four edges is generated.
    For each edge random position along the edge is generated in posList.*/
	Vector3 genPos(int side)
	{
		//offsets defined as the distance between screen centre ([0,0,0] in world space) and edges
		float yOffset = ScreenVariables.worldHeight + caterpillarLength/2.0f;
		float xOffset = ScreenVariables.worldWidth + caterpillarLength/2.0f;

		float[,] posList = 
			new float[4, 2] { {-xOffset, Random.Range(-yOffset, yOffset) },   //left
			{xOffset, Random.Range(-yOffset, yOffset) },    //right
			{Random.Range(-xOffset, xOffset), -yOffset },   //bottom
			{Random.Range(-xOffset, xOffset), yOffset } };  //top

		float xpos = posList[side, 0];
		float ypos = posList[side, 1];
		Vector3 position = new Vector3(xpos, ypos);
		return position;
	}

	/*Generates direction of movement based on which side caterpillar comes from and where along the side it spawns.
    Written such that caterpillars will always move inwards: upper and lower bounds for direction change with caterpillar's position*/
	Vector2 genDir(int side, Vector3 position)
	{   
		//First bound for angle of movement for each side
		float[] angleBounds =
			new float[4] { 45 + 45*position.y/(ScreenVariables.worldHeight),   //left 
			-135 - 45*position.y/ScreenVariables.worldHeight,   //right
			-45 - 45*position.x/ScreenVariables.worldWidth,    //bottom
			135 + 45*position.x/ScreenVariables.worldWidth};   //top

		float bound1 = angleBounds[side];
		float bound2 = bound1 + 90; //second bound always 90 degrees more

		float angDeg = Random.Range(bound1, bound2); //Actual movement angle must lie somewhere between bounds
		float angRad = angDeg * Mathf.PI / 180.0f;

		//Get x,y direction from caterpillar's movement angle
		float dirx = Mathf.Sin(angRad);
		float diry = Mathf.Cos(angRad);
		Vector3 direction = new Vector3(dirx, diry);

		return direction;
	}

	//rotates caterpillar body according to its direction of movement
	//ensures caterpillar is facing the right direction
	void setRotation(Vector3 direction) {
		float angle1 = Mathf.Atan (Mathf.Abs(direction.x) / Mathf.Abs(direction.y));
		float angleDegrees1 = 180.0f * angle1 / Mathf.PI;

		float angle2 = Mathf.Atan (Mathf.Abs(direction.y) / Mathf.Abs(direction.x));
		float angleDegrees2 = 180.0f * angle2 / Mathf.PI;

		if (direction.x > 0) {
			if (direction.y < 0) {
				transform.Rotate (new Vector3 (0, 0, angleDegrees1));
			} else {
				transform.Rotate (new Vector3 (0, 0, 90 + angleDegrees2));
			}
		} else {
			if (direction.y > 0) {
				transform.Rotate (new Vector3 (0, 0, 180 + angleDegrees1));
			} else {
				transform.Rotate (new Vector3 (0, 0, 270 + angleDegrees2));
			}
		}
	}

}
