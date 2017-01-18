using UnityEngine;
using System.Collections;

public class SplashAdvance : MonoBehaviour {

	private LevelManager lvlManager;

	//initialization
	void Start () {
		lvlManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		StartCoroutine ("Wait");
	}

	//waits 2 seconds, then advances to the level
	IEnumerator Wait () {
		yield return new WaitForSeconds (2f);
		lvlManager.LoadNextLevel ();
	}
}
