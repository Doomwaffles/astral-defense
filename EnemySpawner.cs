using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject enemyPrefab;
	public GameObject bossPrefab;

	public float spawnDelay = 2f;
	public float probability = 0.5f;

	Vector3 enemyPosition;

	float xmax;
	float ymin;
	float ymax;
	float screenEdge = 2f;

	//initializes
	void Start () {
		//defines edges as camera points
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3 (1, 1, distance));

		//defines boundaries for the spawner
		xmax = rightEdge.x + screenEdge;
		ymin = leftEdge.y + 0.5f;
		ymax = rightEdge.y - 0.5f;

		//begins spawning enemies
		StartCoroutine ("SpawnEnemies");

	}

	//creates enemies
	IEnumerator SpawnEnemies () {
		while (true) {
			enemyPosition = new Vector3 (xmax, Random.Range (ymin, ymax));
			yield return new WaitForSeconds (spawnDelay);
			if (Random.value < probability) {
				Debug.Log ("Enemy spawned");
				Instantiate (enemyPrefab, enemyPosition, Quaternion.identity, gameObject.transform);
			}
		}
	}

	//spawns boss, but stops spawning enemies
	public void SpawnBoss () {
		enemyPosition = new Vector3 (xmax, 0);
		Instantiate (bossPrefab, enemyPosition, Quaternion.identity, gameObject.transform);
		StopCoroutine ("SpawnEnemies");
		Debug.Log ("Boss spawned");
	}
}
