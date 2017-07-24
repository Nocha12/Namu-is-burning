using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int hp;
	public float enemyMoveSpeed = .1f;

	public float fireDamageTimer = 0;
	public float fireDamageLimit = 1f;

	public float pollutionTimer = 0;
	public float pollutionLimit = 1f;

	void Start () {
		GameManager.instance.enemyList.Add (this.gameObject);

		hp = GameManager.instance.wave * 10;
	}

	void Update () {
		transform.Translate (Vector3.left * enemyMoveSpeed * Time.deltaTime * Time.timeScale);
		transform.position = new Vector2(Mathf.Clamp(transform.position.x, -3f, 1000), -0.3f);

		if (hp <= 0) {
			Destroy (this.gameObject);
		}
	}

	void OnMouseDown()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().target = gameObject;
	}

	public void EnemyDamaged(float damage) {
		hp--;
		FadeManager.instance.FlashOut (this.gameObject);
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Ground") {
			pollutionTimer += Time.deltaTime;
			if (pollutionTimer >= pollutionLimit) {
				col.GetComponent<Ground> ().pollution -= 2;
				pollutionTimer = 0;
			}

			if (col.GetComponent<Ground> ().type == 1) {  //Stone
				GameManager.instance.player.GetComponent<Player>().playerAttackFloat += GameManager.instance.player.GetComponent<Player>().playerAttackFloat * 0.1f;
			} else if (col.GetComponent<Ground> ().type == 2) {  //Fire
				fireDamageTimer += Time.deltaTime;
				if (fireDamageTimer >= fireDamageLimit) {
					EnemyDamaged (1);
					fireDamageTimer = 0;
				}
			} else if (col.GetComponent<Ground> ().type == 3) {  //Trash
				enemyMoveSpeed = 1f;
			} else {
				fireDamageTimer = 0;
				enemyMoveSpeed = 2.5f;
				GameManager.instance.player.GetComponent<Player> ().playerAttackFloat = GameManager.instance.wave * 2;
			}
		}
	}
}
