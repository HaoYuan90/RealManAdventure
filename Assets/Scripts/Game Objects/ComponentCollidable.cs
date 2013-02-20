using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider))]
public class ComponentCollidable : MonoBehaviour
{
	[SerializeField]
	protected int m_CollideDamage = 1;
	
	void OnCollisionEnter(Collision collision)
	{
		//if enemy collide with edge of screen, it is destroyed.
		if(collision.gameObject.tag == "Edges" && gameObject.tag == "Enemy")
		{
			Destroy (gameObject);
		}
		else{
			//find the object bumped into and decrease its health
			ContactPoint contact = collision.contacts[0];
			ObjectHealth tmp = contact.otherCollider.gameObject.GetComponent<ObjectHealth>();
			if (tmp != null)
			{
				tmp.ModifyHealth(-m_CollideDamage);
			}
		}
	}
}
