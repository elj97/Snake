using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseBody : MonoBehaviour
{
	[SerializeField] SnakeBodyType m_SnakeBodyToPurchase;

	public void Purchase()
	{
		SnakeController.Instance.GrowSnake(m_SnakeBodyToPurchase);
	}
}
