using UnityEngine;
using System.Collections;

public class sound_trigger : MonoBehaviour {
	public AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onTriggerEnter(Collider otherObject) {
		audio.Play ();
	}
}
