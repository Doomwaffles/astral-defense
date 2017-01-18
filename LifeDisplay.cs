using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeDisplay : MonoBehaviour {

	//sets the life value as text at the start
	void Start () {
		Text text = GetComponent<Text> ();
		text.text = ("Lives: " + LifeKeeper.lives.ToString());
	}
}