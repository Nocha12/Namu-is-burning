using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public GameObject player;
	public GameObject enemy;

	public List<GameObject> enemyList = new List<GameObject>();

	public int wave = 1;
	public int energy = 999;
	public int makeEnemyCount = 4;
	bool isWaveFinish = false;

	void Start () {
		GameManager.instance = this;
		StartCoroutine (WaitWave ());
	}

	void Update()
	{
		if (enemyList.Count == 0 && isWaveFinish) {
			print ("asfd");
			isWaveFinish = false;
			StartCoroutine (WaitWave ());
		}
	}

	IEnumerator WaitWave() {
		GameObject.Find ("WaveText").GetComponent<Text> ().text = "Wave " + wave.ToString ();
		FadeManager.instance.TextFaidIn (GameObject.Find ("WaveText").gameObject);
		yield return new WaitForSeconds (3);
		FadeManager.instance.TextFaidOut (GameObject.Find ("WaveText").gameObject);
		StartCoroutine (instantiateEnemy());
	}

	IEnumerator instantiateEnemy() {
		for (int i = 0; i < makeEnemyCount; i++) {
			Instantiate (enemy, new Vector3 (10.5f, -0.1f, 0), Quaternion.identity);

			yield return new WaitForSeconds (5 - ((wave * 2) / 10.0f));
		}
		isWaveFinish = true;
		makeEnemyCount += 2;
		wave++;
	}
}
