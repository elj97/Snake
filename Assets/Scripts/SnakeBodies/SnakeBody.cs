using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
	[SerializeField] protected SnakeBodyType m_SnakeBodySelf;

	private void Awake()
	{
		Invoke("Initialize", 5f);
	}

	private void Initialize()
	{
		gameObject.tag = "Body";
		/*
		switch (m_SnakeBodySelf)
		{
			default:
				gameObject.tag = "Body";
				break;
			case SnakeBodyType.Blank:
				gameObject.tag = "Body";
				break;
			case SnakeBodyType.Shooty:
				gameObject.tag = "Body";
				break;
		}
		*/
		m_Initialized = true;
	}

	protected bool m_Initialized;
}
