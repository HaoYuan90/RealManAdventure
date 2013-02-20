using UnityEngine;
using System.Collections;

public class CinematicAnimation: MonoBehaviour {
	
	public float m_DownForce = 200f;
	public float m_WaitBeforePushing = 4.2f;
	public float m_WaitBeforeNext = 9.0f;
	
	void Start()
	{
		StartCoroutine(LoadNextScene());
		StartCoroutine(PushDownChildren());
	}
	
	private IEnumerator PushDownChildren(){
		//push down the letters
		yield return new WaitForSeconds(m_WaitBeforePushing);
		foreach(Transform child in transform){
			child.rigidbody.AddTorque(Vector3.right*m_DownForce);
		}
	}
	
	private IEnumerator LoadNextScene(){
		//wait for sometime before loading menu
		yield return new WaitForSeconds(m_WaitBeforeNext);
		Application.LoadLevel("Menu");
	}
}

