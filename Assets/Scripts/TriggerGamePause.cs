using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGamePause : MonoBehaviour
{
	[SerializeField] GameObject m_ObjectToSetActive;
	[SerializeField] List<GameObject> m_OtherToEnable = null;
	[SerializeField] List<GameObject> m_OtherToDisable = null;

	public void TogglePause()
	{
		GameController.Instance.TriggerPauseAndResume(m_ObjectToSetActive, m_OtherToDisable, m_OtherToEnable);
	}
}
