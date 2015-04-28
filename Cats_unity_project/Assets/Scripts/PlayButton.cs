using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour {
	public Sprite play, stop;
	private Button button;

	private enum Fade
	{
		In,
		Out
	};

	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
	}

	public void togglePlay() {
		if (button.image.overrideSprite == play) {
			button.image.overrideSprite = stop;
		} else {
			button.image.overrideSprite = play;
		}
	}
}
