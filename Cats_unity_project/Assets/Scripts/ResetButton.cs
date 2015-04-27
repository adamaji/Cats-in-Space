using UnityEngine;
using System.Collections;

public class ResetButton : MonoBehaviour {
	public string level_name;
	
	public void resetLevel() {
		Application.LoadLevel (level_name);
	}

}
