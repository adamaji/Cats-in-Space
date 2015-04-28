using UnityEngine;
using System.Collections;

public class WorldObject : MonoBehaviour {
	
	public GameObject gameBoard;
	public float initialRotationDeg = -45;
	Vector3 rotation;
	private float padding = 0.01f;
	private Vector3 startPos;
	private Vector3 startAng;
	private Collider currentColliding;
	private AudioSource audio;
	private Vector3 startOffset;
	
	void Start() {
		this.gameBoard = GameObject.Find ("GameBoard");
		ControllerBehavior controller = GameObject.Find ("LevelController").GetComponent<ControllerBehavior> ();
		string levelname = "level"+controller.level;
		string trackname = transform.parent.name;
		
		// create a reference to the piece image
		string imageDir = "Sprites/" + levelname + "/" + trackname + "/" + this.name;
		Sprite newSprite =  Resources.Load <Sprite>(imageDir);
		if (newSprite){
			GetComponent<SpriteRenderer>().sprite = newSprite;
		} else {
			Debug.LogError("Sprite not found at " + imageDir, this);
		}
		
		// create a reference to the audio file
		string audioDir = "Audio/" + levelname + "/" + trackname + "/" + this.name;
		AudioClip audioClip = Resources.Load<AudioClip> (audioDir);
		if (audioClip) {
			GetComponent<AudioSource> ().clip = audioClip;
		} else {
			Debug.LogError ("Audio not found at " + audioDir, this);
		}
		
		// assign member variables
		audio = gameObject.GetComponent<AudioSource> ();


	}
	
	void OnMouseDown() {
		startPos = gameObject.transform.position;
		startAng = gameObject.transform.eulerAngles;
		startOffset = Camera.main.ScreenToWorldPoint (Input.mousePosition) - startPos;
	}
	
	void OnMouseDrag() {
		
		Vector3 v3 = Camera.main.WorldToScreenPoint(gameBoard.transform.position);
		v3 = Input.mousePosition - v3;
		float angle = ((Mathf.Atan2( v3.y, v3.x)* Mathf.Rad2Deg) - 90 + initialRotationDeg) % 360 ;
		rotation = new Vector3(0.0f,0.0f,angle);
		CircleCollider2D coll = GameObject.Find ("base").GetComponent<CircleCollider2D> ();
		Vector3 mouseWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mouseWorld.z = 0;
		if (coll.bounds.Contains ( mouseWorld)) {
			gameObject.transform.position = startPos;
			gameObject.transform.eulerAngles = startAng;
			gameObject.transform.RotateAround (this.gameBoard.transform.position, new Vector3 (0, 0, 1), rotation.z - startAng.z);
		} else {
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition ) - startOffset;
			pos.z = gameObject.transform.position.z;
			transform.position = pos;
			gameObject.transform.eulerAngles = rotation;
		}
	}
	
	void OnMouseUp() {
		BoxCollider pos = GameObject.Find ("recyclebin").GetComponent<BoxCollider> ();
		
		if (pos.bounds.Contains(Input.mousePosition)) {
			Destroy (this.gameObject);
		} else {
			gameObject.transform.position = startPos;
			gameObject.transform.eulerAngles = startAng;
			gameObject.transform.RotateAround (this.gameBoard.transform.position, new Vector3 (0, 0, 1), rotation.z - startAng.z);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.name == "recyclebin") {
			currentColliding = other;
		} else if (gameBoard.GetComponent<GameBoard>().isRotating && other.name == "needle") {
			audio.Play ();
		}
	}
	
	void OnTriggerExit(Collider exit) {
		if (exit.name == "recyclebin") {
			currentColliding = null;
		}
	}
}
