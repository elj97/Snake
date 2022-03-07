using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{

	[SerializeField] protected SnakeBodyType m_SnakeBodySelf;

	private void Awake()
	{
		// Set tag here or in snake controller?
	}

	protected bool m_Initialized = true;
}
