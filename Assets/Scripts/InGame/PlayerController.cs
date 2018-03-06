﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour 
{
	#region Variables
	[HideInInspector]
	public int IdPlayer;

	public float MoveSpeed;

	[Range(0,1)]
	[Tooltip("Speed reduce pendant la phase de shoot")]
	public float SpeedReduce;

	WeaponAbstract thisWeapon;
	Transform camTrans;
	Transform thisTrans;
	Rigidbody thisRig;

	Player inputPlayer;

	Camera getCam;

	bool shooting = false;
	#endregion
	
	#region Mono
	void Start ( ) 
	{
		thisTrans = transform;
		thisRig = GetComponent<Rigidbody>();

		inputPlayer = ReInput.players.GetPlayer(IdPlayer);
		UpdateWeapon();
		
		getCam = Manager.GameCont.MainCam;
		camTrans = getCam.transform;
	}
	
	void Update () 
	{
		float getDeltaTime = Time.deltaTime;
		
		inputAction ( getDeltaTime );

		checkBorder ( );
	}		
	#endregion
	
	#region Public Methods
	public void UpdateWeapon ( )
	{
		thisWeapon = GetComponentInChildren<WeaponAbstract>();
	}
	#endregion

	#region Private Methods
	void checkBorder ( )
	{
		Vector3 getCamPos = getCam.WorldToViewportPoint ( thisTrans.localPosition );
		
		if ( getCamPos.x > 1 )
		{
			thisTrans.localPosition = new Vector3 ( getCam.ViewportToWorldPoint ( new Vector3 ( 1, getCamPos.y, getCamPos.z ) ).x, thisTrans.localPosition.y, thisTrans.localPosition.z );
		}
		else if ( getCamPos.x < 0 )
		{
			thisTrans.localPosition = new Vector3 ( getCam.ViewportToWorldPoint ( new Vector3 ( 0, getCamPos.y, getCamPos.z ) ).x, thisTrans.localPosition.y, thisTrans.localPosition.z );
		}

		if ( getCamPos.y > 1 )
		{
			thisTrans.localPosition = new Vector3 ( thisTrans.localPosition.x, thisTrans.localPosition.y, getCam.ViewportToWorldPoint ( new Vector3 ( getCamPos.x, 1, getCamPos.z ) ).z );
		}
		else if ( getCamPos.y < 0 )
		{
			thisTrans.localPosition = new Vector3 ( thisTrans.localPosition.x, thisTrans.localPosition.y, getCam.ViewportToWorldPoint ( new Vector3 ( getCamPos.x, 0, getCamPos.z ) ).z );
		}
	}
	void inputAction ( float getDeltaTime )
	{
		interactPlayer ( );
		playerShoot ( getDeltaTime) ;

		playerAim ( getDeltaTime) ;
		playerMove ( getDeltaTime) ;
		
	}

	void playerMove ( float getDeltaTime )
	{
		float Xmove = inputPlayer.GetAxis("MoveX");
		float Ymove = inputPlayer.GetAxis("MoveY");
		
		if ( shooting )
		{
			getDeltaTime *= SpeedReduce;
		}

		thisRig.MovePosition ( thisTrans.localPosition + getDeltaTime * MoveSpeed * new Vector3 ( Xmove, 0, Ymove )  );
	}

	void playerAim ( float getDeltaTime )
	{
		float Xaim = inputPlayer.GetAxis("AimX");
		float Yaim = inputPlayer.GetAxis("AimY");

		if ( Xaim != 0 && Yaim != 0 )
		{
			thisRig.MoveRotation ( Quaternion.LookRotation ( new Vector3 ( Xaim, 0, Yaim ), thisTrans.up ) );
		}
	}

	void playerShoot ( float getDeltaTime )
	{
		float shootInput = inputPlayer.GetAxis("Shoot");

		if ( shootInput > 0 && thisWeapon != null )
		{
			thisWeapon.weaponShoot( thisTrans );

			shooting = true;
		}
		else 
		{
			shooting = false;
		}
	}

	void interactPlayer ( )
	{
		bool interactInput = inputPlayer.GetButtonDown("Interact");

		if ( interactInput )
		{
			
		}
	}

	
	#endregion
}
