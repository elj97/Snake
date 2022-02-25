using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomTransparencySprite : MonoBehaviour
{
	[Range(0, 255)]
	[SerializeField] float m_MinTransparency;
	[Range(0, 255)]
	[SerializeField] float m_MaxTransparency;

	private void Awake()
	{
		m_Renderer = GetComponent<SpriteRenderer>();

		Color color = m_Renderer.color;
		color.a = Random.Range(m_MinTransparency, m_MaxTransparency) / 255;
		m_Renderer.color = color;
	}

	private SpriteRenderer m_Renderer;
}
