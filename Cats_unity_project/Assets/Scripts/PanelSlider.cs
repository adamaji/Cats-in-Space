using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelSlider : MonoBehaviour {

	public float endX;
	public float startX;
	private Vector2 goalAPos;

	public void SlideOut() {
		RectTransform rectT = gameObject.GetComponent<RectTransform> ();
		Vector3 newpos = rectT.anchoredPosition;
		newpos.x = -1*endX;

		goalAPos = newpos;
	}

	public void SlideIn() {
		RectTransform rectT = gameObject.GetComponent<RectTransform> ();
		Vector3 newpos = rectT.anchoredPosition;
		newpos.x = -1*startX;

		goalAPos = newpos;
	}

	void Start() {
		RectTransform rectT = gameObject.GetComponent<RectTransform> ();
		goalAPos = rectT.anchoredPosition;
		//startX = rectT.anchoredPosition.x;
	}

	void Update() {
		RectTransform rectT = gameObject.GetComponent<RectTransform> ();
		rectT.anchoredPosition = Vector2.Lerp (rectT.anchoredPosition, goalAPos, 0.2f);
	}
}
