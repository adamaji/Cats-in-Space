using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayCatHead : MonoBehaviour {

	public float endX = -0.6f;
	public float startX = -1.0f;
	private Vector2 goalAPos;
	private float speed = 0.2f;


	public void SlideOut() {
		RectTransform rectT = gameObject.GetComponent<RectTransform> ();
		Vector3 newpos = rectT.anchoredPosition;
		newpos.x = Screen.width * endX;

		goalAPos = newpos;
	}

	public void SlideIn() {
		RectTransform rectT = gameObject.GetComponent<RectTransform> ();
		Vector3 newpos = rectT.anchoredPosition;
		newpos.x = Screen.width * startX;

		goalAPos = newpos;
	}

	void Start() {
		RectTransform rectT = gameObject.GetComponent<RectTransform> ();
		goalAPos = rectT.anchoredPosition;
		SlideIn ();
		Invoke ("setVisible", speed + 0.01f);
		//startX = rectT.anchoredPosition.x;
	}

	private void setVisible() {
		this.gameObject.GetComponent<Image> ().enabled = true;
	}

	void Update() {
		RectTransform rectT = gameObject.GetComponent<RectTransform> ();
		rectT.anchoredPosition = Vector2.Lerp (rectT.anchoredPosition, goalAPos, speed);
	}
}
