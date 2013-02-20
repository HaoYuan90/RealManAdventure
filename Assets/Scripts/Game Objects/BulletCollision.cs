using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour
{
	protected int m_Damage = 2;
	//if bullet is out of this range, it will be destroyed
	private Vector2 m_BulletRange = new Vector2(20f,20f);
	
	void Update()
	{
		//destroy bullet if it is out of range 
		if(Mathf.Abs(transform.position.x) > m_BulletRange.x ||
			Mathf.Abs(transform.position.y) > m_BulletRange.y)
			SelfDestruct();
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Enemy")
		{
			// Modify the collided object's health is object is labeled as enemy
			ObjectHealth temp = collider.gameObject.GetComponent<ObjectHealth>();
			if (temp != null)
				temp.ModifyHealth(-m_Damage);
		}
		
		//was used in previous versions, now bullet is destroyed once it is out of range
		/*
		else if (collider.tag == "Edges"){
			// Destroy bullet if it collides with edges of screen
			// Debug.Log ("bullet dealloc");
			SelfDestruct();
		}
		*/
	}
	
	public void SelfDestruct()
	{
		Destroy (gameObject);
	}
}
