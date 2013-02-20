using UnityEngine;
using System.Collections;

public class GameLost : MonoBehaviour {
	
	public GameObject m_RestartTextPrefab;
	public GameObject m_RankingTextPrefab;
	
	//We will dim the lighting in game to show the restart button more clearly
	private float m_GameLightIntensity = 0.3f;
	private Vector3 m_RestartTextPosition = new Vector3(0.5f,0.5f,0);
	private Vector3 m_RankingTextPosition = new Vector3(0.5f,0.58f,0);
	
	public void SpaceShipDestroyed()
	{
		// instantiate a pop up asking if user wants to restart
		// Debug.Log("its working");
		
		// see if current score is among high score and render text on screen accordingly
		ProcessPlayerScore();
		// remove gui labels
		GameObject[] labels = GameObject.FindGameObjectsWithTag("GUILabel");
		foreach (GameObject label in labels)
			Destroy(label);
		
		CreateRestartScene();
	}
	
	private void CreateRestartScene()
	{
		// dim lighting in game
		GameObject[] gameLights = GameObject.FindGameObjectsWithTag("GameLight");
		foreach (GameObject light in gameLights)
			light.light.intensity = m_GameLightIntensity;
		
		Instantiate(m_RestartTextPrefab,m_RestartTextPosition,Quaternion.identity);
	}
	
	private void ProcessPlayerScore()
	{
		GameObject timer = GameObject.Find("Timer");
		float time = timer.GetComponent<UITimer>().time;
		//get player's name from playerpref, if no name exists, set to default
		string name = PlayerPrefs.GetString("name");
		if(name == "")
			name = "default";
		int ranking = gameObject.GetComponent<HighScore>().ProcessScore (name,time);
		if(ranking != -1) {
			//current score is high score
			//Debug.Log (ranking);
			GameObject RankingText = (GameObject)Instantiate(m_RankingTextPrefab,m_RankingTextPosition,Quaternion.identity);
			RankingText.guiText.text = string.Format("You rank No.{0} among real men :D",ranking);
		}
	}
}
