using UnityEngine;
using System.Collections;

public class HowScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.guiText.text = "Here you are\n " +
			"finding yourself in the middle of a meteor shower\n" +
			"It is like what they say\n" +
			"\"You did not choose the thug life\n" +
			"The thug life chooses you!\"\n" +
			"You have to get out!\n" +
			"Your spaceship is equiped with back and side thrusters\n" +
			"press up arrow to move forward, left and right arrows to turn\n" +
			"Space key activates you enigmatic super blaster\n" +
			"But it takes long to recharge\n" +
			"Thread carefully\n\n" +
			"Press \"ESC\" to return to menu";
	}
	
	void Update(){
		if(Input.GetButtonDown("Quit"))
			Application.LoadLevel("Menu");
	}
}
