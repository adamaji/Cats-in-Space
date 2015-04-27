using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuObject : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		ControllerBehavior controller = GameObject.Find ("LevelController").GetComponent<ControllerBehavior> ();
		string levelname = "level"+controller.level;
		string trackname = transform.parent.parent.name;
		string imageDir = "Sprites/" + levelname + "/" + trackname + "/" + this.name;
		Sprite newSprite =  Resources.Load <Sprite>(imageDir);
		if (newSprite){
			GetComponent<Image>().sprite = newSprite;
		} else {
			Debug.LogError("Sprite not found at " + imageDir, this);
		}
	}

}
