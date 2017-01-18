using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float damage;

	//sets damage
	public float GetDamage() {
		return damage;
	}

	//removes projectile when it collides with something
	public void Hit () {
		Destroy (gameObject);
	}
}
