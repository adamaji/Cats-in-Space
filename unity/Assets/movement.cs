using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	float speed = 1.0f;

	// Update is called once per frame
	void Update() {
		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		transform.position += move * speed * Time.deltaTime;
	}
}
