using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject itemBeingDragged;
	private GameObject baseObject;
	public GameObject worldAnalog;
	private float initialRotationDeg;
	private Vector3 startPosition;
	private Vector3 rotation;
	private string levelname;
	private string trackname;
	private int alignmentFraction;
	
	// Use this for initialization
	void Start () {
		ControllerBehavior controller = GameObject.Find ("LevelController").GetComponent<ControllerBehavior> ();
		this.levelname = "level" + controller.level;
		this.trackname = transform.parent.parent.name;
		// set sprite
		string imageDir = "Sprites/" + levelname + "/" + trackname + "/" + this.name;
		Sprite newSprite =  Resources.Load <Sprite>(imageDir);
		if (newSprite){
			GetComponent<Image>().sprite = newSprite;
		} else {
			Debug.LogError("Sprite not found at " + imageDir, this);
		}
		// set world analog
		string worldAnalogDir = "Prefabs/" + levelname + "/" + trackname + "/" + this.name;
		this.worldAnalog = Resources.Load <GameObject>(worldAnalogDir);
		if (!this.worldAnalog) {
			Debug.Log ("failed to load world analog from " + worldAnalogDir);
		}
		// set initial rotation degree
		this.initialRotationDeg = this.worldAnalog.GetComponent<WorldObject> ().initialRotationDeg;
		//set base object
		this.baseObject = GameObject.Find ("base");
		// set alignment fraction
		this.alignmentFraction = GameObject.Find ("GameBoard").GetComponent<GameBoard> ().alignmentFraction;
		//
		GetComponent<Image> ().preserveAspect = true;
	}

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		//startPosition = transform.parent.position;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
	#endregion

	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
		Vector3 v3 = Camera.main.WorldToScreenPoint(baseObject.transform.position);
		v3 = Input.mousePosition - v3;
		float angle = ((Mathf.Atan2( v3.y, v3.x)* Mathf.Rad2Deg) - 90 + initialRotationDeg) % 360;
		float fracRot = (angle) / 360;
		float snappedRotFrac = ((int) (fracRot * alignmentFraction)) / ((float) alignmentFraction);
		float degreeSnappedRot = snappedRotFrac * 360;
		rotation = new Vector3(0.0f, 0.0f, degreeSnappedRot);
		
		gameObject.transform.eulerAngles = rotation;
	}
	#endregion

	#region IEndDragHandler implementation
	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;
		//transform.position = startPosition;
		transform.position = transform.parent.position;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		
		GameObject track = GameObject.Find ("base/" + trackname);
		GameObject obj = Instantiate (worldAnalog) as GameObject;
		
		obj.name = worldAnalog.name;
		obj.transform.parent = track.transform;
		obj.GetComponent<WorldObject> ().alignmentFraction = alignmentFraction;
		obj.transform.RotateAround (new Vector3 (0, 0, 0), new Vector3 (0, 0, 1), rotation.z);

		gameObject.transform.eulerAngles = Vector3.zero;
		
		obj.transform.localScale = new Vector3 (0.5f, 0.5f, 0);
	}
	#endregion
}
