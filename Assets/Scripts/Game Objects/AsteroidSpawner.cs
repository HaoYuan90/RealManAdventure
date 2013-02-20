using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {
	
	public GameObject m_AsteroidPrefab;
	public GameObject m_SpaceShip;
	public GameObject m_Asteroids;
	
	[SerializeField]
	private Vector2 m_ForceRange = new Vector2(1000f,5000f);
	
	private Vector2 m_MaxSpawnLocation = new Vector2(10.3f,6.3f);
	private Vector2 m_MinSpawnLocation = new Vector2(-10.3f,-4.3f);
	private Vector2 m_AsteroidStrikeOffset = new Vector2(-2f,2f);
	
	void Start () {
		// Start the coroutine to spawn asteroids
		if(m_SpaceShip != null){
			StartCoroutine(SpawnAsteroids());
		}
	}
	
	public IEnumerator SpawnAsteroids () 
	{
		while(m_SpaceShip != null)
		{
			int spawnFrom = Random.Range (0,4);
			Vector3 pos = new Vector3();
			switch(spawnFrom)
			{
			case 0:
				// Spawn from top
				pos = new Vector3(Random.Range(m_MinSpawnLocation.x,m_MaxSpawnLocation.x),m_MaxSpawnLocation.y,0f);
				break;
			case 1:
				// Spawn from btm
				pos = new Vector3(Random.Range(m_MinSpawnLocation.x,m_MaxSpawnLocation.x),m_MinSpawnLocation.y,0f);
				break;
			case 2:
				// Spawn from left
				pos = new Vector3(m_MinSpawnLocation.x,Random.Range(m_MinSpawnLocation.y,m_MaxSpawnLocation.y),0f);
				break;
			case 3:
				// Spawn from right
				pos = new Vector3(m_MaxSpawnLocation.x,Random.Range(m_MinSpawnLocation.y,m_MaxSpawnLocation.y),0f);
				break;
			default:
				Debug.Log ("Something is wrong at SpawnAsteroids()");
				break;
			}
			//create asteroid and assign a parent to keep them from being cluttered
			GameObject asteroid = (GameObject)Instantiate(m_AsteroidPrefab,pos,Quaternion.identity);
			asteroid.transform.parent = m_Asteroids.transform;
			/*
			 * Apply a force on the asteroid to make it move
			 */
			//Asteroids will move towards the ship with a slight random offset
			Vector3 shipPos = m_SpaceShip.transform.position;
			Vector3 dest = new Vector3(shipPos.x + Random.Range (m_AsteroidStrikeOffset.x, m_AsteroidStrikeOffset.y),
				shipPos.y+Random.Range (m_AsteroidStrikeOffset.x,m_AsteroidStrikeOffset.y));
			
			// Computer directional vector to apply force
			Vector3 forceDirection = new Vector3(dest.x-pos.x, dest.y-pos.y, dest.z-pos.z);
			forceDirection.Normalize();
			float force = Random.Range(m_ForceRange.x,m_ForceRange.y);
			//Debug.Log((forceDirection*force).y);
			asteroid.rigidbody.AddRelativeForce(forceDirection*force);
			
			float waitTime = Random.Range(0f,0.5f);
			yield return new WaitForSeconds(waitTime);
		}
	}
}
