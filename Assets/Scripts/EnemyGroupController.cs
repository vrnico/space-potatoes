using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupController : MonoBehaviour {

	float horizontalMoveAmount = 0.25f;
	float verticalMoveAmount = 0.25f;
	float maximumInterval = 0.3f;
	float minimumInterval = 0.025f;
	float rightBound = 5.5f;
	float leftBound = -5.5f;

	private EnemyController[] enemies;
	float moveTimer;
	int totalEnemies;
	bool movingRight;

	void Start () {
		enemies = this.GetComponentsInChildren<EnemyController> ();

		totalEnemies = enemies.Length;
	}
	

	public void Update () {
		if (Time.timeScale == 0)
			return;
		moveTimer -= Time.deltaTime;

		if (moveTimer <= 0.0f) {
			enemies = this.GetComponentsInChildren<EnemyController> ();

			int enemyCount = enemies.Length;

			float difficultyPercentage = 1 - ((float)enemyCount / totalEnemies);
			float interval = maximumInterval + (minimumInterval - maximumInterval) * difficultyPercentage;
		
			moveTimer = interval;

			float minimumX = 0;
			float maximumX = 0;

			foreach (EnemyController enemy in enemies) {
				if (enemy.transform.position.x < minimumX)
					minimumX = enemy.transform.position.x;
				else if (enemy.transform.position.x > maximumX)
					maximumX = enemy.transform.position.x;
			}

			if (movingRight && maximumX >= rightBound || !movingRight && minimumX <= leftBound) {
				this.transform.position = new Vector3 (
					this.transform.position.x,
					this.transform.position.y - verticalMoveAmount,
					this.transform.position.z
				);

				movingRight = !movingRight;


			} else {
				this.transform.position = new Vector3 (
					this.transform.position.x + horizontalMoveAmount * (movingRight ? 1: -1),
					this.transform.position.y,
					this.transform.position.z
				);
			}
		}
	}
}
