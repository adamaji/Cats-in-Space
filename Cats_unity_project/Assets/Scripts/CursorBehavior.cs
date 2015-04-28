using UnityEngine;
using System.Collections;

public class CursorBehavior : MonoBehaviour {

	//

	public Sprite openImg;
	public Sprite closeImg;
	private SpriteRenderer myRenderer;

	void Start () {
		myRenderer = gameObject.GetComponent<SpriteRenderer> ();
		myRenderer.sprite = openImg;
		float worldScreenHeight = Camera.main.orthographicSize * 2;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
		transform.localScale = new Vector3 ( (worldScreenWidth / myRenderer.sprite.bounds.size.x)/6,
		                                   worldScreenHeight / myRenderer.sprite.bounds.size.y, 1);
	}
	
	void Update () {
		Vector3 screenpos = Input.mousePosition - new Vector3 (0, Screen.height / 2, 0);
		Vector3 pos = Camera.main.ScreenToWorldPoint (screenpos);
		pos.z = transform.position.z;
		transform.position = pos;
	}

	void OnMouseDown() {
		myRenderer.sprite = closeImg;
	}

	void OnMouseUp() {
		myRenderer.sprite = openImg;
	}
}
