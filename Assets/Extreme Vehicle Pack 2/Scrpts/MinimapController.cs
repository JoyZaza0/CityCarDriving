using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTAssets.EasyMinimapSystem;

public class MinimapController : MonoBehaviour
{
	[SerializeField] private MinimapRenderer _minimapRenderer;
	
	private void OnEnable()
	{
		CCDS_Events.OnLocalPlayerSpawned += OnPlayerSpawned;
	}
	
	private void OnDisable()
	{
		CCDS_Events.OnLocalPlayerSpawned -= OnPlayerSpawned;
	}
	
	private void OnPlayerSpawned(CCDS_Player player)
	{
		MinimapCamera minimapCamera = player.GetComponent<MinimapCamera>();
		
		if(minimapCamera)
		{
			if(!_minimapRenderer.gameObject.activeSelf)
				_minimapRenderer.gameObject.SetActive(true);
				
			_minimapRenderer.minimapCameraToShow = minimapCamera;
		}
	}
}
