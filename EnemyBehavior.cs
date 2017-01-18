using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	public float health;
	public float projectileSpeed;
	public float shotsPerSecond;
	public float speed;
	public int scoreValue;
	public GameObject projectilePrefab;
	public GameObject explosion;
	public AudioClip fire;
	public AudioClip death;

	private ScoreKeeper scoreKeeper;

	//gives the enemy access to the score
	void Start () {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	//reduces health when the player shoots the enemy
	void OnTriggerEnter2D (Collider2D col) {
		Projectile missile = col.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage ();
			missile.Hit ();
			if (health <= 0) {
				Death ();
			}
		}
	}

	//plays a death sound and animation, destroys the enemy, and increases the score
	void Death () {
		Instantiate (explosion, transform.position, Quaternion.identity);
		AudioSource.PlayClipAtPoint (death, transform.position);
		Destroy (gameObject);
		scoreKeeper.Score (scoreValue);
		Debug.Log (scoreValue + " points earned");
	}

	//creates a projectile at its position and plays a sound
	void Fire () {
		AudioSource.PlayClipAtPoint (fire, transform.position);

		GameObject projectile = Instantiate (projectilePrefab, transform.position, Quaternion.identity) as GameObject;
		projectile.GetComponent<Rigidbody2D> ().velocity = new Vector3 (-projectileSpeed, 0, 0);
	}

	//randomly shoots based on a set time, and becomes more likely as time passes
	void Update () {
		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability) {
			Fire ();
		}
		transform.position += Vector3.left * speed * Time.deltaTime;
	}
}
