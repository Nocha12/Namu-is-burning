using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public GameObject player;
	public GameObject enemy;
	public GameObject endingScene;
	public Text hpText;
	public Text waveT;
	public Text energyText;
	public Text finWaveText;

	public List<GameObject> enemyList = new List<GameObject>();

	public int wave = 1;
	public int energy = 0;
	public int makeEnemyCount = 4;
	bool isWaveFinish = false;
	bool isFinished = false;

	public int homeHp = 100;

	void Start () {
		endingScene = GameObject.Find ("EndingScene");
		GameManager.instance = this;
		StartCoroutine (WaitWave ());
	}

	void Update()
	{
		hpText.text = homeHp.ToString() + "%";
		energyText.text = energy.ToString ();
		waveT.text = wave.ToString ();

		if (enemyList.Count == 0 && isWaveFinish) {
			isWaveFinish = false;
			StartCoroutine (WaitWave ());
		}

		if (isFinished) {
			if (Input.GetMouseButtonDown (0)) {
				SceneManager.LoadScene ("MainScene");
			}
		}

		if (homeHp <= 0) {
			finWaveText.text = wave.ToString ();
			for(int i = 0; i < endingScene.transform.childCount; i++) 
			{ 
				Transform child = endingScene.transform.GetChild(i); 
				child.gameObject.SetActive(true);
			}
			FadeManager.instance.CanvasFaidIn (endingScene);

			FadeManager.instance.TextFaidOut (waveT.gameObject);
			FadeManager.instance.TextFaidOut (hpText.gameObject);
			FadeManager.instance.TextFaidOut (energyText.gameObject);
			StartCoroutine(WaitToFinish ());
		}
	}

	IEnumerator WaitWave() {
		GameObject.Find ("WaveText").GetComponent<Text> ().text = "Wave " + wave.ToString ();
		FadeManager.instance.TextFaidIn (GameObject.Find ("WaveText").gameObject);
		yield return new WaitForSeconds (3);
		FadeManager.instance.TextFaidOut (GameObject.Find ("WaveText").gameObject);
		StartCoroutine (instantiateEnemy());
	}

	IEnumerator WaitToFinish()
	{
		yield return new WaitForSeconds (4);
		isFinished = true;
	}

	IEnumerator instantiateEnemy() {
		for (int i = 0; i < makeEnemyCount; i++) {
			Instantiate (enemy, new Vector3 (10.5f, -0.1f, 0), Quaternion.identity);

			yield return new WaitForSeconds (5 - ((wave * 2) / 10.0f));
			if (homeHp <= 0)
				break;
		}
		if (homeHp > 0) {
			isWaveFinish = true;
			makeEnemyCount += 2;
			wave++;
		}
	}
}
