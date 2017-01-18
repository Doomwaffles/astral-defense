using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float padding = 0.5f;
	public float projectileSpeed;
	public float firingRate;
	public float health = 200f;
	public AudioClip fire;
	public AudioClip death;
	public GameObject projectilePrefab;
	public GameObject explosion;

	private LevelManager lvlManager;
	private LifeKeeper lifeKeeper;
	private SpriteRenderer spriteRender;
	private PolygonCollider2D playerCollider;
	private bool canFire = true;

	float xmin;
	float xmax;
	float ymin;
	float ymax;

	//intialization
	void Start () {
		//sets everything up
		lvlManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		lifeKeeper = GameObject.Find ("Lives").GetComponent<LifeKeeper> ();
		spriteRender = gameObject.GetComponent<SpriteRenderer> (); 
		playerCollider = gameObject.GetComponent<PolygonCollider2D> ();

		//sets camera boundaries as positions
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));

		//further defines boundaries
		xmin = leftEdge.x + padding;
		xmax = rightEdge.x - padding;
		ymin = leftEdge.y + padding;
		ymax = rightEdge.y - padding;

	}

	//plays a "fire" sound, then creates a projectile shooting outwards if allowed
	void Fire () {
		if (canFire == true) {
			AudioSource.PlayClipAtPoint (fire, transform.position);
			GameObject projectile = Instantiate (projectilePrefab, transform.position, Quaternion.identity) as GameObject;
			projectile.GetComponent<Rigidbody2D> ().velocity = new Vector3 (projectileSpeed, 0, 0);
		}
	}
		
	//each time the frame is updated
	void Update () {
		//allows movement based on arrow keys or WASD
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		//restricts player to game space
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		float newY = Mathf.Clamp (transform.position.y, ymin, ymax);

		transform.position = new Vector3 (newX, newY, transform.position.z);

		//creates projectiles
		if (Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire", 0.0000001f, firingRate);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("Fire");
		}

	}

	//determines what happens when something collides with the player
	void OnTriggerEnter2D (Collider2D col) {
		Projectile projectile = col.gameObject.GetComponent<Projectile> ();
		//when the object is an enemy projectile, health is lost according to how much damage it's worth
		if (projectile) {
			health -= projectile.GetDamage ();
			projectile.Hit ();
			if (health <= 0) {
				Death ();
			}
		} else {
			Death ();
		}
	}

	//decides what happens on player death
	void Death () {
		//hides the player and keeps them from doing anything
		canFire = false;
		playerCollider.enabled = false;
		spriteRender.enabled = false;
		Instantiate (explosion, transform.position, Quaternion.identity);
		AudioSource.PlayClipAtPoint (death, transform.position);
		if (LifeKeeper.lives > 0) {
			//when the player has lives...
			StartCoroutine (RestartLevel ());
		} else if (LifeKeeper.lives <= 0) {
			//if the player has no lives...
			StartCoroutine (EndGame ());
		}
	}

	//...the level starts over
	IEnumerator RestartLevel () {
		lifeKeeper.Died ();
		yield return new WaitForSeconds (1.5f);
		lvlManager.LoadLastLevel ();
	}

	//...the game ends
	IEnumerator EndGame () {
		yield return new WaitForSeconds (1.5f);
		lvlManager.LoadLevel ("Lose");
	}
}
