using System.Collections;
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
	Transform thisTrans;
	Rigidbody thisRig;

	Player inputPlayer;

	bool shooting = false;
	#endregion
	
	#region Mono
	void Start ( ) 
	{
		thisTrans = transform;
		thisRig = GetComponent<Rigidbody>();

		inputPlayer = ReInput.players.GetPlayer(IdPlayer);
		UpdateWeapon();
	}
	
	void Update () 
	{
		float getDeltaTime = Time.deltaTime;

		inputAction ( getDeltaTime );
	}
	#endregion
	
	#region Public Methods
	public void UpdateWeapon ( )
	{
		thisWeapon = GetComponentInChildren<WeaponAbstract>();
	}
	#endregion

	#region Private Methods
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
