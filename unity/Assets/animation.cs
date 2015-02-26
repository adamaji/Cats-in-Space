using UnityEngine;
using System.Collections;

public class animation : MonoBehaviour {
	
	public float speed = 1.0f;
	private Animator anim;
	private RuntimeAnimatorController runtimeAnimatorController;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		var run_factor = 1;

		if (move.x != 0) {
			anim.SetBool ("walkEnabled", true);
		} else {
			anim.SetBool ("walkEnabled", false);
		}
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			run_factor = 2;
			anim.SetBool ("runEnabled", true);
		} else {
			anim.SetBool ("runEnabled", false);
		}
		transform.position += move * speed * run_factor * Time.deltaTime;
	}
}
