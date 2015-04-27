using UnityEngine;
using System.Collections;

public class WorldObject : MonoBehaviour {

	public GameObject baseObject;
	public float initialRotationDeg = 0;
	Vector3 rotation;
	private float padding = 0.01f;

	void Start() {
		this.baseObject = GameObject.Find ("base");
		ControllerBehavior controller = GameObject.Find ("LevelController").GetComponent<ControllerBehavior> ();
		string levelname = "level"+controller.level;
		string trackname = transform.parent.name;
		string imageDir = "Sprites/" + levelname + "/" + trackname + "/" + this.name;
		Sprite newSprite =  Resources.Load <Sprite>(imageDir);
		if (newSprite){
			GetComponent<SpriteRenderer>().sprite = newSprite;
		} else {
			Debug.LogError("Sprite not found at " + imageDir, this);
		}
	}

	void OnMouseDrag() {
		Vector3 v3 = Camera.main.WorldToScreenPoint(baseObject.transform.position);
		v3 = Input.mousePosition - v3;
		float angle = ((Mathf.Atan2( v3.y, v3.x) * Mathf.Rad2Deg) - 90 + initialRotationDeg) % 360 ;
		rotation = new Vector3(0.0f, 0.0f, angle * padding);

		//gameObject.transform.eulerAngles = rotation;
		gameObject.transform.RotateAround(new Vector3 (0, 0, 0), new Vector3 (0, 0, 1), rotation.z);
	}
}
