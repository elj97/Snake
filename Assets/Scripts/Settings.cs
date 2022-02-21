using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Menu and Settings scripts should be combined
/// </summary>


public class Settings : MonoBehaviour
{
	[SerializeField] Slider m_CameraShakeSlider;

	public void SetCameraShake()
	{
		m_LCameraShakeValue = m_CameraShakeSlider.value;

		Debug.Log($"Local Camera Shake Value is: {m_LCameraShakeValue}");
	}

	// Example
	public void SetExampleBool(bool boolean)
	{
		m_ExampleBool = boolean ? 2 : 0;
	}

	public void ApplySettings()
	{
		// Prefs
		PlayerPrefs.SetFloat("CameraShake", m_LCameraShakeValue);

		// Example;
		PlayerPrefs.SetInt("ExampleBool", m_ExampleBool);

		// Settings
		m_CameraShakeSlider.value = m_LCameraShakeValue;

		Debug.Log($"Player Prefs Camera Shake Value is: {PlayerPrefs.GetFloat("CameraShake")}");
	}

	private void Start()
	{
		m_CameraShakeSlider.value = PlayerPrefs.GetFloat("CameraShake");

		// Example
		m_ExampleBoolBoolean = PlayerPrefs.GetInt("ExampleBool") == 0 ? false : true;
	}

	// Local values, discarded on exit
	private float m_LCameraShakeValue;

	// Using these to example how to do a bool since there is no playerprefs.setbool and I'll forget later how to
	private int m_ExampleBool;
	private bool m_ExampleBoolBoolean;
}
