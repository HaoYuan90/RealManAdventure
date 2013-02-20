using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	private float m_CurrentAngle;
	private float m_DestAngle;
	private float m_RotationSpeed;
	private float m_RotationSpeedRad;
	private int m_ItemSelected;
	// 0 = new game
	// 1 = how
	// 2 = highscore
	// 3 = exit
	private bool m_IsRotatingCW = false;
	
	void Start ()
	{
		m_ItemSelected = 0;
		m_CurrentAngle = m_DestAngle = transform.eulerAngles.y;
		//Debug.Log(m_DestAngle);
		m_RotationSpeed = 0f;
		m_RotationSpeedRad = 0f;
	}

	void Update ()
	{
		Debug.Log(m_ItemSelected);
		if (!(Mathf.Abs(m_CurrentAngle - m_DestAngle) < 0.000001)){
			m_RotationSpeed = (m_DestAngle - m_CurrentAngle)*Time.deltaTime*4;
			m_RotationSpeedRad = m_RotationSpeed*Mathf.PI/180;
			if((m_IsRotatingCW && m_CurrentAngle + m_RotationSpeed >= m_DestAngle) ||
				!m_IsRotatingCW && m_CurrentAngle + m_RotationSpeed <= m_DestAngle)
			{
				Vector3 temp = transform.eulerAngles;
				temp.y = m_DestAngle;
				transform.eulerAngles = temp;
				m_CurrentAngle = m_DestAngle;
			}
			else{
				transform.RotateAroundLocal(new Vector3(0,1,0),m_RotationSpeedRad);
				m_CurrentAngle += m_RotationSpeed;
			}
		}	
		else
		{
			m_RotationSpeed = 0f;
			m_RotationSpeedRad = 0f;
		}
			
		//shift to left item
		if (Input.GetButtonDown("RotateAC"))
		{
			m_ItemSelected ++;
			if(m_ItemSelected >3)
				m_ItemSelected = 0;
			m_DestAngle -= 90;
			m_IsRotatingCW = false;
		}
		//shift to right item
		else if (Input.GetButtonDown("RotateC"))
		{
			m_ItemSelected --;
			if(m_ItemSelected <0)
				m_ItemSelected = 3;
			m_DestAngle += 90;
			m_IsRotatingCW = true;
		}
		//select item
		else if (Input.GetButton("Shoot")){
			switch(m_ItemSelected){
			case 0:
				Application.LoadLevel("GameScene");
				break;
			case 1:
				Application.LoadLevel("How");
				break;
			case 2:
				Application.LoadLevel("HighScore");
				break;
			case 3:
				Application.Quit();
				break;
			default:
				break;
			}
		}
		
		else if(Input.GetButtonDown("Quit")){
			Application.Quit();
		}
	}
}
