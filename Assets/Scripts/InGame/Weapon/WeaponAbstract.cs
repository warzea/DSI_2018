using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAbstract : MonoBehaviour 
{
	#region Variables
	public float FireRate;
	public GameObject Bullet;
	public Transform SpawnBullet;

	Transform thisTrans;
	Transform getGargabe;
	bool canShoot = true;
	#endregion
	
	#region Mono
	void Awake ( )
	{
		getGargabe = Manager.GameCont.Garbage;
		thisTrans = transform;
	}
	#endregion
	
	#region Public Methods
	public void UpdateBullet ( )
	{

	}
	public void weaponShoot ( Transform thisTrans )
	{
		if ( canShoot )
		{
			canShoot = false;

			GameObject getBullet = ( GameObject ) Instantiate ( Bullet, SpawnBullet.position, thisTrans.localRotation, getGargabe );
			getBullet.transform.position = thisTrans.position;
			getBullet.GetComponent<Rigidbody>( ).AddForce ( getBullet.transform.forward * 10, ForceMode.VelocityChange );

			StartCoroutine ( waitNewShoot ( ) );
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
