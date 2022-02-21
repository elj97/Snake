using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance;

	[SerializeField] private AudioSource m_MusicSource;
	[SerializeField] private AudioSource m_SfxSource;

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		ChangeMasterVolume(PlayerPrefs.GetFloat("Volume"));
	}

	public void PlaySound(AudioClip clip)
	{
		m_SfxSource.PlayOneShot(clip);
	}

	public void ChangeMasterVolume(float value)
	{
		AudioListener.volume = value;
	}
}
