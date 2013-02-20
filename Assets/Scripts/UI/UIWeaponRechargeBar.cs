using UnityEngine;
using System.Collections;

public class UIWeaponRechargeBar : MonoBehaviour {
	
	
	private Rect m_Rectangle = new Rect(0, 0, 120, 7);
	private Rect m_LabelRect = new Rect(0, 0, 80, 20);
	private Rect m_GlowRect = new Rect(0,0,126,13);
	// x field is the offset from center of screen to left of healthbar
	// y field is the offset from btm of screen to top of healthbar
	private Vector2 m_RectOffset = new Vector2(60, 22);
	// x field is the offset from left of healthbar to left of label
	// y field is the offset from top of healthbar to top of label
	private Vector2 m_LabelOffset = new Vector2(70, 8);
	private Vector2 m_GlowOffset = new Vector2(63, 25);
	
	Texture2D m_Background;
	Texture2D m_Foreground;
	Texture2D m_Glow;
	
	private float m_WeaponRechargeTime;
	private float m_CurrentTime;
	private float m_GlowAlpha;
	private bool m_AlphaIncreasing;
	
	public void WeaponFired() {
		m_CurrentTime = 0f;
		m_GlowAlpha = 0f;
		m_AlphaIncreasing = true;
		m_Glow.SetPixel(0,0, new Color(0f,1f,1f,m_GlowAlpha));
		m_Glow.Apply();
	}

	// Use this for initialization
	void Start () {
		// Use 1x1 pixels to draw the health bars
		m_Background = new Texture2D(1,1);
		m_Background.SetPixel(0,0, Color.black);
		m_Background.Apply();
		m_Foreground = new Texture2D(1,1);
		m_Foreground.SetPixel(0,0, new Color(0f,1f,1f,1f));
		m_Foreground.Apply();
		
		m_GlowAlpha = 0f;
		m_AlphaIncreasing = true;
		m_Glow = new Texture2D(1,1);
		m_Glow.SetPixel(0,0, new Color(0f,1f,1f,m_GlowAlpha));
		m_Glow.Apply();
		
		m_WeaponRechargeTime = gameObject.GetComponent<ShipShoot>().m_WeaponRechargeTime;
		m_CurrentTime = m_WeaponRechargeTime;
	}
	
	void Update () {
		m_CurrentTime += Time.deltaTime;
		//if weapon is fully charged, the weapon bar will glow
		if(m_CurrentTime > m_WeaponRechargeTime){
			m_CurrentTime = m_WeaponRechargeTime;
			if(m_AlphaIncreasing){
				m_GlowAlpha += Time.deltaTime/2;
				if(m_GlowAlpha > 1f){
					m_GlowAlpha = 1f;
					m_AlphaIncreasing = false;
				}
			}
			else {
				m_GlowAlpha -= Time.deltaTime/2;
				if(m_GlowAlpha < 0f){
					m_GlowAlpha = 0f;
					m_AlphaIncreasing = true;
				}
			}
			m_Glow.SetPixel(0,0, new Color(0f,1f,1f,m_GlowAlpha));
			m_Glow.Apply();
		}
	}
	
	void OnGUI () {
		//draw health bar
		m_Rectangle.x = Screen.width/2 - m_RectOffset.x;
		m_Rectangle.y = Screen.height - m_RectOffset.y;
		GUI.DrawTexture(m_Rectangle, m_Background);
		Rect partialRect = m_Rectangle;
		partialRect.width = m_Rectangle.width * (m_CurrentTime/m_WeaponRechargeTime);
		partialRect.x = m_Rectangle.x;
		GUI.DrawTexture(partialRect, m_Foreground);
		
		//draw label
		m_LabelRect.x = m_Rectangle.x-m_LabelOffset.x;
		m_LabelRect.y = m_Rectangle.y-m_LabelOffset.y;
		GUIStyle labelStyle = new GUIStyle();
		labelStyle.fontStyle = FontStyle.Bold;
		labelStyle.fontSize = 16;
		labelStyle.normal.textColor = Color.white;
		//labelStyle.font.material = 
		GUI.Label(m_LabelRect,"Weapon",labelStyle);
		
		//draw glow
		m_GlowRect.x = Screen.width/2 - m_GlowOffset.x;
		m_GlowRect.y = Screen.height - m_GlowOffset.y;
		GUI.DrawTexture(m_GlowRect, m_Glow);
	}
}
