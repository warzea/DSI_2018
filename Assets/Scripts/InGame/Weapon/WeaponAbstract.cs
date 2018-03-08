using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponAbstract : MonoBehaviour 
{
	#region Variables
	public int WeightRandom = 0;
	public bool AutoShoot = true;
	public bool Projectile = false;
	public bool Through = false;
	public bool Explosion = false;

	public int BulletCapacity;
	// -- siAuto
	public float FireRate;
	// -- fin auto

	// -- si manuel
	public float CoolDown;
	// -- fin manuel
	public float BackPush;
	
	[Range(0,1)]
	[Tooltip("Speed reduce pendant la phase de shoot")]
	public float SpeedReduce = 0.5f;

	public int Damage = 1;

	public float Range;
	// -- Si Projectile
	public int NbrBullet = 1;
	public float ScaleBullet = 1;
	public float SpeedBullet = 1;
	public bool Gust = true;
		// -- si rafale
		public float SpaceBullet = 0;
		// -- fin rafale

		// -- si spread
		public float Angle = 0;
		// -- fin spread
	// -- fin Projectile
	
	// -- si Zone
	public float WidthRange;
	public float SpeedZone;
	public float TimeDest;
	// -- fin zone
	
	// -- si Explosion
	public float Diameter;
	// -- end Explosion

	
	//public float ForceProjection;
	//public float SpeedBullet = 10;
	
	public GameObject Bullet;
	public Transform SpawnBullet;

	[HideInInspector]
	public bool canShoot = true;

	Transform getGargabe;
	int nbrBullet;
	
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
			BulletAbstract getScript = getBullet.GetComponent<BulletAbstract>();
			//getBullet.GetComponent<Rigidbody>( ).AddForce ( getBullet.transform.forward * SpeedBullet, ForceMode.VelocityChange );
			getScript.BulletDamage = Damage;
			getScript.BulletRange = Range;
			
			customWeapon ( getBullet.transform, getScript );
			
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
			//getRigid.AddForce(getForward * ForceProjection, ForceMode.VelocityChange);
			
			GetComponent<Collider>().enabled = true;
			getTrans.gameObject.AddComponent(typeof(BulletAbstract));
			getTrans.GetComponent<BulletAbstract>().direction = playerTrans.forward;
			
			Manager.GameCont.WeaponB.NewWeapon(getPC);
		}
	}
	#endregion

	#region Private Methods
	void customWeapon ( Transform getBullet, BulletAbstract scriptBullet )
	{
		if ( Projectile )
		{
			getBullet.transform.localScale *= ScaleBullet;
			scriptBullet.MoveSpeed = SpeedBullet;

			if ( Gust )
			{

			}
			else
			{

			}
		}
	}
	IEnumerator waitNewShoot ( )
	{
		yield return new WaitForSeconds ( FireRate );

		canShoot = true;
	}
	#endregion
}
