using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
	public NavMeshTriangulation Triangulation { get; set; }
	public NavMeshAgent Agent { get { return m_Agent; } }

	[SerializeField] EnemyScriptableObject m_EnemyScriptableObject;

	private void Awake()
	{
		m_Agent = GetComponent<NavMeshAgent>();
	}
	private void OnEnable()
	{
		SetupAgentFromConfiguration();
		m_Initialized = true;
	}

	private void OnDisable()
	{
		m_Initialized = false;
	}

	private void Update()
	{
		/// Do Regular Jobs
		if (m_Initialized)
		{
			if (GameController.Instance.GameTime > m_MovementUpdateTime)
			{
				Movement();
			}
		}
		if (GameController.Instance.GameIsPaused)
		{
			if (m_Agent.speed != 0)
			{
				m_Agent.speed = 0;
			}
		}
		else
		{
			if (m_Agent.speed != m_Speed)
			{
				m_Agent.speed = m_Speed;
			}
		}
	}

	private void SetupAgentFromConfiguration()
	{
		m_Self = m_EnemyScriptableObject.m_Type;
		m_Movement = m_EnemyScriptableObject.m_Movement;
		m_Speed = m_EnemyScriptableObject.m_MovementSpeed;

		SetupBehaivour();
	}

	private void SetupBehaivour()
	{
		// I don't know how to do switch statements with flags - could convert it to multiple non-flag enums and go through the switch statement as many times as enum values set, not sure if that would be efficient though
		if (m_Self.HasFlag(EnemyType.AOE))
		{
			// AOE Behaivour
		}
		if (m_Self.HasFlag(EnemyType.Melee))
		{
			// Melee Behaivour
		}
		if (m_Self.HasFlag(EnemyType.AOE))
		{
			// Ranged Behaivour
		}
	}

	private void Movement()
	{
		Vector3 positionTarget = Target();
		//Vector3 moveDirection = positionTarget - transform.position;
		m_Agent.destination = positionTarget;

		//float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		//transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		
		m_MovementUpdateTime = GameController.Instance.GameTime + Random.Range(m_MinMovementUpdate, m_MaxMovementUpdate);
	}

	private Vector3 Target()
	{
		switch (m_Movement)
		{
			#region case CirclePlayer
			case EnemyMovementType.CirclePlayer:
				// This one might actually be a bit more complicated than I thought with the navmesh tracking ??
				return Vector3.zero;
			#endregion
			#region case Erratic
			case EnemyMovementType.Erratic:
				// These two don't need to be set every time but unless there is more one-time things that need to be done
				// we won't make a whole new switch statement in the SetupBehaivour for it
				m_MinMovementUpdate = 1f;
				m_MaxMovementUpdate = 3f;
				int vertexIndex = Random.Range(0, Triangulation.vertices.Length);
				NavMeshHit hit;
				if (NavMesh.SamplePosition(Triangulation.vertices[vertexIndex], out hit, 2f, -1))
				{
					return hit.position;
				}
				else
				{
					// If no target was found set target to zero
					Debug.LogWarning("Couldn't find new target on NavMeshAgent");
					return Vector3.zero;
				}
			#endregion
			#region case FollowPlayer
			case EnemyMovementType.FollowPlayer:
				m_MinMovementUpdate = 0.1f;
				m_MaxMovementUpdate = 0.1f;
				return SnakeController.Instance.transform.position;
			#endregion
			#region case RamPlayer
			case EnemyMovementType.RamPlayer:
				return Vector3.zero;
			#endregion
			#region case Spin
			case EnemyMovementType.Spin:
				return Vector3.zero;
			#endregion
		}
		Debug.LogError("Could not find new Target");
		return Vector3.zero;
	}

	// Do these need to be organised?
	private bool m_Initialized;

	private NavMeshAgent m_Agent;
	
	private EnemyType m_Self;
	private EnemyMovementType m_Movement;

	// How many frames per update to where the enemy should move, should probably be in seconds instead? idk
	private float m_MinMovementUpdate;
	private float m_MaxMovementUpdate;
	private float m_MovementUpdateTime = 1f;

	private float m_Speed;
}
