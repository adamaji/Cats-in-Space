#pragma strict

var menuWidth = 100;
var percentMenuShowing = 0.0;
var addables = new Array();

function Start () {

}

function Update () {
	
	if ( Input.mousePosition.x > Screen.width - menuWidth ) {
		percentMenuShowing += 0.1;
		if ( percentMenuShowing > 1 ) percentMenuShowing = 1.0;
		
	} else {
		percentMenuShowing -= 0.1;
		if ( percentMenuShowing < 0 ) percentMenuShowing = 0.0;
	}
}

function OnGUI () {
	GUI.Box (Rect (Screen.width - menuWidth*percentMenuShowing , 0, menuWidth, Screen.height), "Addables");
	var addables = GameObject.FindGameObjectsWithTag( "addable" );
	for ( var i=0; i<addables.length; i++ ){
		if (GUI.Button ( Rect (Screen.width - menuWidth*percentMenuShowing , i*Screen.height/addables.length, menuWidth, Screen.height/addables.length), addables[ i ].name )){
			var enabled = addables[ i ].GetComponent( SpriteRenderer ).enabled;
			addables[ i ].GetComponent( SpriteRenderer ).enabled = !enabled;
			if ( enabled ) {
				addables[ i ].GetComponent( AudioSource ).volume = 0.0;
			} else {
				addables[ i ].GetComponent( AudioSource ).volume = 1.0;
			}
		}
	}	
}