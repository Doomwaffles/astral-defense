using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Leaderboard : MonoBehaviour {

	private Text text;

	int firstScore;
	int secondScore;
	int thirdScore;
	int fourthScore;
	int fifthScore;

	//initialization
	void Start () {
		text = gameObject.GetComponent<Text> ();
		UpdateScores ();
	}

	//changes the scores to the ones in playerprefs
	void UpdateScores () {
		firstScore = PlayerPrefs.GetInt ("0highScore");
		secondScore = PlayerPrefs.GetInt ("1highScore");
		thirdScore = PlayerPrefs.GetInt ("2highScore");
		fourthScore = PlayerPrefs.GetInt ("3highScore");
		fifthScore = PlayerPrefs.GetInt ("4highScore");

		//calls for the text to be changed
		UpdateLeaderboard ();
	}

	//changes the text to the list of scores
	void UpdateLeaderboard () {
		text.text = (firstScore + "\n" +
		secondScore + "\n" + thirdScore +
		"\n" + fourthScore + "\n" +
		fifthScore);
	}
}
