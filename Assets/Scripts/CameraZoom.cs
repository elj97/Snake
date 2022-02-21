using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
	[SerializeField] float m_ZoomSize = 5f;
	[SerializeField] float m_MinZoom;
	[SerializeField] float m_MaxZoom;
	[SerializeField] float m_Sensitivity;

	private void Awake()
	{
		m_Camera = GetComponent<Camera>();
	}

	private void Update()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if (m_ZoomSize > m_MinZoom)
			{
				m_ZoomSize -= m_Sensitivity;
			}
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			if (m_ZoomSize < m_MaxZoom)
			{
				m_ZoomSize += m_Sensitivity;
			}
		}

		m_Camera.orthographicSize = m_ZoomSize;
	}

	private Camera m_Camera;
}
