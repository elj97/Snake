using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraShake : MonoBehaviour
{
	[SerializeField] CameraShake m_CameraShake;
	[SerializeField] float m_ShakeDuration = 0.15f;
	[SerializeField] float m_Magnitude = 0.4f;

	public void ShakeCamera()
	{
		StartCoroutine(m_CameraShake.Shake(m_ShakeDuration, m_Magnitude));
	}
}
