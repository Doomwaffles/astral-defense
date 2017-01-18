using UnityEngine;
using System.Collections;

public class BossBehavior : MonoBehaviour {

	public float health;
	public float projectileSpeed;
	public float shotsPerSecond;
	public float speed;
	public int scoreValue;
	public GameObject projectilePrefab;
	public GameObject explosion;
	public AudioClip fire;
	public AudioClip death;
	public AudioClip explode;

	private bool canShoot = true;
	private ScoreKeeper scoreKeeper;
	private LevelManager lvlManager;
	private SpriteRenderer spriteRender;
	private PolygonCollider2D bossCollider;

	//gives the boss access to everything it needs
	void Start () {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
		lvlManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		spriteRender = gameObject.GetComponent<SpriteRenderer> (); 
		bossCollider = gameObject.GetComponent<PolygonCollider2D> ();
	}

	//randomly shoots based on a set time, and becomes more likely as time passes
	void Update () {
		if (canShoot == true) {
			float probability = Time.deltaTime * shotsPerSecond;
			if (Random.value < probability) {
				Fire ();
			}
		}

		//moves the boss towards its correct spot
		Vector3 finalPosition = new Vector3 (4, 0);
		transform.position = Vector3.MoveTowards (transform.position, finalPosition, speed * Time.deltaTime);
	}

	//reduces health when the player shoots the boss
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

	//plays a death sound, hides the boss, and increases the score before advancing
	void Death () {
		canShoot = false;
		bossCollider.enabled = false;
		spriteRender.enabled = false;
		AudioSource.PlayClipAtPoint (death, transform.position);
		scoreKeeper.Score (scoreValue);
		Debug.Log (scoreValue + " points earned");
		InvokeRepeating ("Explosion", 0.00001f, 0.3f);
		StartCoroutine ("AdvanceGame");
	}

	//makes the death explosions
	void Explosion () {
		Vector3 explosionPosition = new Vector3 (Random.Range (3, 6), Random.Range (-3, 3));
		Instantiate (explosion, explosionPosition, Quaternion.identity);
		AudioSource.PlayClipAtPoint (explode, explosionPosition);
	}

	//creates a projectile around its position and plays a sound
	void Fire () {
		float shootRange = (Random.Range (transform.position.y - 2, transform.position.y + 2));
		Vector3 shootPosition = new Vector3 (transform.position.x, shootRange);
		AudioSource.PlayClipAtPoint (fire, transform.position);

		GameObject projectile = Instantiate (projectilePrefab, shootPosition, Quaternion.identity) as GameObject;
		projectile.GetComponent<Rigidbody2D> ().velocity = new Vector3 (-projectileSpeed, 0, 0);
	}

	//waits, then advances the game
	IEnumerator AdvanceGame () {
		yield return new WaitForSeconds (3.5f);
		CancelInvoke ("Explosion");
		yield return new WaitForSeconds (0.5f);
		lvlManager.LoadNextLevel ();
	}
}
