using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRange : MonoBehaviour {

	public List<Enemy> enemyAttackList = new List<Enemy>();

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Enemy") {
			enemyAttackList.Add (col.gameObject.GetComponent<Enemy>());
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Enemy") {
			enemyAttackList.Remove (col.gameObject.GetComponent<Enemy> ());
		}
	}
}
