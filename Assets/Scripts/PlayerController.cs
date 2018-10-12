using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public delegate void OnHitEnemyAction ();
	public delegate void OnKillEnemyAction ();

	public OnHitEnemyAction OnHitEnemy;
	public OnKillEnemyAction OnKillEnemy;

	float speed = 6;
	float shootCooldown = 0.5f;

	private AudioSource source;
	public AudioClip shootSound;

	public GameObject bulletPrefab;

	float shootTimer = 0;

	void Update () {
		shootTimer -= Time.deltaTime;

		processInput();
	}

	void Awake () {

		source = GetComponent<AudioSource> ();
	}


	void processInput () {
		if (Input.GetKey("left") || Input.GetKey("a")){
			if (this.transform.position.x > - 5.5){
				this.transform.position = new Vector3 (
					this.transform.position.x - speed * Time.deltaTime,
					this.transform.position.y,
					this.transform.position.z
				);
			}
		}

		if (Input.GetKey ("right") || Input.GetKey ("d")) {
			if (this.transform.position.x < 5.5) {
				this.transform.position = new Vector3(

					this.transform.position.x + speed * Time.deltaTime,
					this.transform.position.y,
					this.transform.position.z
				
				);
			}
		}

		if (Input.GetKeyDown ("space") || Input.GetKeyDown ("k")) {
			

			if (shootTimer <= 0.0f) {
				shootTimer = shootCooldown;
				shoot ();
				source.PlayOneShot (shootSound);
			}
		}
	}

	void shoot(){
		if (Time.timeScale == 0)
			return;
		GameObject bulletObject = GameObject.Instantiate<GameObject> (bulletPrefab);
		bulletObject.transform.SetParent (this.transform.parent);
		bulletObject.transform.position = this.transform.position;
		bulletObject.GetComponent<BulletController> ().OnKillEnemy = () => {
			if (this.OnKillEnemy != null) {
				this.OnKillEnemy ();
			}
		};
	}

	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.GetComponent<EnemyController> () != null) {
			if (OnHitEnemy != null) {
				OnHitEnemy ();
			}
		}
	}
}
