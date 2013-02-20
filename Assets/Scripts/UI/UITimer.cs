using UnityEngine;
using System.Collections;

public class UITimer : MonoBehaviour {
	
	public float time;
	
	void Start () 
	{
		time = 0f;
	}
	// Update is called once per frame
	void Update () 
	{
		time += Time.deltaTime;
		gameObject.guiText.text = string.Format("{0:0.00}",time);
	}
}
