using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
	public static EnemySpawner Instance;

	[SerializeField] int m_EnemyAmount = 5;
	[SerializeField] float m_SpawnDelay = 1f;
	[SerializeField] GameObject m_EnemyPrefabTest;

	public void KillEnemy(Enemy enemy)
	{
		m_Pool.Release(enemy);
	}

	private void Awake()
	{
		Instance = this;
		// This is an expensive call and should not be called often
		m_Triangulation = NavMesh.CalculateTriangulation();
	}

	private void Start()
	{
		m_Pool = new ObjectPool<Enemy>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, 10, 20);

		//InvokeRepeating(nameof(Spawn), 0.2f, 0.2f);

		for (var i = 0; i < m_EnemyAmount; i++)
		{
			Enemy enemy = m_Pool.Get();
		}
	}

	#region Pooling
	private Enemy CreatePooledItem()
	{
		GameObject enemyObj = Instantiate(m_EnemyPrefabTest);
		return enemyObj.GetComponent<Enemy>();
	}
	private void OnReturnedToPool(Enemy enemy)
	{
		enemy.gameObject.SetActive(false);
		// Can also have de-initialisation here
	}
	private void OnTakeFromPool(Enemy enemy)
	{
		enemy.gameObject.SetActive(true);
		/// Initialization
		enemy.Triangulation = m_Triangulation; // This should go in instantiation not take
		// Spawn Position
		int vertexIndex = Random.Range(0, m_Triangulation.vertices.Length);
		NavMeshHit hit;
		if (NavMesh.SamplePosition(m_Triangulation.vertices[vertexIndex], out hit, 2f, -1))
		{
			enemy.Agent.Warp(hit.position);
		}
		/*else
		{
			// If no spawn was found re-add the enemy to the pool and send an error
			Debug.LogError("Couldn't find placement on NavMeshAgent");
			m_Pool.Release(enemy);
		}*/
	}
	private void OnDestroyPoolObject(Enemy enemy)
	{
		Destroy(enemy.gameObject);
	}
	#endregion

	private void Spawn()
	{
		for (var i = 0; i < m_EnemyAmount; i++)
		{
			var enemy = m_Pool.Get();
		}
	}

	private ObjectPool<Enemy> m_Pool;

	private NavMeshTriangulation m_Triangulation;
}
