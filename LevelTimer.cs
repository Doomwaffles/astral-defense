using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelTimer : MonoBehaviour {

	public float levelTime = 60f;

	private Text text;
	private EnemySpawner enemySpawner;

	//level starts, sets everything up
	void Start () {
		enemySpawner = GameObject.Find ("EnemySpawner").GetComponent<EnemySpawner> ();
		text = GetComponent<Text> ();
		text.text = ("Time: " + levelTime);
		InvokeRepeating ("Timer", 1f, 1f);
	}

	//tracks the time passed, then advances the level when it runs out
	void Timer () {
		if (levelTime > 0) {
			levelTime--;
			text.text = ("Time: " + levelTime);
		} else if (levelTime <= 0) {
			text.text = ("BOSS");
			enemySpawner.SpawnBoss ();
			CancelInvoke ("Timer");
		}
	}
}
