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

	void Start ( )
	{
		if ( getCam == null )
		{
			getCam = Manager.GameCont.MainCam;
		}

		if ( ThisPlayer == null )
		{
			ThisPlayer = Manager.GameCont.WeaponB.transform;
		}
	}
	#endregion
	
	
	#region Public
	void Update ()
	{
		thisTrans.position = getCam.WorldToScreenPoint(ThisPlayer.position );
		thisTrans.localPosition +=  Vector3.up * 9;
	}
	#endregion
	
	
	#region Private

	#endregion
}
