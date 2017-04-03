using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class currentLevelSelectLevel {

	//current level player is looking at on level select screen
	//stored here so level does not return to level 1 when switching between arcade and classic mode
	public static int currentLevel = 1;
}
