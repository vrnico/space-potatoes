using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletController : MonoBehaviour {

	float speed = 10.0f;

	private Action onKillEnemy;

	public Action OnKillEnemy { set { this.onKillEnemy = value;}}


	void Update () {
		this.transform.position = new Vector3 (
			this.transform.position.x,
			this.transform.position.y + speed * Time.deltaTime,
			this.transform.position.z
			);
		
		if (this.transform.position.y > 6) {
			GameObject.Destroy (this.gameObject);
			}
		}
	public void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.GetComponent<EnemyController> () != null) {
			onKillEnemy ();
			GameObject.Destroy (this.gameObject);
			GameObject.Destroy (collider.gameObject);
		}
	}
}
