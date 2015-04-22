using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	
	public static GameObject itemBeingDragged;
	public GameObject worldAnalog;
	public GameObject baseObject;
	public float initialRotationDeg = 0;
	Vector3 startPosition;
	Vector3 rotation;
	
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

		GameObject obj = Instantiate (worldAnalog) as GameObject;
		obj.transform.parent = baseObject.transform;
		//obj.transform.eulerAngles = rotation;
		obj.transform.RotateAround(new Vector3 (0, 0, 0), new Vector3 (0, 0, 1), rotation.z);

		gameObject.transform.eulerAngles = Vector3.zero;
	}
	
	#endregion
}