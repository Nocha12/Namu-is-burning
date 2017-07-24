using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public static AudioManager instance;

	public AudioSource ingameSound;
	public AudioSource mainSound;
	public AudioSource touchSound;
	public AudioSource hitSound;

	void Start () {
		AudioManager.instance = this;
	}

	public void startScenePlay() {
		mainSound.Play ();
	}

	public void ingameScenePlay() {
		ingameSound.Play ();
	}
}
