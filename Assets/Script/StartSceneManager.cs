using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour {

	void Start () {
		AudioManager.instance.startScenePlay ();
	}

	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			SceneManager.LoadScene ("GameScene");
		}
	}
}
