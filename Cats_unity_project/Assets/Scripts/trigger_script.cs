using UnityEngine;
using System.Collections;

public class trigger_script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("initialized trigger script\n");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("triggered\n");
	}
}
