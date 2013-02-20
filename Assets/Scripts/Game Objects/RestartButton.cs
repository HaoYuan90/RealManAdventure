using UnityEngine;
using System.Collections;

//this script enable restart button action

public class RestartButton : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Restart"))
		{
			Application.LoadLevel("GameScene");
		}
	}
}
