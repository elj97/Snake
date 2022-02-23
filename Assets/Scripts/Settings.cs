using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Menu and Settings scripts should be combined
/// </summary>


public class Settings : MonoBehaviour
{
	[SerializeField] Slider m_CameraShakeSlider;
	[SerializeField] Slider m_VolumeSlider;

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

	public void ResetSetting()
	{
		SoundManager.Instance.ChangeMasterVolume(PlayerPrefs.GetFloat("Volume"));
	}

	public void ApplySettings()
	{
		// Prefs
		PlayerPrefs.SetFloat("CameraShake", m_LCameraShakeValue);

		PlayerPrefs.SetFloat("Volume", m_LVolumeValue);

		// Example;
		PlayerPrefs.SetInt("ExampleBool", m_ExampleBool);

		// Settings
		m_CameraShakeSlider.value = m_LCameraShakeValue;

		Debug.Log($"Player Prefs Camera Shake Value is: {PlayerPrefs.GetFloat("CameraShake")}");

		SoundManager.Instance.ChangeMasterVolume(m_LVolumeValue);
	}

	private void Start()
	{
		// setting initial values from player prefs
		m_CameraShakeSlider.value = PlayerPrefs.GetFloat("CameraShake");
		m_VolumeSlider.value = PlayerPrefs.GetFloat("Volume");
		// Example
		m_ExampleBoolBoolean = PlayerPrefs.GetInt("ExampleBool") == 0 ? false : true;

		// add listeners
		m_VolumeSlider.onValueChanged.AddListener(delegate {SetVolume();});
	}

	private void SetVolume()
	{
		m_LVolumeValue = m_VolumeSlider.value;
		SoundManager.Instance.ChangeMasterVolume(m_LVolumeValue);
	}

	// Local values, discarded on exit
	private float m_LCameraShakeValue;
	private float m_LVolumeValue;

	// Using these to example how to do a bool since there is no playerprefs.setbool and I'll forget later how to
	private int m_ExampleBool;
	private bool m_ExampleBoolBoolean;
}
