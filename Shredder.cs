using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	//destroys anything that touches it
	void OnTriggerEnter2D (Collider2D col) {
		Destroy (col.gameObject);
	}
}
