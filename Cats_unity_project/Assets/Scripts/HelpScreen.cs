using UnityEngine;
using System.Collections;

public class HelpScreen : MonoBehaviour {

	void Start(){
		GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, 0f, 0f);
		transform.localScale = new Vector3 (1f, 1f, 1f);
		transform.position = new Vector2 (0.5f, 0.5f);
		//GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<GUITexture> ().enabled = false;
	}

	public void ToggleScreen() {
		GetComponent<GUITexture> ().enabled = !GetComponent<GUITexture> ().enabled;
	}
}
