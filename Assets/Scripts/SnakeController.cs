using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
	public static SnakeController Instance;

	public float SpeedMultiplier { get; set; }

	[SerializeField] float m_MoveSpeed = 5f;
	[SerializeField] float m_SteerSpeed = 180f;
	[SerializeField] float m_BodySpeed = 5f;

	[SerializeField] int m_Gap = 10;

	[SerializeField] float m_PositionHistoryUpdateTime = 5f; // in frames

	[SerializeField] CameraShake m_CameraShake;

	[SerializeField] UnityEvent m_OnDeath;

	[SerializeField] float m_ImmunityTime;

	public void GrowSnake(SnakeBodyType snakeBody)
	{
		GameObject body = Instantiate(ChosenBody(snakeBody));
		m_BodyParts.Add(body);
		if (GameController.Instance.GameIsPaused)
		{
			body.SetActive(false);
		}
	}

	public void SetBodyActive(bool setBool)
	{
		foreach (var body in m_BodyParts)
		{
			body.SetActive(setBool);
		}
	}

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		Invoke("CanStart", m_StartTime);
		GameController.Instance.PlayerIsAlive = true;
	}

	private void Update()
	{
		if (GameController.Instance.GameIsPaused == false)
		{
			// Move Forward
			transform.position += transform.up * (m_MoveSpeed * (1 + SpeedMultiplier)) * Time.deltaTime;

			// Steer
			float steerDirection = Input.GetAxis("Horizontal");
			transform.Rotate(Vector3.forward * -steerDirection * m_SteerSpeed * Time.deltaTime);

			// Store Position History
			if (m_UpdateTime < m_PositionHistoryUpdateTime)
			{
				m_UpdateTime++;
			}
			else
			{
				m_UpdateTime = 0;
				m_PositionHistory.Insert(0, transform.position);
			}

			// Move Body Parts
			int index = 0;
			foreach (var body in m_BodyParts)
			{
				Vector3 point = m_PositionHistory[Mathf.Min(index * m_Gap, m_PositionHistory.Count - 1)];
				Vector3 moveDirection = point - body.transform.position;
				body.transform.position += moveDirection * m_BodySpeed * Time.deltaTime;

				float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
				body.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
				index++;
			}

			// Set Immunity
			if (m_Immune)
			{
				if (m_ImmunityTimeElapsed < m_ImmunityTime)
				{
					m_ImmunityTimeElapsed += Time.deltaTime;
				}
				else
				{
					m_Immune = false;
					m_ImmunityTimeElapsed = 0;
				}
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (m_Started)
		{
			if (collision.CompareTag("Food_Basic"))
			{
				GrowSnake(SnakeBodyType.Blank);
				Destroy(collision.gameObject);
			}

			if (collision.CompareTag("Food_Spicy"))
			{
				GrowSnake(SnakeBodyType.Shooty);
				Destroy(collision.gameObject);
			}

			if (collision.CompareTag("Body"))
			{
				if (m_Immune == false)
				{
					//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
					SnakeHit();
				}
			}
		}
	}

	private void SnakeHit()
	{
		m_Immune = true;
		bool bodyDestroyed = false;
		foreach(var body in m_BodyParts)
		{
			// If there is a body part to destroy then destroy it and stop
			if (body != null && body.activeInHierarchy)
			{
				StartCoroutine(m_CameraShake.Shake(0.1f, 0.2f));
				Instantiate(GameAssets.Instance.Explosion, body.transform.position, Quaternion.identity);
				m_BodyParts.Remove(body);
				DeactivateObject(body);
				bodyDestroyed = true;
				break;
			}
		}
		if (bodyDestroyed == false)
		{
			DestroySnake();
		}
		transform.Rotate(Vector3.back * m_SteerSpeed * -240 * Time.deltaTime);
	}

	private void DestroySnake()
	{
		GameController.Instance.PlayerIsAlive = false;
		StartCoroutine(m_CameraShake.Shake(0.15f, 0.4f));

		Instantiate(GameAssets.Instance.Explosion, transform.position, Quaternion.identity);
		//Invoke("DestroyBodyPart", m_DestroyTime);
		Invoke("TriggerRetryButton", 0.5f);
		DeactivateObject(gameObject.transform.GetChild(0).gameObject);
		m_MoveSpeed = 0f;
	}

	private void DestroyBodyPart()
	{
		Instantiate(GameAssets.Instance.Explosion, m_BodyParts[m_BodyPartIndex].transform.position, Quaternion.identity);
		DeactivateObject(m_BodyParts[m_BodyPartIndex].gameObject);
		m_BodyPartIndex++;
		if (m_DestroyTime < 0.085f)
		{
			m_DestroyTime += 0.005f;
		}

		if (m_BodyPartIndex < m_BodyParts.Count)
		{
			Invoke("DestroyBodyPart", m_DestroyTime);
		}
		else
		{
			Invoke("TriggerRetryButton", 1f);
		}
	}

	private void TriggerRetryButton()
	{
		m_OnDeath.Invoke();
	}

	private void DeactivateObject(GameObject gameObj)
	{
		gameObj.SetActive(false);
	}

	private void CanStart()
	{
		m_Started = true;
	}

	private GameObject ChosenBody(SnakeBodyType snakeBody)
	{
		GameObject body = GameAssets.Instance.SnakeBody_Blank;

		switch (snakeBody)
		{
			case SnakeBodyType.Blank:
				// skip
				break;
			case SnakeBodyType.Shooty:
				body = GameAssets.Instance.SnakeBody_Shooty;
				break;
			case SnakeBodyType.Healthy:
				body = GameAssets.Instance.SnakeBody_Healthy;
				break;
			case SnakeBodyType.Speedy:
				body = GameAssets.Instance.SnakeBody_Speedy;
				break;
			case SnakeBodyType.Spikey:
				body = GameAssets.Instance.SnakeBody_Spikey;
				break;
		}

		return body;
	}

	private List<GameObject> m_BodyParts = new List<GameObject>();
	private List<Vector3> m_PositionHistory = new List<Vector3>();

	private float m_StartTime = 1f;
	private bool m_Started;

	private int m_BodyPartIndex = 0;
	private float m_DestroyTime = 0.05f;

	private float m_UpdateTime;
	private bool m_Immune;
	private float m_ImmunityTimeElapsed;
}
