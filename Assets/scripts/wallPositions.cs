using UnityEngine;
using System.Collections;

public class wallPositions : MonoBehaviour {
    //sets wall size and position based on screen size
    private EdgeCollider2D[] walls;
    private Vector3 worldScreenDim;

	// Use this for initialization
	void Start () {
        walls = GetComponents<EdgeCollider2D>();
        Vector3 pixelScreenDim = new Vector3(Screen.width, Screen.height, 0.0f);
        worldScreenDim = Camera.main.ScreenToWorldPoint(pixelScreenDim);

		walls[0].offset = new Vector2(2*worldScreenDim.x, 0);
		walls[1].offset = new Vector2(-2*worldScreenDim.x, 0);
     }
	
}
