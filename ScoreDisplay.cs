using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	//sets the scorekeeper value as text at the start
	void Start () {
		Text text = GetComponent<Text> ();
		text.text = ScoreKeeper.score.ToString ();
	}
}
