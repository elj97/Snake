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

	private void Start()
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
		// Frame Counter
		if (m_FrameCounter < 23767)
		{
			m_FrameCounter++;
		}
		else
		{
			m_FrameCounter = 1;
		}
		if (m_Initialized)
		{
			// Movement
			if (m_FrameCounter % m_MovementUpdateTime == 0)
			{
				Movement();
			}
		}
	}

	private void SetupAgentFromConfiguration()
	{
		//m_Self = m_EnemyScriptableObject.m_Type;
		m_Self = EnemyType.AOE;
		//m_Movement = m_EnemyScriptableObject.m_Movement;
		m_Movement = EnemyMovementType.Erratic;
		//m_Speed = m_EnemyScriptableObject.m_MovementSpeed;

		SetupBehaivour();
	}

	private void SetupBehaivour()
	{
		// I don't know how to do flags with switch statements - could convert it to multiple non-flag enums and go through the switch statement as many times as enum values set, not sure if that would be efficient though
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
		Vector3 moveDirection = positionTarget - transform.position;
		//transform.position += moveDirection * m_Speed * Time.deltaTime;
		m_Agent.destination = positionTarget;

		float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	private Vector3 Target()
	{
		// switch statement
		switch (m_Movement)
		{
			case EnemyMovementType.CirclePlayer:
				return Vector3.zero;
			case EnemyMovementType.Erratic:
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
			case EnemyMovementType.FollowPlayer:
				return Vector3.zero;
			case EnemyMovementType.RamPlayer:
				return Vector3.zero;
			case EnemyMovementType.Spin:
				return Vector3.zero;
		}
		Debug.LogError("Could not find new Target");
		return Vector3.zero;
	}

	// Do these need to be organised?
	private bool m_Initialized;

	private NavMeshAgent m_Agent;

	private float m_Speed;
	private EnemyType m_Self;
	private EnemyMovementType m_Movement;

	// How many frames per update to where the enemy should move, should probably be in seconds instead? idk
	private int m_MovementUpdateTime = 300;

	private short m_FrameCounter;
}
