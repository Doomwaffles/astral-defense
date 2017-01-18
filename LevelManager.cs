using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	//hides and locks the mouse cursor. Doesn't have to be here, but this script is everywhere
	void Start () {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	//loads level by name
	public void LoadLevel (string name) {
		Debug.Log ("Level load requested for: " + name);
		SceneManager.LoadScene (name);
	}

	//stops game
	public void QuitRequest () {
		Debug.Log ("Quit the game, please!");
		Application.Quit ();
	}

	//advances to the next level
	public void LoadNextLevel () {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	//loads last level
	public void LoadLastLevel () {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);
	}

	//reloads the level
	public void ReloadLevel () {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
		
	void Update () {
		//checks for when the escape key is pressed, then quits the game
		if (Input.GetKeyUp (KeyCode.Escape)) {
			QuitRequest ();
		}

		//advances to the next level, is a debug cheat code
		if (Input.GetKeyDown (KeyCode.Keypad5)) {
			LoadNextLevel ();
		}
	}
}
