using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float Speed { get; set; }

	private void Update()
	{
		transform.position += transform.up * Speed * Time.deltaTime;
	}
}
