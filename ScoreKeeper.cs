using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	private Text text;
	private string levelName;

	public static int score = 0;

	int newScore;
	int oldScore;


	//gives the score keeper access to the score text
	void Start () {
		text = GetComponent<Text> ();
	}

	//increases score and updates text
	public void Score (int points) {
		Debug.Log ("Scored points");
		score += points;
		text.text = score.ToString();
	}

	//resets score
	public static void Reset () {
		score = 0;
		Debug.Log ("Score reset.");
	}

	//checks to see what level is loaded, if an end scene, saves the score
	void OnLevelWasLoaded () {
		levelName = (SceneManager.GetActiveScene ().name);
		if (levelName == "Win" || levelName == "Lose") {
			AddScore (score);
		}
	}

	//tracks the high scores
	public void AddScore (int score) {
		newScore = score;
		Debug.Log ("AddScore called.");
		//only saves the first 5 scores
		for (int i = 0; i < 5; i++) {
			//compares the keys
			if (PlayerPrefs.HasKey (i + "highScore")) {
				//compares the scores
				if (PlayerPrefs.GetInt (i + "highScore") < newScore) {
					//replaces the old score with the new one, and bumps the old one down
					oldScore = PlayerPrefs.GetInt (i + "highScore");
					PlayerPrefs.SetInt (i + "highScore", newScore);
					newScore = oldScore;
					Debug.Log ("AddScore loop done");
				}
			} else {
				//sets the score if there's nothing in its place
				PlayerPrefs.SetInt (i + "highScore", newScore);
				newScore = 0;
			}
		}
	}
}