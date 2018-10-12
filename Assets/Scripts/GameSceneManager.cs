using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {


	public Camera mainCamera;
	public Text scoreText;
	public Text gameOverText;
	public PlayerController player;
	public EnemyGroupController enemyGroup;

	int score;
	int enemyCount;
	float gameTimer;
	bool gameOver;

	private AudioSource source;
	public AudioClip shootSound;

	void Awake () {

		source = GetComponent<AudioSource> ();
	}

	void Start () {
		Time.timeScale = 1;

		player.OnHitEnemy += OnGameOver;
		player.OnKillEnemy += OnKillEnemy;

		scoreText.enabled = true;
		gameOverText.enabled = false;

		enemyCount = enemyGroup.GetComponentsInChildren<EnemyController> ().Length;
	}

	void Update () {
		if (gameOver) {
			if (Input.GetKeyDown ("r")) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}

			return;
		}

		scoreText.text = "Score: " + score;
	}

	void OnKillEnemy(){

		this.score += 100;
		enemyCount--;

		if (enemyCount == 0) {
			OnGameOver();
		}
	}

	void OnGameOver() {
		gameOver = true;

		scoreText.enabled = false;
		gameOverText.enabled = true;

		if (enemyCount != 0) {
			gameOverText.text = "Game Over!\n Score: " + score + "\n Press R to restart!";
		} else {
			gameOverText.text = "You Win!\nScore: " + score + "\n Press R to restart!";
			source.PlayOneShot (shootSound);
		}

		Time.timeScale = 0;
	}
}
