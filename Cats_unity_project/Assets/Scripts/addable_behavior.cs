using UnityEngine;
using System.Collections;

public class addable_behavior : MonoBehaviour {
	public int order_in_layer;
	public KeyCode my_trigger;

	private SpriteRenderer my_renderer;
	private AudioSource my_audio;

	// Use this for initialization
	void Start () {
		my_renderer = gameObject.GetComponent<SpriteRenderer> ();
		my_renderer.enabled = false;
		my_audio = gameObject.GetComponent<AudioSource> ();
		my_audio.volume = 0;
		my_renderer.sortingOrder = order_in_layer;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (my_trigger)) {
			if (my_renderer.enabled) {
				my_audio.volume = 0.0f;
				my_renderer.enabled = false;
			} else {
				my_audio.volume = 1.0f;
				my_renderer.enabled = true;
			}
		}
	
	}
}
