using UnityEngine;
using System.Collections;

public class WorldObject : MonoBehaviour {
	
	static private GameObject gameBoard;
	public float initialRotationDeg = -45;
	Vector3 rotation;
	private float padding = 0.01f;
	private Vector3 startPos;
	private Vector3 startAng;
	private Collider currentColliding;
	private AudioSource audio;
	private Vector3 startOffset;
	public int alignmentFraction = 360;

	public GameObject hoveredGO;
	public enum HoverState{HOVER, NONE};
	public HoverState hover_state = HoverState.NONE;
	private bool hasStarted = false;
	
	void Start() {
		WorldObject.gameBoard = GameObject.Find ("GameBoard");
		ControllerBehavior controller = GameObject.Find ("LevelController").GetComponent<ControllerBehavior> ();
		string levelname = "level" + controller.level;
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
		hasStarted = true;
	}

//	void Update () {
//		if (hasStarted) {
//			RaycastHit hitInfo = new RaycastHit ();
//			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//			
//			if (Physics.Raycast (ray, out hitInfo)) {
//				//			if(hover_state == HoverState.NONE){
//				//				hitInfo.collider.SendMessage("OnMouseEnter", SendMessageOptions.DontRequireReceiver);
//				//				hoveredGO = hitInfo.collider.gameObject;
//				//			}
//				hover_state = HoverState.HOVER;       
//			} else {
//				//			if(hover_state == HoverState.HOVER){
//				//				hoveredGO.SendMessage("OnMouseExit", SendMessageOptions.DontRequireReceiver);
//				//			}
//				hover_state = HoverState.NONE;
//			}
//			
//			if (hover_state == HoverState.HOVER) {
//				hitInfo.collider.SendMessage ("OnMouseOver", SendMessageOptions.DontRequireReceiver); //Mouse is hovering
//				if (Input.GetMouseButtonDown (0)) {
//					hitInfo.collider.SendMessage ("OnMouseDown", SendMessageOptions.DontRequireReceiver); //Mouse down
//				}
//				if (Input.GetMouseButtonUp (0)) {
//					hitInfo.collider.SendMessage ("OnMouseUp", SendMessageOptions.DontRequireReceiver); //Mouse up
//				}
//				
//			}
//		}
//	}
	
	void OnMouseDown() {
		if (hasStarted) {
			startPos = gameObject.transform.position;
			startAng = gameObject.transform.eulerAngles;
			startOffset = Camera.main.ScreenToWorldPoint (Input.mousePosition) - startPos;
		}
	}
	
	void OnMouseDrag() {
		if (hasStarted) {
			Vector3 v3 = Camera.main.WorldToScreenPoint (gameBoard.transform.position);
			v3 = Input.mousePosition - v3;
			float angle = ((Mathf.Atan2 (v3.y, v3.x) * Mathf.Rad2Deg) - 90 + initialRotationDeg) % 360;
			float fracRot = ((angle - startAng.z) % 360) / 360;
			float snappedRotFrac = ((int)(fracRot * alignmentFraction)) / ((float)alignmentFraction);
			float degreeSnappedRot = snappedRotFrac * 360;
// we should account for the board rotation here
			float autoRotated = (degreeSnappedRot) % 360;
			rotation = new Vector3 (0.0f, 0.0f, autoRotated);

			CircleCollider2D coll = GameObject.Find ("base").GetComponent<CircleCollider2D> ();
			Vector3 mouseWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mouseWorld.z = 0;

			if (coll.bounds.Contains (mouseWorld)) {
				float transformAngle = (rotation.z - startAng.z) % 360;
				gameObject.transform.position = startPos;
				gameObject.transform.eulerAngles = startAng;

				gameObject.transform.RotateAround (gameBoard.transform.position, new Vector3 (0, 0, 1), rotation.z);
			} else {
				Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition) - startOffset;
				pos.z = gameObject.transform.position.z;
				transform.position = pos;
				gameObject.transform.eulerAngles = rotation;
			}
		}
	}
	
	void OnMouseUp() {
		if (hasStarted) {
			BoxCollider pos = GameObject.Find ("recyclebin").GetComponent<BoxCollider> ();
			if (pos.bounds.Contains (Input.mousePosition)) {
				Destroy (this.gameObject);
			} else {
				gameObject.transform.position = startPos;
				gameObject.transform.eulerAngles = startAng;
				gameObject.transform.RotateAround (WorldObject.gameBoard.transform.position, new Vector3 (0, 0, 1), rotation.z);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (gameBoard.GetComponent<GameBoard> ().isRotating) {
			audio.Play ();
		}
	}
}
