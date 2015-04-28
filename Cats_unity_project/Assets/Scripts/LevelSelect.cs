using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {
	public int level;

	public enum Fade
	{
		In,
		Out
	};

	
	public void resetLevel() {
		StartCoroutine (FadeAudio (4.0f, Fade.Out));
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
				Debug.Log (a.volume);
				yield return new WaitForSeconds (step * Time.deltaTime);
			}
		}
		Application.LoadLevel (level);
	}
}