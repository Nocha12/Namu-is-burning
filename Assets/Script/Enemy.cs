using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float hp;
	public float enemyMoveSpeed = .06f;

	public float fireDamageTimer = 0;
	public float fireDamageLimit = 1f;

	public float pollutionTimer = 0;
	public float pollutionLimit = 1f;

	float attackTimer = 0;
	float attackDelay = 1f;

	Animator enemyAnimator;

	void Start () {
		GameManager.instance.enemyList.Add (this.gameObject);
		enemyAnimator = GetComponent<Animator> ();

		hp = 10 + GameManager.instance.wave * 3;
	}

	void Update () {
		attackTimer += Time.deltaTime;

		transform.Translate (Vector3.left * enemyMoveSpeed * Time.deltaTime * Time.timeScale);
		transform.position = new Vector2(Mathf.Clamp(transform.position.x, -3.5f, 1000), -0.3f);
		EnemyAttack ();

		if (hp <= 0) {
			GameManager.instance.energy++;
			GameManager.instance.enemyList.Remove (gameObject);
			Destroy (this.gameObject);
		}
	}

	void OnMouseDown()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().target = gameObject;
	}

	public void EnemyDamaged(float damage) {
		hp -= damage + 1;
		FadeManager.instance.FlashOut (this.gameObject);
	}

	void EnemyAttack() {
		if (transform.position.x <= -3.5f && attackTimer > attackDelay) {
			enemyAnimator.SetBool ("isAttack", true);
			GameManager.instance.homeHp--;
			attackTimer = 0;
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Ground") {
			pollutionTimer += Time.deltaTime;
			if (pollutionTimer >= pollutionLimit) {
				col.GetComponent<Ground> ().pollution -= 5;
				pollutionTimer = 0;
			}

			if (col.GetComponent<Ground> ().type == 1) {  //Stone
				GameManager.instance.player.GetComponent<Player>().playerAttackFloat += GameManager.instance.player.GetComponent<Player>().playerAttackFloat * 0.1f;
			} else if (col.GetComponent<Ground> ().type == 2) {  //Fire
				fireDamageTimer += Time.deltaTime;
				if (fireDamageTimer >= fireDamageLimit) {
					EnemyDamaged (col.GetComponent<Ground>().level + 3);
					fireDamageTimer = 0;
				}
			} else if (col.GetComponent<Ground> ().type == 3) {  //Trash
				enemyMoveSpeed = 1f;
			} else {
				enemyMoveSpeed = 2f;
				GameManager.instance.player.GetComponent<Player> ().playerAttackFloat = GameManager.instance.wave * 2;
			}
		}
	}
}
