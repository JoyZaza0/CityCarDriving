using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionPopup : MonoBehaviour
{
	[SerializeField] private GameObject _track;
	[SerializeField] private GameObject _popup;
	[SerializeField] private Button _continue;
	[SerializeField] private Button _restart;
	[SerializeField] private TextMeshProUGUI _missionStatus;
	private CCDS_Marker _marker;
	private static MissionPopup _instance;
	
	public static MissionPopup Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = FindObjectOfType<MissionPopup>();
			}
			
			return _instance;
		}
	}
	
	public bool CanShow{get; private set;}
	
	private void Awake()
	{
		if(_instance != null && _instance != this)
		{
			Destroy(gameObject);
			return;
		}
		
		_instance = this;
	}
	
	private void OnEnable()
	{
		_continue.onClick.AddListener(Continue);
		_restart.onClick.AddListener(Restart);
	}
	
	private void OnDisable()
	{
		_continue.onClick.RemoveAllListeners();
		_restart.onClick.RemoveAllListeners();
	}
	
	private void Continue()
	{
		CCDS_UI_Manager.Instance.Fade();
		RCCP_SceneManager.Instance.activePlayerCamera.ChangeCamera(RCCP_Camera.CameraMode.TPS);
		RCCP_SceneManager.Instance.activePlayerVehicle.SetCanControl(true);
		RCCP.Transport(CCDS_GameplayManager.Instance.spawnPoint.transform.position,CCDS_GameplayManager.Instance.spawnPoint.transform.rotation);
		_popup.SetActive(false);
		_track.SetActive(false);
	}
	
	private void Restart()
	{
		if(_marker)
		{
			CCDS_UI_Manager.Instance.Fade();
			RCCP_SceneManager.Instance.activePlayerCamera.ChangeCamera(RCCP_Camera.CameraMode.TPS);
			RCCP.Transport(_marker.transform.position,Quaternion.identity);
			_popup.SetActive(false);
		}
	}
	
	public void Setup(ACCDS_Mission mission, string info)
	{
		CanShow = mission is CCDS_MissionObjective_Race;
		
		if(CanShow)
		{
			_missionStatus.text = info;
		}		
	}
	
	public void Show()
	{
		_popup.SetActive(true);
	}
	

	public void SetMarker(CCDS_Marker marker)
	{
		_marker = marker;
	}
	
}
