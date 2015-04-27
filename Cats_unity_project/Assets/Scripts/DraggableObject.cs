using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	
	public static GameObject itemBeingDragged;
	public GameObject worldAnalog;
	public GameObject baseObject;
	public float initialRotationDeg = -45;
	Vector3 startPosition;
	Vector3 rotation;
	private string trackname;

	void Start() {
		ControllerBehavior controller = GameObject.Find ("LevelController").GetComponent<ControllerBehavior> ();
		string levelname = "level"+controller.level + "/";
		trackname = transform.parent.parent.name + "/";
		this.baseObject = GameObject.Find ("base");
		this.worldAnalog = Resources.Load <GameObject>("Prefabs/" + levelname + trackname + this.name);
		initialRotationDeg = -45;
	}

	#region IBeginDragHandler implementation
	
	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
	
	#endregion
	
	#region IDragHandler implementation
	
	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
		Vector3 v3 = Camera.main.WorldToScreenPoint(baseObject.transform.position);
		v3 = Input.mousePosition - v3;
		float angle = ((Mathf.Atan2( v3.y, v3.x)* Mathf.Rad2Deg) - 90 + initialRotationDeg) % 360 ;
		rotation = new Vector3(0.0f,0.0f,angle);

		gameObject.transform.eulerAngles = rotation;
		//gameObject.transform.RotateAround (new Vector3 (0.5f, 0.5f, 0), new Vector3 (0, 0, 1), angle);
	}
	
	#endregion
	
	#region IEndDragHandler implementation
	
	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;
		transform.position = startPosition;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;

		GameObject track = GameObject.Find ("base/" + trackname);
		GameObject obj = Instantiate (worldAnalog) as GameObject;
		obj.name = worldAnalog.name;
		obj.transform.parent = track.transform;
		//obj.transform.eulerAngles = rotation;
		obj.transform.RotateAround(new Vector3 (0, 0, 0), new Vector3 (0, 0, 1), rotation.z);

		gameObject.transform.eulerAngles = Vector3.zero;
	}
	
	#endregion
}