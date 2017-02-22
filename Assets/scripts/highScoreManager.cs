using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highScoreManager : MonoBehaviour {

	private static highScoreManager _instance;

	public static highScoreManager Instance {
		get {
			if (_instance == null) {
				GameObject go = new GameObject ("highScoreManager");
				go.AddComponent<highScoreManager> ();
			}
			return _instance;
		}
	}
/*
	public int level1HighScore;
	public int level2HighScore;
	public int level3HighScore;

	//stars for level 1
	public bool lvl1Star1 = false;
	public bool lvl1Star2 = false;
	public bool lvl1Star3 = false;

	//stars for level 2
	public bool lvl2Star1 = false;
	public bool lvl2Star2 = false;
	public bool lvl2Star3 = false;

	//stars for level 3
	public bool lvl3Star1 = false;
	public bool lvl3Star2 = false;
	public bool lvl3Star3 = false;
*/
	public struct level
	{
		public int highScore;
		public bool star1;
		public bool star2;
		public bool star3;

		public level(int Score, bool one, bool two, bool three)
		{
			highScore = Score;
			star1 = one;
			star2 = two;
			star3 = three;
		}
	}

	public level One;
	public level Two;
	public level Three;

	void Awake() {
		DontDestroyOnLoad (this);
		_instance = this;

		//all stars start out false
		One = new level (0, false, false, false);
		Two = new level (0, false, false, false);
		Three = new level (0, false, false, false);

	}
}
