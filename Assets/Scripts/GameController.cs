using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public static GameController Instance;

	public bool GameIsPaused { get { return m_GameIsPaused; } }
	public bool PlayerIsAlive { get; set; }

	[SerializeField] float m_FoodSpawnIntervalMin = 1f;
	[SerializeField] float m_FoodSpawnIntervalMax = 5f;

	[SerializeField] GameObject m_PauseMenu;
	[SerializeField] GameObject m_StoreMenu;
	[SerializeField] GameObject m_Countdown;

	[SerializeField] UnityEvent m_OnResume;

	public void PauseGame()
	{
		m_GameIsPaused = true;
		//Time.timeScale = 0;
	}

	public void ResumeGame()
	{
		m_OnResume.Invoke();
		//Time.timeScale = 1;
		m_GameIsPaused = false;
		SnakeController.Instance.SetBodyActive(true);

		if (m_SpawnedFood == false)
		{
			Invoke("SpawnFood", m_TempFoodSpawnInterval);
			m_SpawnedFood = true;
		}
	}

	public void TriggerCountdown()
	{
		m_Countdown.SetActive(true);
	}

	public void RestartScene()
	{
		ResumeGame();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void SetScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void TriggerPauseAndResume(GameObject objToSetActive, List<GameObject> otherToDisable = null, List<GameObject> otherToEnable = null)
	{
		if (PlayerIsAlive)
		{
			if (m_GameIsPaused)
			{
				if (m_Countdown.activeInHierarchy)
				{
					m_Countdown.SetActive(false);
					ResumeGame();
				}
				else
				{
					objToSetActive.SetActive(false);
					if (otherToDisable != null)
					{
						foreach (GameObject obj in otherToDisable)
						{
							obj.SetActive(false);
						}
					}
					TriggerCountdown();
				}
			}
			else
			{
				PauseGame();
				objToSetActive.SetActive(true);
				if (otherToEnable != null)
				{
					foreach (GameObject obj in otherToEnable)
					{
						obj.SetActive(true);
					}
				}
			}
		}
	}

	private void Awake()
	{
		Instance = this;

		m_InGameMenuItems.Add(m_PauseMenu);
		m_InGameMenuItems.Add(m_StoreMenu);
	}

	private void Start()
	{
		float foodSpawnInterval = Random.Range(m_FoodSpawnIntervalMin, m_FoodSpawnIntervalMax);
		Invoke("SpawnFood", foodSpawnInterval);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TriggerPauseAndResume(m_PauseMenu, m_InGameMenuItems);
		}
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			TriggerPauseAndResume(m_StoreMenu, m_InGameMenuItems);
		}
	}

	private void SpawnFood()
	{
		var spawnPoint = new Vector3(Random.Range(-10f, 10f), Random.Range(-5.5f, 5.5f), 0f);
		Instantiate(Food(), spawnPoint, Quaternion.identity);
		float foodSpawnInterval = Random.Range(m_FoodSpawnIntervalMin, m_FoodSpawnIntervalMax);
		if (m_GameIsPaused)
		{
			m_TempFoodSpawnInterval = foodSpawnInterval;
			m_SpawnedFood = false;
		}
		else
		{
			Invoke("SpawnFood", foodSpawnInterval);
			m_SpawnedFood = true;
		}
	}

	private GameObject Food()
	{
		float per = Random.Range(0, 100);
		if (per < 20)
		{
			return GameAssets.Instance.Food_Spicy;
		}
		return GameAssets.Instance.Food_Basic;
	}

	private float m_FoodSpawnInterval;
	private bool m_GameIsPaused;
	private float m_TempFoodSpawnInterval;
	private bool m_SpawnedFood;
	private List<GameObject> m_InGameMenuItems = new List<GameObject>();
}
