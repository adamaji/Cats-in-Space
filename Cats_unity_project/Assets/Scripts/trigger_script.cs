using UnityEngine;
using System.Collections;

public class trigger_script : MonoBehaviour {
	private AudioSource audio;
	// Use this for initialization
	void Start () {
		Debug.Log ("initialized trigger script\n");
		audio = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("triggered\n");
		audio.Play ();
	}

	void OnTriggerStay(Collider other) {
	}

	void OnTriggerLeave(Collider other) {
	}
}
