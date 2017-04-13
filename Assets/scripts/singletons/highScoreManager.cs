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

	[Serializable]
	public class levelArcade
	{
		public string levelName;
		public int highScore;
		public bool star1;
		public bool star2;
		public bool star3;
		public bool perfect;

		public levelArcade(string name, int Score, bool one, bool two, bool three, bool perfectScore)
		{
			levelName = name;
			highScore = Score;
			star1 = one;
			star2 = two;
			star3 = three;
			perfect = perfectScore;
		}
	}

	[Serializable]
	public class levelEndless {
		public string levelName;
		public int highScore;
		public int maxStreak;
		public int farShots;

		public levelEndless(string name, int score, int streak, int far) {
			levelName = name;
			highScore = score;
			maxStreak = streak;
			farShots = far;
		}
	}

	public levelArcade[] arcadeLevels;
	public levelEndless[] endlessLevels;

	//if player has beaten value becomes true. This value must be true for player to be prompted for a rating! 
	//Ensures players have played through some of the game before rating it
	public bool beatenLevel5;		
	public bool askedForRating;		//if player has already been asked for a rating this value becomes true
	private string[] arcadeLevelNames = new string[] {
		"level 1", "level 2", "level 3", "level 4", "level 5",
		"level 6", "level 7", "level 8", "level 9", "level 10",
		"level 11", "level 12", "level 13", "level 14", "level 15"};
	private string[] endlessLevelNames = new string[] {
		"level 1 endless", "level 2 endless", "level 3 endless", "level 4 endless", "level 5 endless",
		"level 6 endless", "level 7 endless", "level 8 endless", "level 9 endless", "level 10 endless",
		"level 11 endless", "level 12 endless", "level 13 endless", "level 14 endless", "level 15 endless"};

	void Awake() {
		_instance = this;
		arcadeLevels = new levelArcade[arcadeLevelNames.Length];
		endlessLevels = new levelEndless[endlessLevelNames.Length];

		//make all arcade levels
		for (int i = 0; i < arcadeLevelNames.Length; i++) {
			arcadeLevels[i] = new levelArcade(arcadeLevelNames[i], 0, false, false, false, false);
		}

		//make all endless levels
		for (int i = 0; i < endlessLevelNames.Length; i++) {
			endlessLevels[i] = new levelEndless(endlessLevelNames[i], 0, 0, 0);
		}

		beatenLevel5 = false;
		askedForRating = false;
	}

	//loads data when script is enabled, ie when game loads
	void OnEnable() {
		if (File.Exists (Application.persistentDataPath + Path.DirectorySeparatorChar + "bugburyScoresV2.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + Path.DirectorySeparatorChar + "bugburyScoresV2.dat", FileMode.Open);
			playerScores currentScores = (playerScores)bf.Deserialize (file);
			file.Close ();

			//can only seem to save and load ints and bools... not custom data types
			arcadeLevels = currentScores.allArcadeLevels;
			endlessLevels = currentScores.allEndlessLevels;

			beatenLevel5 = currentScores.beatenLevel5;
			askedForRating = currentScores.askedForRating;
		}
	}

	//saves data when script is destroyed, ie when player exits the game
	void OnDisable() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + Path.DirectorySeparatorChar + "bugburyScoresV2.dat");

		playerScores newScores = new playerScores ();

		newScores.allArcadeLevels = arcadeLevels;
		newScores.allEndlessLevels = endlessLevels;

		newScores.beatenLevel5 = beatenLevel5;
		newScores.askedForRating = askedForRating;

		bf.Serialize (file, newScores);
		file.Close ();

	}
		
}


[Serializable]
class playerScores {
	public highScoreManager.levelArcade[] allArcadeLevels;
	public highScoreManager.levelEndless[] allEndlessLevels;
	public bool beatenLevel5;
	public bool askedForRating;
}

