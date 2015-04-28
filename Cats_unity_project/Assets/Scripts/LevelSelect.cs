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
		FadeAudio (4.0f, Fade.Out);
		Debug.Log ("asdf");
		Application.LoadLevel (level);
	}

	IEnumerator FadeAudio (float timer, Fade fadeType) {
		float start = fadeType == Fade.In ? 0.0F : 1.0F;
		float end = fadeType == Fade.In ? 1.1F : 0.0F;
		float i = 0.0F;
		float step = 1.0F / timer;
		while (i <= 1.0F) {
			Debug.Log (i);
			i += step * Time.deltaTime;
			GameObject.Find ("Audio Source").GetComponent<AudioSource>().volume = Mathf.Lerp (start, end, i);
			yield return new WaitForSeconds(step * Time.deltaTime);
		}
	}
}