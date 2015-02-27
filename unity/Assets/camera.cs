
using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

	public Transform target;
	public float distance;

	void Start() {

	}

	void Update(){

		Vector3 temp = transform.position;

		if (Vector3.Distance (temp, target.position) > 10) {
			temp.x = target.position.x;
			temp.y = target.position.y;
			temp.z = target.position.z - distance;
			transform.position = Vector3.Lerp(transform.position, temp, 1f);
		}
		
	}

}