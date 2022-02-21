using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] NavMeshAgent m_Agent;
	[SerializeField] int m_EnemyAmount = 5;
	[SerializeField] float m_SpawnDelay = 1f;
	[SerializeField] List<Enemy> EnemyPrefabs = new List<Enemy>();

	private void Awake()
	{
		// Is expensive and in the spawner itself should only be called once and then used for all enemy spawns 
		NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();

		int vertexIndex = Random.Range(0, triangulation.vertices.Length);

		NavMeshHit hit;
		if (NavMesh.SamplePosition(triangulation.vertices[vertexIndex], out hit, 2f, -1))
		{
			m_Agent.Warp(hit.position);
		}
		else
		{
			Debug.LogError("Couldn't place on NavMeshAgent");
		}
	}
}
