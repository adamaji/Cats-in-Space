﻿using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {
	public int level;

	private enum Fade
	{
		In,
		Out
	};

	
	public void resetLevel() {
		GameObject.Find ("screenFader").GetComponent<SceneFadeInOut> ().FadeToBlack();
		StartCoroutine (FadeAudio (1.5f, Fade.Out));
	}

	public void loadLevel(int level){
		Application.LoadLevel (level);
	}

	IEnumerator FadeAudio (float timer, Fade fadeType) {
		float start = fadeType == Fade.In ? 0.0F : 1.0F;
		float end = fadeType == Fade.In ? 1.1F : 0.0F;
		float i = 0.0F;
		float step = 1.0F / timer;
		GameObject audio = GameObject.Find ("Audio Source");
		if (audio) {
			AudioSource a = audio.GetComponent<AudioSource>();
			while (i <= 1.0F) {
				i += step * Time.deltaTime;
				a.volume = Mathf.Lerp (start, end, i);
				yield return new WaitForSeconds (step * Time.deltaTime);
			}
		}
		Application.LoadLevel (level);
	}
}