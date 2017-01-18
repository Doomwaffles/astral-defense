using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public float lifeTime = 1f;

	//starts the life timer
	void Start () {
		StartCoroutine ("LifeTimer");
	}

	//waits for the explosion to play, then destroys it
	IEnumerator LifeTimer () {
		yield return new WaitForSeconds (lifeTime);
		Destroy (gameObject);
	}
}
