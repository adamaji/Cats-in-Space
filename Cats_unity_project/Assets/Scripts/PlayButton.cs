using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour {
	public Sprite play, stop;
	private Button button;
	public PlayCatHead catHead;

	private enum Fade
	{
		In,
		Out
	};

	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
		catHead = GameObject.Find ("PlayCatHead").GetComponent<PlayCatHead>();
	}

	public void togglePlay() {
		if (button.image.overrideSprite == play) {
			button.image.overrideSprite = stop;
			catHead.SlideOut ();
		} else {
			button.image.overrideSprite = play;
			catHead.SlideIn();
		}
	}
}
