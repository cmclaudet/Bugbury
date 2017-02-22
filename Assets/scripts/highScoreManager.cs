using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
//		DontDestroyOnLoad (this);
		_instance = this;

		//all stars start out false
		One = new level (0, false, false, false);
		Two = new level (0, false, false, false);
		Three = new level (0, false, false, false);

	}

	public void Load() {
		if (File.Exists (Application.persistentDataPath + "/playerScores.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerScores.dat", FileMode.Open);
			playerScores currentScores = (playerScores)bf.Deserialize (file);
			file.Close ();

			One.highScore = currentScores.highScore1;
			Two.highScore = currentScores.highScore2;
			Three.highScore = currentScores.highScore3;

			One.star1 = currentScores.lvl1star1;
			Two.star1 = currentScores.lvl2star1;
			Three.star1 = currentScores.lvl3star1;

			One.star2 = currentScores.lvl1star2;
			Two.star2 = currentScores.lvl2star2;
			Three.star2 = currentScores.lvl3star2;

			One.star3 = currentScores.lvl1star3;
			Two.star3 = currentScores.lvl2star3;
			Three.star3 = currentScores.lvl3star3;
		}
	}

	public void Save() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerScores.dat");

		playerScores newScores = new playerScores ();
		newScores.highScore1 = One.highScore;
		newScores.highScore2 = Two.highScore;
		newScores.highScore3 = Three.highScore;

		newScores.lvl1star1 = One.star1;
		newScores.lvl2star1 = Two.star1;
		newScores.lvl3star1 = Three.star1;

		newScores.lvl1star2 = One.star2;
		newScores.lvl2star2 = Two.star2;
		newScores.lvl3star2 = Three.star2;

		newScores.lvl1star3 = One.star3;
		newScores.lvl2star3 = Two.star3;
		newScores.lvl3star3 = Three.star3;

		bf.Serialize (file, newScores);
		file.Close ();
	}
		
}

[Serializable]
class playerScores {
	public int highScore1;
	public int highScore2;
	public int highScore3;

	public bool lvl1star1;
	public bool lvl1star2;
	public bool lvl1star3;

	public bool lvl2star1;
	public bool lvl2star2;
	public bool lvl2star3;

	public bool lvl3star1;
	public bool lvl3star2;
	public bool lvl3star3;
}

/*
class playerScores {
	public highScoreManager.level one { get; set; }
	public highScoreManager.level two { get; set; }
	public highScoreManager.level three { get; set; } 
} */
