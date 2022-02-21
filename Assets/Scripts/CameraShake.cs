using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
	{
		Vector3 originalPos = transform.localPosition;

		float elapsed = 0f;

		while (elapsed < duration)
		{
			float x = Random.Range(-1f, 1f) * magnitude * PlayerPrefs.GetFloat("CameraShake");
			float y = Random.Range(-1f, 1f) * magnitude * PlayerPrefs.GetFloat("CameraShake");

			transform.localPosition = new Vector3(x, y, originalPos.z);

			elapsed += Time.deltaTime;

			yield return null;
		}

		transform.localPosition = originalPos;
	}
}
