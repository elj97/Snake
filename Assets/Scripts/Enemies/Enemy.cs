using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] EnemyScriptableObject m_EnemyScriptableObject;
	[SerializeField] EnemyType m_EnemyTypeTest;

	private void OnEnable()
	{
		SetupAgentFromConfiguration();
	}

	private void Update()
	{
		// Do Regular Jobs
		// Frame Counter
		if (m_FrameCounter < 23767)
		{
			m_FrameCounter++;
		}
		else
		{
			m_FrameCounter = 1;
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

		switch (m_Movement)
		{
			case EnemyMovementType.CirclePlayer:

				break;
			case EnemyMovementType.Erratic:

				break;
			case EnemyMovementType.FollowPlayer:

				break;
			case EnemyMovementType.RamPlayer:

				break;
			case EnemyMovementType.Spin:

				break;
		}
	}

	private void Movement()
	{
		Vector3 positionTarget = m_Target;
		Vector3 moveDirection = positionTarget - transform.position;
		transform.position += moveDirection * m_Speed * Time.deltaTime;

		float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		Invoke("Movement", m_MovementUpdateTime / Time.deltaTime);
	}

	private float m_Speed;
	private EnemyType m_Self;
	private EnemyMovementType m_Movement;

	// How many frames per update to where the enemy should move
	private int m_MovementUpdateTime = 1;
	private Vector3 m_Target;

	private short m_FrameCounter;
}
