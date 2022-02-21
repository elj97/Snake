using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyShoot : SnakeBody
{
	[SerializeField] float m_ShootSpeed;
	[SerializeField] float m_ShootSpeedRandomiser;
	[SerializeField] float m_ProjectileSpeed;
	[SerializeField] float m_ProjectileDisappearTime;

	[SerializeField] int m_ShotsFired;
	[SerializeField] float m_ShotDelay; // Only relevant if shots fired is above 1

	[SerializeField] ProjectileType m_Projectile;
	[SerializeField] ProjectileDirection m_ProjectileDirection;

	[SerializeField] float m_TempDirection;
	[SerializeField] Quaternion m_BulletDirection;

	private void Update()
	{
		if (m_Initialized && m_StartedShooting == false)
		{
			ShootLoop();
			m_StartedShooting = true;
		}
	}

	private void ShootLoop()
	{
		int shots = 0;
		while (shots < m_ShotsFired)
		{
			Invoke("Shoot", m_ShotDelay * shots);
			shots++;
		}
		float nextShot = m_ShootSpeed + Random.Range(-m_ShootSpeedRandomiser, m_ShootSpeedRandomiser);
		Invoke("ShootLoop", nextShot);
	}

	private void Shoot()
	{
		if (GameController.Instance.PlayerIsAlive && GameController.Instance.GameIsPaused == false && isActiveAndEnabled)
		{
			Quaternion rotation = transform.GetChild(0).rotation;
			switch (m_ProjectileDirection)
			{
				case ProjectileDirection.BodyFace:
					//rotation = rotation;
					break;
				case ProjectileDirection.Random:
					rotation.z += Random.Range(0f, 360f);
					break;
				case ProjectileDirection.RandomSide:
					if (Random.Range(1, 100) < 51)
					{
						rotation.z += m_TempDirection;
					}
					else
					{
						rotation.z -= m_TempDirection;
					}
					break;
				case ProjectileDirection.OrderSide:
					if (m_FirstShot)
					{
						rotation *= Quaternion.Euler(0f, 0f, 90f);
						m_FirstShot = false;
					}
					else
					{
						rotation *= Quaternion.Euler(0f, 0f, -90f);
						m_FirstShot = true;
					}
					break;
			}
			m_BulletDirection = rotation;
			GameObject bullet = GameObject.Instantiate(ProjectilePrefab(), transform.position, rotation);
			bullet.GetComponent<Projectile>().Speed = m_ProjectileSpeed;
			// !!! Needs changing: pausing wont stop the destroy. I'm thinking the bullet shouldn't disappear until its far enough from the player
			// that they can't see it, I think that would feel a bit better anyway
			Destroy(bullet, m_ProjectileDisappearTime);
		}
	}

	private GameObject ProjectilePrefab()
	{
		switch (m_Projectile)
		{
			case ProjectileType.Basic:
				return GameAssets.Instance.Bullet_Basic;
				break;
		}

		// Default to basic bullet
		return GameAssets.Instance.Bullet_Basic;
	}

	private bool m_StartedShooting;
	private bool m_FirstShot = true;
}
