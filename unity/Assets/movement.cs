using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
	
//	public float speed = 1.0f;
//
//	// Use this for initialization
//	void Start () {
//	
//	}
//
//	// Update is called once per frame
//	void Update() {
//		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
//		transform.position += move * speed * Time.deltaTime;
//	}

	
	private float moveSpeed = 5;
	private Vector3 moveDirection;
	
	
	void Update() {
		moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"),0,0).normalized;
	}
	
	void FixedUpdate() {
		rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
	}
}
