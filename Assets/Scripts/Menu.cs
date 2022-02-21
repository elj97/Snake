using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Menu and Settings scripts should be combined
/// </summary>

public class Menu : MonoBehaviour
{
	public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	private void Start()
	{
		// Setup Default Settings
		var cameraShake = PlayerPrefs.GetFloat("CameraShake", 1);
	}
}
