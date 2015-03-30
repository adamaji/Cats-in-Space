#pragma strict

var menuWidth = 100;
var percentMenuShowing = 0.0;
var percentSubMenuShowing = 0.0;
//showSubMenu is either null or a string of the name of the submenu to show
var showSubMenu = null;
var addables = new Array();

function Start () {

}

function Update () {
	
	if ( Input.mousePosition.x > Screen.width - menuWidth ) {
		percentMenuShowing += 0.1;
		if ( percentMenuShowing > 1 ) percentMenuShowing = 1.0;
		if ( showSubMenu ) {
			percentSubMenuShowing += 0.1;
			if ( percentSubMenuShowing > 1 ) percentSubMenuShowing = 1.0;
		} else {
			percentSubMenuShowing -= 0.1;
			if ( percentSubMenuShowing< 0 ) percentSubMenuShowing = 0.0;
		}	
		
	} else {
		percentMenuShowing -= 0.1;
		if ( percentMenuShowing < 0 ) percentMenuShowing = 0.0;
		
		percentSubMenuShowing -= 0.1;
		if ( percentSubMenuShowing< 0 ) percentSubMenuShowing = 0.0;
	}
	

}

function OnGUI () {	
	
	GUI.Box (Rect (Screen.width - menuWidth*percentMenuShowing , 0, menuWidth, Screen.height), "Categories");
	if (!showSubMenu ){
		var categories = GameObject.FindGameObjectsWithTag( "category" );
		for ( var i=0; i<categories.length; i++ ){
			if (GUI.Button ( Rect (Screen.width - menuWidth*percentMenuShowing , i*Screen.height/categories.length, menuWidth, Screen.height/categories.length), categories[ i ].name )){
				showSubMenu = categories[i].name;
			}
		}	
	}	

	GUI.Box (Rect (Screen.width - menuWidth*percentSubMenuShowing , 0, menuWidth, Screen.height), "Addables");
	if ( showSubMenu ){
		var addables = GameObject.FindGameObjectsWithTag( "addable" );
		if ( GUI.Button(Rect (Screen.width - menuWidth*percentSubMenuShowing , 0, menuWidth, Screen.height/(addables.length+1)), "<-back" )) {
			showSubMenu = null;
		}
		var k = 1; //addable position in list
		for ( var j=0; j<addables.length; j++ ){
			if ( addables[j].transform.parent.name == showSubMenu ) {
				if (GUI.Button ( Rect (Screen.width - menuWidth*percentSubMenuShowing , k*Screen.height/(addables.length+1), menuWidth, Screen.height/(addables.length+1)), addables[ j ].name )){
					var enabled = addables[ j ].GetComponent( SpriteRenderer ).enabled;
					addables[ j ].GetComponent( SpriteRenderer ).enabled = !enabled;
					if ( enabled ) {
						addables[ j ].GetComponent( AudioSource ).volume = 0.0;
					} else {
						addables[ j ].GetComponent( AudioSource ).volume = 1.0;
					}
				}
				k++;
			}
		}	
	}
}