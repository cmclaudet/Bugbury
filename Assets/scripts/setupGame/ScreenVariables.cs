using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sets screen dimensions for easy access throughout the game
[System.Serializable]
public class ScreenVariables {
	public static float worldHeight = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height, 0)).y;
	public static float worldWidth = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 0)).x;
}
