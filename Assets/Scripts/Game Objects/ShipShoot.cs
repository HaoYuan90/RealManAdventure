using UnityEngine;
using System.Collections;

public class ShipShoot : MonoBehaviour
{
	public GameObject m_PrefabBullet;
	public float m_WeaponRechargeTime = 5.0f;
	
	[SerializeField]
	protected float m_Speed = 30.0f;
	private bool m_ReadyToFire = true;
	private float m_WeaponChargeTime = 0.5f;
	
	private GameObject m_FrotalParticleVortex;
	
	void Start ()
	{
		m_FrotalParticleVortex = GameObject.Find("CanonInhaleParticleSystem");
		m_FrotalParticleVortex.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Shoot bullets
		if (Input.GetButtonDown("Shoot") && m_ReadyToFire)
		{
			// Create bullet and set its velocity along its y-axis
			m_FrotalParticleVortex.SetActive(true);
			m_ReadyToFire = false;
			StartCoroutine(Shoot());
		}
	}
	
	private IEnumerator WeaponRecharge()
	{
		yield return new WaitForSeconds(m_WeaponRechargeTime);
		m_ReadyToFire = true;
	}
	
	private IEnumerator Shoot()
	{
		// Create bullet and set its velocity along its y-axis
		yield return new WaitForSeconds(m_WeaponChargeTime);
		gameObject.GetComponent<UIWeaponRechargeBar>().WeaponFired();
		m_FrotalParticleVortex.SetActive(false);
		GameObject bullet = (GameObject)Instantiate(m_PrefabBullet,transform.position,transform.rotation);
			bullet.rigidbody.velocity = transform.TransformDirection(Vector3.up *m_Speed);
		StartCoroutine(WeaponRecharge());
	}
}
