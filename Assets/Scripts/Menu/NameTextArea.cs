using UnityEngine;
using System.Collections;

public class NameTextArea : MonoBehaviour {
	
	private string m_playerName;
	private string m_namePrefix;
	private GUIStyle m_playerNameStyle;
	private GUIStyle m_textStyle;
	
	private Rect m_playerNameRect = new Rect(0,50,120,28);
	private Rect m_namePrefixRect = new Rect(0,50,70,28);
	private Rect m_buttonRect = new Rect(0,50,150,28);
	
	void Start()
	{
		m_playerName = PlayerPrefs.GetString("name");
		m_namePrefix = "Player: ";
		
		m_playerNameRect = new Rect(Screen.width/2 - m_playerNameRect.width/2, m_playerNameRect.yMax, m_playerNameRect.width, m_playerNameRect.height);
		m_namePrefixRect = new Rect(m_playerNameRect.xMin - m_namePrefixRect.width -5, m_namePrefixRect.yMax, m_namePrefixRect.width, m_namePrefixRect.height);
		m_buttonRect = new Rect(m_playerNameRect.xMax+5, m_buttonRect.yMax,m_buttonRect.width,m_buttonRect.height);
	}
	
    void OnGUI() 
	{
		if(m_playerNameStyle == null){
			m_playerNameStyle = new GUIStyle(GUI.skin.textField);
			m_playerNameStyle.fontSize = 20;
			m_playerNameStyle.normal.textColor = Color.white;
		}
		if(m_textStyle == null){
			m_textStyle = new GUIStyle(GUI.skin.label);
			m_textStyle.fontSize = 20;
			m_textStyle.normal.textColor = Color.white;
		}
		
		GUI.Label(m_namePrefixRect,m_namePrefix,m_textStyle);
        m_playerName = GUI.TextField(m_playerNameRect, m_playerName, 10 ,m_playerNameStyle);
		if(GUI.Button(m_buttonRect,"Save name and exit")){
			PlayerPrefs.SetString ("name",m_playerName);
			Application.LoadLevel("Menu");
		}
    }
}
