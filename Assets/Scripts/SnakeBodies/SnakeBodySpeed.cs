using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodySpeed : SnakeBody
{
	[SerializeField] float m_SpeedMultiplierAdd = 0.05f;

	private void Awake()
	{
		SnakeController.Instance.SpeedMultiplier += m_SpeedMultiplierAdd;
	}
}
