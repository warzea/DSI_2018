using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponAbstract : MonoBehaviour 
{
	#region Variables
	public int BulletCapacity; 
	public float FireRate;
	public float ForceProjection;
	public float SpeedBullet = 10;
	public GameObject Bullet;
	public Transform SpawnBullet;


	Transform getGargabe;
	int nbrBullet;
	bool canShoot = true;
	#endregion
	
	#region Mono
	void Awake ( )
	{
		nbrBullet = BulletCapacity;
		getGargabe = Manager.GameCont.Garbage;
	}
	#endregion
	
	#region Public Methods
	public void weaponShoot ( Transform playerTrans )
	{
		if ( canShoot && nbrBullet > 0 )
		{
			nbrBullet --;
			canShoot = false;

			GameObject getBullet = ( GameObject ) Instantiate ( Bullet, SpawnBullet.position, playerTrans.localRotation, getGargabe );
			getBullet.GetComponent<Rigidbody>( ).AddForce ( getBullet.transform.forward * SpeedBullet, ForceMode.VelocityChange );

			StartCoroutine ( waitNewShoot ( ) );
		}
		else if ( nbrBullet <= 0 )
		{
			canShoot = false;

			PlayerController getPC = playerTrans.GetComponent<PlayerController>();
			Transform getTrans = transform;
			Vector3 getForward = playerTrans.forward;
			Rigidbody getRigid;

			transform.SetParent(null);
			getPC.UpdateWeapon();

			getRigid = gameObject.AddComponent<Rigidbody>();
			getRigid.useGravity = false;
			getRigid.AddForce(getForward * ForceProjection, ForceMode.VelocityChange);
			
			GetComponent<Collider>().enabled = true;
			getTrans.gameObject.AddComponent(typeof(BulletAbstract));

			Manager.GameCont.WeaponB.NewWeapon(getPC);
		}
	}
	#endregion

	#region Private Methods
	IEnumerator waitNewShoot ( )
	{
		yield return new WaitForSeconds ( FireRate );

		canShoot = true;
	}
	#endregion
}
