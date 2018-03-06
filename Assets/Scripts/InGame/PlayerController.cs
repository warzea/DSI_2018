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
	public float FireRate;
	public GameObject Bullet;
	public Transform SpawnBullet;

	Transform thisTrans;
	Transform getGargabe;
	Rigidbody thisRig;

	Player inputPlayer;

	bool canShoot = true;
	#endregion
	
	#region Mono
	void Start ( ) 
	{
		thisTrans = transform;
		thisRig = GetComponent<Rigidbody>();
		getGargabe = Manager.GameCont.Gargabe;

		inputPlayer = ReInput.players.GetPlayer(IdPlayer);
	}
	
	void Update () 
	{
		float getDeltaTime = Time.deltaTime;

		inputAction ( getDeltaTime );
	}
	#endregion
	
	#region Public Methods
	#endregion

	#region Private Methods
	void inputAction ( float getDeltaTime )
	{
		playerAim ( getDeltaTime) ;
		playerMove ( getDeltaTime) ;
		
		playerShoot ( getDeltaTime) ;

		 
	}

	void playerMove ( float getDeltaTime )
	{
		float Xmove = inputPlayer.GetAxis("MoveX");
		float Ymove = inputPlayer.GetAxis("MoveY");
		
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

		if ( shootInput > 0 && canShoot )
		{
			canShoot = false;

			GameObject getBullet = ( GameObject ) Instantiate ( Bullet, SpawnBullet.position, thisTrans.localRotation, getGargabe );
			getBullet.GetComponent<Rigidbody>( ).AddForce ( getBullet.transform.forward * 10, ForceMode.VelocityChange );

			StartCoroutine ( waitNewShoot ( ) );
		}
	}

	IEnumerator waitNewShoot ( )
	{
		yield return new WaitForSeconds ( FireRate );

		canShoot = true;
	}
	#endregion
}
