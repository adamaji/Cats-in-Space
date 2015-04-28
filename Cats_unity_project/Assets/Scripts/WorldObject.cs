using UnityEngine;
using System.Collections;

public class WorldObject : MonoBehaviour {

	public GameObject baseObject;
	public float initialRotationDeg = -45;
	Vector3 rotation;
	private float padding = 0.01f;
	private Vector3 startPos;
	private Vector3 startAng;
	private Collider currentColliding;

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
		initialRotationDeg = -45;
	}

	void OnMouseDown() {
		startPos = gameObject.transform.position;
		startAng = gameObject.transform.eulerAngles;
	}
	
	void OnMouseDrag() {
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition );
		pos.z = gameObject.transform.position.z;
		transform.position = pos;
		Vector3 v3 = Camera.main.WorldToScreenPoint(baseObject.transform.position);
		v3 = Input.mousePosition - v3;
		float angle = ((Mathf.Atan2( v3.y, v3.x)* Mathf.Rad2Deg) - 90 + initialRotationDeg) % 360 ;
		rotation = new Vector3(0.0f,0.0f,angle);
		
		gameObject.transform.eulerAngles = rotation;
		//gameObject.transform.RotateAround(new Vector3 (0, 0, 0), new Vector3 (0, 0, 1), rotation.z);
	}
	
	void OnMouseUp() {
		BoxCollider pos = GameObject.Find ("recyclebin").GetComponent<BoxCollider> ();
		Debug.Log (pos.bounds);
		Debug.Log (Input.mousePosition);
		if (pos.bounds.Contains(Input.mousePosition)) {
			Destroy (this.gameObject);
		} else {
			gameObject.transform.position = startPos;
			gameObject.transform.eulerAngles = startAng;
			gameObject.transform.RotateAround (this.baseObject.transform.position, new Vector3 (0, 0, 1), rotation.z - startAng.z);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.name == "recyclebin") {
			currentColliding = other;
		}
	}
	
	void OnTriggerExit(Collider exit) {
		if (exit.name == "recyclebin") {
			currentColliding = null;
		}
	}
}
