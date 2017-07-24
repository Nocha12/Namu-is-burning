using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float playerMoveSpeed = 4.0f;
	public bool playerLeftMoveCheck = false;
	public bool playerRightMoveCheck = false;
	float attackTimer = 0;
	float attackDelay = 0.5f;

	public float playerAttackFloat;

	public PlayerAttackRange attackRange;

	public GameObject target;

	void Update () {
		attackTimer += Time.deltaTime;
		if (target) {
			if (Vector2.Distance (target.transform.position, transform.position) < 2.3f) {
				if (target.transform.position.x < transform.position.x)
					transform.rotation = Quaternion.Euler (0, 180, 0);
				else
					transform.rotation = Quaternion.Euler (0, 0, 0);
				if (attackTimer > attackDelay) {
					PlayerAttack ();
					attackTimer = 0f;
				}
			}
			else if (target.transform.position.x < transform.position.x) {
				GameManager.instance.player.transform.Translate (Vector2.right * playerMoveSpeed * Time.deltaTime * Time.timeScale);
				transform.rotation = Quaternion.Euler (0, 180, 0);
			} else {
				GameManager.instance.player.transform.Translate (Vector2.right * playerMoveSpeed * Time.deltaTime * Time.timeScale);
				transform.rotation = Quaternion.Euler (0, 0, 0);
			}
		}

		playerAttackFloat = GameManager.instance.wave * 2;
		transform.position = new Vector2(Mathf.Clamp(transform.position.x, -3.3f, 7.75f), -0.925f);
	}

	public void PlayerAttack() {
		List<Enemy> attackEnemyList = attackRange.enemyAttackList;
		foreach (Enemy e in attackEnemyList) {
			e.EnemyDamaged (playerAttackFloat);
		}
	}
}
