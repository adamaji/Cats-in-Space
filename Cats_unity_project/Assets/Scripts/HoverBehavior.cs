using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoverBehavior : MonoBehaviour {

	public void ShowUIText(string text) {
		GameObject panel = GameObject.Find ("textBox");
		panel.GetComponent<Text> ().text = text;
		RectTransform r = panel.GetComponent<RectTransform> ();
		Vector2 pos = Input.mousePosition;
		//r.offsetMin = new Vector2 (pos.x, r.offsetMin.y);
		//r.offsetMax = new Vector2 (r.offsetMax.x, pos.y);
		//r.anchoredPosition = pos;
		//r.rect.position = pos;
		r.position = gameObject.transform.position;
	}

	public void UnShowUIText(){
		GameObject panel = GameObject.Find ("textBox");
		panel.GetComponent<Text> ().text = "";
	}
}
