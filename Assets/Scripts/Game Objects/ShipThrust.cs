using UnityEngine;
using System.Collections;

public class ShipThrust : MonoBehaviour 
{
	protected float m_ThrustStrength = 25.0f;
	
	// NOTE: Rigidbody -> Drag is > 0 so that it slows down when you release the thrusters
	// Or, if you like it could be set to 0 to simulate zero-friction like in space;
	// though that makes the game a lot harder.
	
	// NOTE: The Ship prefab is locked to the X-Y plane by freezing its Z coordinate.
	// Refer to the Rigidbody component on the Ship prefab: Constraints -> Freeze Position
	
	// Update is called once per frame
	void Update ()
	{
		// NOTE: Assumes the ship's default placement is upwards	
		// Forward
		if (Input.GetButton("Thrust"))
		{
			// Apply force to positive y direction with respect to spaceship
			rigidbody.AddRelativeForce(0,m_ThrustStrength,0);
		}
	}
}
