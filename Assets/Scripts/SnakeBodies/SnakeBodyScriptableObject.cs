using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Base Snakebody Configuration", menuName = "ScriptableObject/Base Snakebody Configuration")]
public class SnakeBodyScriptableObject : ScriptableObject
{
	public SnakeBodyType Type;
	public ProjectileType ProjectType;
	public int Armor;
	public float ArmorRegenSpeed;
	public float Speed;
	// possibly healing, ghost (make itself and maybe adjacent see through and so snake is able to pass through itself on those parts)
}
