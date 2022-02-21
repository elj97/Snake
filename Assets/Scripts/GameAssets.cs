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

	private void Awake()
	{
		Instance = this;
	}
}

public enum SnakeBodyType
{
	Blank,
	Shooty,
	Spikey,
	Healthy,
	Speedy,
}

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
	BodyFace, // Will shoot the direction the body is facing
	RandomSide, // Will shoot 180 or -180 degrees of the direction the body is facing, randomly deciding which
	OrderSide, // Will shoot 180 then -180 degrees of the direction the body is facing
	Random, // Will shoot in any direction randomly
}