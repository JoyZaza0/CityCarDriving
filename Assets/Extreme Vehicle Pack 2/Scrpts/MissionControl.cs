using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class MissionControl : MonoBehaviour
{
	
	private void OnEnable()
	{
		CCDS_Events.OnMissionCompleted += OnMissionCompleted;
	}
	
	private void OnDisable()
	{
		CCDS_Events.OnMissionCompleted -= OnMissionCompleted;
	}
	
	
	private void OnMissionCompleted()
	{
		if(MissionPopup.Instance.CanShow)
		{
			RCCP_SceneManager.Instance.activePlayerVehicle.SetCanControl(false);
			RCCP_SceneManager.Instance.activePlayerCamera.ChangeCamera(RCCP_Camera.CameraMode.FIXED);
		
			StartCoroutine(CompleteWithDelay(2f));
		}
		
	}
	
	private IEnumerator CompleteWithDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		
		MissionPopup.Instance.Show();
	}
		
}
