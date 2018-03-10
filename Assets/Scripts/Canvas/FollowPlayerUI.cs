using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerUI : MonoBehaviour 
{
	#region Variables
	public Transform ThisPlayer;
	public Camera getCam;
	Transform thisTrans;

	#endregion
	
	
	#region Mono
	void Awake ( )
	{
		thisTrans = transform;
	}
	#endregion
	
	
	#region Public
	void Update ()
	{
		thisTrans.position = getCam.WorldToScreenPoint(ThisPlayer.position + Vector3.up * 5);
	}
	#endregion
	
	
	#region Private

	#endregion
}
