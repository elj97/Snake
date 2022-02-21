using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
	public static GameAssets Instance;

	// Snake Body
	public GameObject SnakeBody_Blank { get { return m_SnakeBodyBlank; } }
	public GameObject SnakeBody_Shooty { get { return m_SnakeBodyShooty; } }
	public GameObject SnakeBody_Spikey { get { return m_SnakeBodySpikey; } }
	public GameObject SnakeBody_Healthy { get { return m_SnakeBodyHealthy; } }
	public GameObject SnakeBody_Speedy { get { return m_SnakeBodySpeedy; } }

	// Food
	public GameObject Food_Basic { get { return m_FoodBasic; } }
	public GameObject Food_Spicy { get { return m_FoodSpicy; } }

	// Projectiles
	public GameObject Bullet_Basic { get { return m_BulletBasic; } }

	// Effects
	public GameObject Explosion { get { return m_Explosion; } }

	// Sound Effects
	public Sfx_UI SFX_UI { get { return m_sfxUI; } } // example

	[Header("Snake Body")]
	[SerializeField] GameObject m_SnakeBodyBlank;
	[SerializeField] GameObject m_SnakeBodyShooty;
	[SerializeField] GameObject m_SnakeBodySpikey;
	[SerializeField] GameObject m_SnakeBodyHealthy;
	[SerializeField] GameObject m_SnakeBodySpeedy;

	[Header("Food")]
	[SerializeField] GameObject m_FoodBasic;
	[SerializeField] GameObject m_FoodSpicy;

	[Header("Projectiles")]
	[SerializeField] GameObject m_BulletBasic;

	[Header("Effects")]
	[SerializeField] GameObject m_Explosion;

	[Header("Sound Effects")]
	[SerializeField] Sfx_UI m_sfxUI; // example

	private void Awake()
	{
		Instance = this;
	}
}

// example
[Serializable]
public struct Sfx_UI
{
	public GameObject BigExplosionSFX { get { return m_BigExplosionSFX; } }

	[Header("Explosions")]
	[SerializeField] GameObject m_BigExplosionSFX;
}

public enum SnakeBodyType
{
	Blank,			// Default body, no special abilities
	Shooty,			// Shoots
	Spikey,			// Damages on contact
	Healthy,		// Adds armour (re: should be retitled to Armour)
	Speedy,			// Increases speed
}

// Soon to be deprecated if store is created
public enum FoodType
{
	Basic,
	Spicy,
}

public enum ProjectileType
{
	Basic,
}

public enum ProjectileDirection
{
	BodyFace,		// Will shoot the direction the body is facing
	RandomSide,		// Will shoot 180 or -180 degrees of the direction the body is facing, randomly deciding which
	OrderSide,		// Will shoot 180 then -180 degrees of the direction the body is facing
	Random,			// Will shoot in any direction randomly
}

[System.Flags]
public enum EnemyType
{
	Melee = 1 << 0,		// Damage on contact
	Ranged = 1 << 1,	// Shoots something
	AOE = 1 << 2,		// Periodically damage everything in radius around it
}

public enum EnemyMovementType
{
	Spin,			// Spins in place
	Erratic,		// Random Movement
	FollowPlayer,	// Will follow player, stopping when close enough
	CirclePlayer,	// Will start circling player when close enough
	RamPlayer,		// Will speed up when close enough to player
}