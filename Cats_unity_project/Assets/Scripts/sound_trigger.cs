using UnityEngine;
using System.Collections;

public class sound_trigger : MonoBehaviour {
	public AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = gameObject.GetComponent<AudioSource> ();
		print ("initialized sound trigger\n");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onTriggerEnter2D(Collider2D otherObject) {
		print ("collided\n");
		audio.Play ();
	}

	void onTriggerStay2D(Collider2D other) {
		print ("stay\n");
	}

	void onTriggerLeave2D(Collider2D other) {
		print ("leave\n");
	}

	void onCollisionEnter2D(Collision2D otherObject) {
		print ("collided\n");
		audio.Play ();
	}

	void onCollisionStay2D(Collision2D other) {
		print ("stay\n");
	}

	void onCollisionLeave2D(Collision2D other) {
		print ("leave\n");
	}
}
