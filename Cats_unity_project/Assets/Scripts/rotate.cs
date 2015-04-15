using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {
	public Rigidbody2D rb2D;
	public float speed = 15f;

	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate() {
		rb2D.MoveRotation(rb2D.rotation + speed * Time.fixedDeltaTime);
	}
}
