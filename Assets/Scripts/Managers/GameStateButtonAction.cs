using UnityEngine;
using System.Collections;

//this script manages buttons in charge of controlling game's state instead of game object
//it is to be attached to gamestatemanager
public class GameStateButtonAction : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Quit"))
			Application.LoadLevel("Menu");
	}
}
