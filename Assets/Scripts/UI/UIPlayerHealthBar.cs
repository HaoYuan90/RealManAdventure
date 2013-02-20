using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ObjectHealth))]
public class UIPlayerHealthBar : MonoBehaviour {
	
	private Rect m_Rectangle = new Rect(0, 0, 80, 7);
	private Rect m_LabelRect = new Rect(0, 0, 80, 20);
	// x field is the offset from center of screen to left of healthbar
	// y field is the offset from btm of screen to top of healthbar
	private Vector2 m_RectOffset = new Vector2(40, 45);
	// x field is the offset from left of healthbar to left of label
	// y field is the offset from top of healthbar to top of label
	private Vector2 m_LabelOffset = new Vector2(55, 8);
	
	Texture2D m_Background;
	Texture2D m_Foreground;
	
	ObjectHealth m_Health;

	// Use this for initialization
	void Start () {
		// Use 1x1 pixels to draw the health bars
		m_Background = new Texture2D(1,1);
		m_Background.SetPixel(0,0, Color.red);
		m_Background.Apply();
		m_Foreground = new Texture2D(1,1);
		m_Foreground.SetPixel(0,0, Color.green);
		m_Foreground.Apply();
		
		m_Health = GetComponent<ObjectHealth>();
	}
	
	void OnGUI () {
		//draw health bar
		m_Rectangle.x = Screen.width/2 - m_RectOffset.x;
		m_Rectangle.y = Screen.height - m_RectOffset.y;
		GUI.DrawTexture(m_Rectangle, m_Background);
		Rect partialRect = m_Rectangle;
		partialRect.width = m_Rectangle.width * (m_Health.FractionHP);
		partialRect.x = m_Rectangle.x;
		GUI.DrawTexture(partialRect, m_Foreground);
		
		m_LabelRect.x = m_Rectangle.x-m_LabelOffset.x;
		m_LabelRect.y = m_Rectangle.y-m_LabelOffset.y;
		GUIStyle labelStyle = new GUIStyle();
		labelStyle.fontStyle = FontStyle.Bold;
		labelStyle.fontSize = 16;
		labelStyle.normal.textColor = Color.white;
		GUI.Label(m_LabelRect,"Player",labelStyle);
	}
}
