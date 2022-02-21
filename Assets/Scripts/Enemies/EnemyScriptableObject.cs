using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Configuration", menuName = "ScriptableObject/Enemy Configuration")]
public class EnemyScriptableObject : ScriptableObject
{
	public EnemyType m_Type;
	public EnemyMovementType m_Movement;
	public float m_FindPlayerRadius;
	public float m_MovementSpeed;
}
