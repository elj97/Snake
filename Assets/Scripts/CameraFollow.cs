using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rename as FollowTarget
public class CameraFollow : MonoBehaviour
{
	[SerializeField] Transform m_Target;
	[SerializeField] float m_SmoothSpeed = 0.125f; // between 0 and 1

	[SerializeField] Vector3 m_Offset;

	[SerializeField] bool m_LookAtTarget;

	[SerializeField] bool m_ClampByBorder;
	[SerializeField] float m_WorldBorderLeft;
	[SerializeField] float m_WorldBorderRight;
	[SerializeField] float m_WorldBorderTop;
	[SerializeField] float m_WorldBorderBottom;

	private void LateUpdate()
	{
		Vector3 desiredPosition = m_Target.position + m_Offset;
		if (m_ClampByBorder)
		{
			desiredPosition.x = Mathf.Clamp(desiredPosition.x, m_WorldBorderLeft, m_WorldBorderRight);
			desiredPosition.y = Mathf.Clamp(desiredPosition.y, m_WorldBorderBottom, m_WorldBorderTop);
		}
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, m_SmoothSpeed * Time.fixedDeltaTime);
		transform.position = smoothedPosition;

		if (m_LookAtTarget)
		{
			transform.LookAt(m_Target);
		}
	}
}
