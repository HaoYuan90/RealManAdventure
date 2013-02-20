using UnityEngine;
using System.Collections;

public class ShipTurn : MonoBehaviour
{
	protected float m_RotationTorque = 2.0f;
	
	// NOTE: The Ship prefab is locked to only rotating about the Z-axis (the X and Y axes are frozen).
	// Refer to the Rigidbody component on the Ship prefab: Constraints -> Freeze Rotation
	
	// Update is called once per frame
	void Update ()
	{
		// Rotate anti-clockwise
		if (Input.GetButton("RotateAC"))
		{
			// Apply torque with respect to z-axis
			rigidbody.AddRelativeTorque(0,0,m_RotationTorque);
		}
		// Rotate clockwise
		else if (Input.GetButton("RotateC"))
		{
			// Apply torque with respect to z-axis
			rigidbody.AddRelativeTorque(0,0,-m_RotationTorque);
		}
	}
}
