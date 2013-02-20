using UnityEngine;
using System.Collections;

public class ObjectHealth : MonoBehaviour
{
	public GameObject m_ExplosionPrefab;
	
	[SerializeField]
	protected int m_MaxHP = 10;
	protected int m_CurrHP;
	
	// Getter
	public int CurrHP {	get	{ return m_CurrHP; } }
	public int MaxHP {	get	{ return m_MaxHP; } }
	public float FractionHP { get { return (float)(m_CurrHP)/(float)(m_MaxHP); } }
	
	void Start()
	{
		m_CurrHP = m_MaxHP;
	}
	
	public void ModifyHealth(int amount)
	{
		m_CurrHP += amount;
		
		if (m_CurrHP > m_MaxHP)
		{
			m_CurrHP = m_MaxHP;
		}
		else if (m_CurrHP <= 0)
		{
			SelfDestruct();
		}
	}
	
	public void SetHealth(int amount)
	{
		m_CurrHP = amount;
		ModifyHealth (0); // run thru the checks in Modify()
	}
		
	public void SelfDestruct()
	{
		// Method is called when object is destroyed because hp reaches 0
		// Explosion effect is added and all collision attributes of the object is removed.
		Instantiate(m_ExplosionPrefab,transform.position,Quaternion.identity);
		
		if(gameObject.GetComponent<ComponentCollidable>()!=null)
			Destroy (gameObject.GetComponent<ComponentCollidable>());
		if(gameObject.collider!=null)
			Destroy (gameObject.collider);
		if(gameObject.GetComponent<UIObjectHealthBar>()!= null)
			Destroy (gameObject.GetComponent<UIObjectHealthBar>());
		
		// If SpaceShip is destroyed, broadcast the message to all gamestatemanagers(prolly only one in this game)
		if(gameObject.name == "SpaceShip"){
			GameObject[] managers = GameObject.FindGameObjectsWithTag("GameStateManager");
			foreach(GameObject manager in managers)
				manager.SendMessage("SpaceShipDestroyed");
		}
		StartCoroutine(DestroyAfterExplosion());
	}
			
	public IEnumerator DestroyAfterExplosion()
	{
		// At this stage, object is just a rendered mesh without physics attributes
		// Mesh is destroyed after some time of the explosion effect just to look nice
		yield return new WaitForSeconds(0.4f);
		Destroy(this.gameObject);
	}
}
