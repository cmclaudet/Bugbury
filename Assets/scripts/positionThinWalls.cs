using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script places walls at exact fractional points of the screen's width, ie ensures walls are distributed as they should be over the screen
//ensures devices with different aspect ratios will still have even width for all lanes
public class positionThinWalls : MonoBehaviour {
	//insert indices for x and y position of highest thin wall of the group
	//x indices range from 0 to 6, y indices from 0 to 8 (game has been designed like this)
	public float xIndex;
	public float yIndex;

	private float finishLine;

	// Use this for initialization
	void Start () {
		//grab finish line y position to calculate effective screen length
		finishLine = caterpillarManager.Instance.finishLine;

		//find width of lane from screen width then find world x position by multiplying by x index
		float laneWidth = ScreenVariables.worldWidth / 3.0f;
		float worldXpos = -ScreenVariables.worldWidth + laneWidth * xIndex;

		//find length of 1 unit (given that effective screen height is made up of 8 units)
		//find y position from unit length and y index
		float ySquareLength = (ScreenVariables.worldHeight - finishLine)/8.0f;
		float worldYpos = finishLine + ySquareLength * yIndex;

		transform.position = new Vector3 (worldXpos, worldYpos);
	}

}
