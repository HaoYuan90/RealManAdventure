using UnityEngine;
using System.Collections;

/**
 * This class enables asteroids to rotate on themselves
 */
public class AsteroidAutoRotation : MonoBehaviour {
	
	[SerializeField]
	protected float m_RotateMagnitude = 0.0005f;

	// Use this for initialization
	void Start ()
	{
		// Set a random angle for it to spin around
		rigidbody.angularVelocity = Random.rotation.eulerAngles * m_RotateMagnitude;
	}
}
