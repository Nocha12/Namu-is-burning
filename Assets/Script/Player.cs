using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float playerMoveSpeed = 4.0f;
	public bool playerLeftMoveCheck = false;
	public bool playerRightMoveCheck = false;
	float attackTimer = 0;
	float attackDelay = 1f;
	public float playerAttackFloat;

	public PlayerAttackRange attackRange;

	public GameObject target;
	Animator playerAnimator;

	void Start() {
		playerAnimator = GetComponent<Animator> ();
	}

	void Update () {
		attackTimer += Time.deltaTime;
		if (target) {
			if (Vector2.Distance (target.transform.position, transform.position) < 2.3f) {
				if (target.transform.position.x < transform.position.x)
					transform.rotation = Quaternion.Euler (0, 0, 0);
				else
					transform.rotation = Quaternion.Euler (0, 180, 0);
				if (attackTimer > attackDelay) {
					playerAnimator.SetTrigger ("Attack");
					AudioManager.instance.hitSound.Play ();
					PlayerAttack ();
					attackTimer = 0f;
				}
			} else if (target.transform.position.x < transform.position.x) {
				if (attackTimer > attackDelay) {
					playerAnimator.SetBool ("isWalk", true);
					GameManager.instance.player.transform.Translate (Vector2.left * playerMoveSpeed * Time.deltaTime * Time.timeScale);
					transform.rotation = Quaternion.Euler (0, 0, 0);
				}
			} else {
				if (attackTimer > attackDelay) {
					playerAnimator.SetBool ("isWalk", true);
					GameManager.instance.player.transform.Translate (Vector2.left * playerMoveSpeed * Time.deltaTime * Time.timeScale);
					transform.rotation = Quaternion.Euler (0, 180, 0);
				}
			}
		}
		else
			playerAnimator.SetBool ("isWalk", false);
		playerAttackFloat = (15 + GameManager.instance.wave * 3) / 5;
		transform.position = new Vector2(Mathf.Clamp(transform.position.x, -3.3f, 7.75f), -1.45f);
	}

	void PlayerAttack() {
		List<Enemy> attackEnemyList = attackRange.enemyAttackList;
		foreach (Enemy e in attackEnemyList) {
			e.EnemyDamaged (playerAttackFloat);
		}
	}
}
