using UnityEngine;
using System.Collections;

public class GameBoard : MonoBehaviour {
	public Rigidbody2D rb2D;
	public float speed = 28f;
	public int alignmentFraction = 360;
	public bool isRotating = false;

	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
	}

	public void toggleRotate() {
		isRotating = !isRotating;
	}

	void FixedUpdate() {
		if (isRotating) {
			rb2D.MoveRotation (rb2D.rotation + speed * Time.fixedDeltaTime);
		}
	}
}
