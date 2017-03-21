using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/*holds high score data and player star count for all levels
saves and loads data so that player can keep improving high score
Saved data: high score and whether any of the 3 stars have been obtained for each level */
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
		public bool perfect;
		public int HSEndless;
		public int MSEndless;
		public int FSEndless;

		public level(int Score, bool one, bool two, bool three, bool perfectScore, int scoreEndless, int maxStreakEnd, int farShotEnd)
		{
			highScore = Score;
			star1 = one;
			star2 = two;
			star3 = three;
			perfect = perfectScore;
			HSEndless = scoreEndless;
			MSEndless = maxStreakEnd;
			FSEndless = farShotEnd;
		}
	}

	public level One;
	public level Two;
	public level Three;
	public level Four;
	public level Five;

	//if player has beaten value becomes true. This value must be true for player to be prompted for a rating! 
	//Ensures players have played through the game before beating it
	public bool beatenLevel5;		
	public bool askedForRating;		//if player has already been asked for a rating this value becomes true

	void Awake() {
		_instance = this;

		//all stars start out false
		One = new level (0, false, false, false, false, 0, 0, 0);
		Two = new level (0, false, false, false, false, 0, 0, 0);
		Three = new level (0, false, false, false, false, 0, 0, 0);
		Four = new level (0, false, false, false, false, 0, 0, 0);
		Five = new level (0, false, false, false, false, 0, 0, 0);

		beatenLevel5 = false;
		askedForRating = false;
	}

	//loads data when script is enabled, ie when game loads
	void OnEnable() {
		if (File.Exists (Application.persistentDataPath + Path.DirectorySeparatorChar + "bugburyScores.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + Path.DirectorySeparatorChar + "bugburyScores.dat", FileMode.Open);
			playerScores currentScores = (playerScores)bf.Deserialize (file);
			file.Close ();

			//can only seem to save and load ints and bools... not custom data types
			One.highScore = currentScores.highScore1;
			Two.highScore = currentScores.highScore2;
			Three.highScore = currentScores.highScore3;
			Four.highScore = currentScores.highScore4;
			Five.highScore = currentScores.highScore5;

			One.star1 = currentScores.lvl1star1;
			Two.star1 = currentScores.lvl2star1;
			Three.star1 = currentScores.lvl3star1;
			Four.star1 = currentScores.lvl4star1;
			Five.star1 = currentScores.lvl5star1;

			One.star2 = currentScores.lvl1star2;
			Two.star2 = currentScores.lvl2star2;
			Three.star2 = currentScores.lvl3star2;
			Four.star2 = currentScores.lvl4star2;
			Five.star2 = currentScores.lvl5star2;

			One.star3 = currentScores.lvl1star3;
			Two.star3 = currentScores.lvl2star3;
			Three.star3 = currentScores.lvl3star3;
			Four.star3 = currentScores.lvl4star3;
			Five.star3 = currentScores.lvl5star3;

			One.perfect = currentScores.perfect1;
			Two.perfect = currentScores.perfect2;
			Three.perfect = currentScores.perfect3;
			Four.perfect = currentScores.perfect4;
			Five.perfect = currentScores.perfect5;

			One.HSEndless = currentScores.endlessHS1;
			Two.HSEndless = currentScores.endlessHS2;
			Three.HSEndless = currentScores.endlessHS3;
			Four.HSEndless = currentScores.endlessHS4;
			Five.HSEndless = currentScores.endlessHS5;

			One.MSEndless = currentScores.maxStreakEnd1;
			Two.MSEndless = currentScores.maxStreakEnd2;
			Three.MSEndless = currentScores.maxStreakEnd3;
			Four.MSEndless = currentScores.maxStreakEnd4;
			Five.MSEndless = currentScores.maxStreakEnd5;

			One.FSEndless = currentScores.farShotEnd1;
			Two.FSEndless = currentScores.farShotEnd2;
			Three.FSEndless = currentScores.farShotEnd3;
			Four.FSEndless = currentScores.farShotEnd4;
			Five.FSEndless = currentScores.farShotEnd5;

			beatenLevel5 = currentScores.beatenLevel5;
			askedForRating = currentScores.askedForRating;
		}
	}

	//saves data when script is destroyed, ie when player exits the game
	void OnDisable() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + Path.DirectorySeparatorChar + "bugburyScores.dat");

		playerScores newScores = new playerScores ();

		newScores.highScore1 = One.highScore;
		newScores.highScore2 = Two.highScore;
		newScores.highScore3 = Three.highScore;
		newScores.highScore4 = Four.highScore;
		newScores.highScore5 = Five.highScore;

		newScores.lvl1star1 = One.star1;
		newScores.lvl2star1 = Two.star1;
		newScores.lvl3star1 = Three.star1;
		newScores.lvl4star1 = Four.star1;
		newScores.lvl5star1 = Five.star1;

		newScores.lvl1star2 = One.star2;
		newScores.lvl2star2 = Two.star2;
		newScores.lvl3star2 = Three.star2;
		newScores.lvl4star2 = Four.star2;
		newScores.lvl5star2 = Five.star2;

		newScores.lvl1star3 = One.star3;
		newScores.lvl2star3 = Two.star3;
		newScores.lvl3star3 = Three.star3;
		newScores.lvl4star3 = Four.star3;
		newScores.lvl5star3 = Five.star3;

		newScores.perfect1 = One.perfect;
		newScores.perfect2 = Two.perfect;
		newScores.perfect3 = Three.perfect;
		newScores.perfect4 = Four.perfect;
		newScores.perfect5 = Five.perfect;

		newScores.endlessHS1 = One.HSEndless;
		newScores.endlessHS2 = Two.HSEndless;
		newScores.endlessHS3 = Three.HSEndless;
		newScores.endlessHS4 = Four.HSEndless;
		newScores.endlessHS5 = Five.HSEndless;

		newScores.maxStreakEnd1 = One.MSEndless;
		newScores.maxStreakEnd2 = Two.MSEndless;
		newScores.maxStreakEnd3 = Three.MSEndless;
		newScores.maxStreakEnd4 = Four.MSEndless;
		newScores.maxStreakEnd5 = Five.MSEndless;

		newScores.farShotEnd1 = One.FSEndless;
		newScores.farShotEnd2 = Two.FSEndless;
		newScores.farShotEnd3 = Three.FSEndless;
		newScores.farShotEnd4 = Four.FSEndless;
		newScores.farShotEnd5 = Five.FSEndless;

		newScores.beatenLevel5 = beatenLevel5;
		newScores.askedForRating = askedForRating;

		bf.Serialize (file, newScores);
		file.Close ();

	}
		
}


[Serializable]
class playerScores {
	public int highScore1;
	public int highScore2;
	public int highScore3;
	public int highScore4;
	public int highScore5;

	public bool lvl1star1;
	public bool lvl1star2;
	public bool lvl1star3;

	public bool lvl2star1;
	public bool lvl2star2;
	public bool lvl2star3;

	public bool lvl3star1;
	public bool lvl3star2;
	public bool lvl3star3;

	public bool lvl4star1;
	public bool lvl4star2;
	public bool lvl4star3;

	public bool lvl5star1;
	public bool lvl5star2;
	public bool lvl5star3;

	public bool perfect1;
	public bool perfect2;
	public bool perfect3;
	public bool perfect4;
	public bool perfect5;

	public int endlessHS1;
	public int endlessHS2;
	public int endlessHS3;
	public int endlessHS4;
	public int endlessHS5;

	public int maxStreakEnd1;
	public int maxStreakEnd2;
	public int maxStreakEnd3;
	public int maxStreakEnd4;
	public int maxStreakEnd5;

	public int farShotEnd1;
	public int farShotEnd2;
	public int farShotEnd3;
	public int farShotEnd4;
	public int farShotEnd5;

	public bool beatenLevel5;
	public bool askedForRating;
}

