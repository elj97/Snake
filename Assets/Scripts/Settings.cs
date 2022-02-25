using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

/// <summary>
/// Menu and Settings scripts should be combined
/// </summary>


public class Settings : MonoBehaviour
{
	[SerializeField] Slider m_CameraShakeSlider;
	[SerializeField] Slider m_VolumeSlider;
	[SerializeField] ControlsSetting m_ControlSetting;
	[SerializeField] CountdownSetting m_CountdownSetting;

	public void SetCameraShake()
	{
		m_LCameraShakeValue = m_CameraShakeSlider.value;
	}
	
	public void SetCountdown()
	{
		m_LCountdown = !m_LCountdown;
		BoolToggleParts(m_LCountdown, m_CountdownSetting.On, m_CountdownSetting.Off);
	}
	
	public void SetControls()
	{
		m_LControls = !m_LControls;
		BoolToggleParts(m_LControls, m_ControlSetting.On, m_ControlSetting.Off);
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

		PlayerPrefs.SetInt("Controls", m_LControls ? 2 : 0);

		PlayerPrefs.SetInt("Countdown", m_LCountdown ? 2 : 0);

		// Settings
		m_CameraShakeSlider.value = m_LCameraShakeValue;

		SoundManager.Instance.ChangeMasterVolume(m_LVolumeValue);
	}

	private void Start()
	{
		// Getting Player Prefs
		float cameraShake = PlayerPrefs.GetFloat("CameraShake");
		float volume = PlayerPrefs.GetFloat("Volume");
		bool controls = PlayerPrefs.GetInt("Controls") == 0 ? false : true;
		bool countdown = PlayerPrefs.GetInt("Countdown") == 0 ? false : true;

		// Setting initial values from Player Prefs
		m_CameraShakeSlider.value = cameraShake;
		m_VolumeSlider.value = volume;
		BoolToggleParts(controls, m_ControlSetting.On, m_ControlSetting.Off);
		BoolToggleParts(countdown, m_CountdownSetting.On, m_CountdownSetting.Off);

		// Setting local values from Player Prefs
		m_LCameraShakeValue = cameraShake;
		m_LVolumeValue = volume;
		m_LControls = controls;
		m_LCountdown = countdown;
		
		// Add listeners
		m_VolumeSlider.onValueChanged.AddListener(delegate {SetVolume();});
		m_ControlSetting.ThisButton.onClick.AddListener(SetControls);
		m_CountdownSetting.ThisButton.onClick.AddListener(SetCountdown);
	}

	private void SetVolume()
	{
		m_LVolumeValue = m_VolumeSlider.value;
		SoundManager.Instance.ChangeMasterVolume(m_LVolumeValue);
	}

	private void BoolToggleParts(bool boolean, GameObject[] onObj, GameObject[] offObj)
	{
		foreach (GameObject obj in onObj)
		{
			obj.SetActive(boolean);
		}
		foreach (GameObject obj in offObj)
		{
			obj.SetActive(!boolean);
		}
	}

	// Local values, discarded on exit
	private float m_LCameraShakeValue;
	private float m_LVolumeValue;
	private bool m_LControls;
	private bool m_LCountdown;
}

[Serializable]
struct ControlsSetting
{
	public Button ThisButton { get { return m_Button; } }
	public GameObject[] On { get { return m_On; } }
	public GameObject[] Off { get { return m_Off; } }

	[SerializeField] Button m_Button;
	[SerializeField] GameObject[] m_On;
	[SerializeField] GameObject[] m_Off;
}

[Serializable]
struct CountdownSetting
{
	public Button ThisButton { get { return m_Button; } }
	public GameObject[] On { get { return m_On; } }
	public GameObject[] Off { get { return m_Off; } }

	[SerializeField] Button m_Button;
	[SerializeField] GameObject[] m_On;
	[SerializeField] GameObject[] m_Off;
}
