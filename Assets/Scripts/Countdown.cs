using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Countdown : MonoBehaviour
{
	[SerializeField] UnityEvent m_OnStart;
	[SerializeField] UnityEvent m_OnComplete;

	public void TriggerOnStart()
	{
		m_OnStart.Invoke();
	}

	public void TriggerOnComplete()
	{
		m_OnComplete.Invoke();
	}
}
