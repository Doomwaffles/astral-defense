using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeKeeper : MonoBehaviour {

	private Text text;

	public static int lives = 3;

	//gives the life keeper access to the life text
	void Start () {
		text = GetComponent<Text> ();
	}

	//decreases lives and updates text
	public void Died () {
		Debug.Log ("Life lost");
		lives--;
		text.text = ("Lives: " + lives.ToString());
	}

	//resets lives
	public static void Reset () {
		lives = 3;
		Debug.Log ("Lives reset.");
	}
}