using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
	public static GameAssets Instance;

	#region Snake Body
	public GameObject SnakeBody_Blank	{ get { return m_SnakeBodyBlank; } }
	public GameObject SnakeBody_Shooty	{ get { return m_SnakeBodyShooty; } }
	public GameObject SnakeBody_Spikey	{ get { return m_SnakeBodySpikey; } }
	public GameObject SnakeBody_Healthy { get { return m_SnakeBodyHealthy; } }
	public GameObject SnakeBody_Speedy	{ get { return m_SnakeBodySpeedy; } }

	[Header("Snake Body")]
	[SerializeField] GameObject m_SnakeBodyBlank;
	[SerializeField] GameObject m_SnakeBodyShooty;
	[SerializeField] GameObject m_SnakeBodySpikey;
	[SerializeField] GameObject m_SnakeBodyHealthy;
	[SerializeField] GameObject m_SnakeBodySpeedy;

	#endregion

	#region Food
	public GameObject Food_Basic { get { return m_FoodBasic; } }
	public GameObject Food_Spicy { get { return m_FoodSpicy; } }

	[Header("Food")]
	[SerializeField] GameObject m_FoodBasic;
	[SerializeField] GameObject m_FoodSpicy;
	#endregion

	#region Projectiles
	public GameObject Bullet_Basic { get { return m_BulletBasic; } }

	[Header("Projectiles")]
	[SerializeField] GameObject m_BulletBasic;
	#endregion

	#region Effects
	public GameObject Explosion { get { return m_Explosion; } }

	[Header("Effects")]
	[SerializeField] GameObject m_Explosion;
	#endregion

	#region Sound Effects
	public Sfx_UI SFX_UI { get { return m_sfxUI; } } // example

	[Header("Sound Effects")]
	[SerializeField] Sfx_UI m_sfxUI; // example
	#endregion

	#region Tags
	public string TagDamage	{ get { return m_TagDamage; } }
	public string TagBounce	{ get { return m_TagBounce; } }
	public string TagSafe	{ get { return m_TagSafe; } }

	private string m_TagDamage = "Damage";
	private string m_TagBounce = "Bounce";
	private string m_TagSafe = "Safe";
	#endregion

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
	Stay,			// Stays in place
	Erratic,		// Random Movement
	FollowPlayer,	// Will follow player, stopping when close enough
	CirclePlayer,	// Will start circling player when close enough
	RamPlayer,		// Will speed up when close enough to player
}