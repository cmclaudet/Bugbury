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
		DontDestroyOnLoad (this);
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

			One = currentScores.one;
			Two = currentScores.two;
			Three = currentScores.three;
		}
	}

	public void Save() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/playerScores.dat", FileMode.Open);

		playerScores newScores = new playerScores ();
		newScores.one = One;
		newScores.two = Two;
		newScores.three = Three;

		bf.Serialize (file, newScores);
		file.Close ();
	}
		
}

[Serializable]
class playerScores {
	public highScoreManager.level one { get; set; }
	public highScoreManager.level two { get; set; }
	public highScoreManager.level three { get; set; }
}
