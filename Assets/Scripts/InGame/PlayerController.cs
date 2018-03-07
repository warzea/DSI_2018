using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using DG.Tweening;

public class PlayerController : MonoBehaviour 
{
	#region Variables
	[HideInInspector]
	public int IdPlayer;
	public int LifePlayer = 3;
	public float MoveSpeed;
	public float DashDistance = 5;
	public float DashTime = 1;
	public float DistToDropItem = 1;
	public Transform WeaponPos;
	public Transform BagPos;
	public Transform BoxPlace;

	[Range(0,1)]
	[Tooltip("Speed reduce pendant la phase de shoot")]
	public float SpeedReduce;
	[Range(0,1)]
	[Tooltip("Speed reduce pendant qu'on pousse la caisse")]
	public float SpeedReduceOnBox = 0.1f;
	public float SmoothRotateOnBox = 10;

	[HideInInspector]
	public List<GameObject> AllItem;

	PlayerController thisPC;
	WeaponAbstract thisWeapon;
	Transform thisTrans;
	Transform getBoxWeapon;
	Rigidbody thisRig;

	Player inputPlayer;

	Camera getCam;

	bool shooting = false;
	bool dashing = false;
	bool canDash = true;
	bool canEnterBox = false;
	bool canShoot = true;

	bool DriveBox = false;

	#endregion
	
	#region Mono
	void Awake ( )
	{
		thisPC = GetComponent<PlayerController>();
	}
	void Start ( ) 
	{
		thisTrans = transform;
		thisRig = GetComponent<Rigidbody>();

		inputPlayer = ReInput.players.GetPlayer(IdPlayer);
		
		getBoxWeapon = Manager.GameCont.WeaponB.transform;
		getCam = Manager.GameCont.MainCam;
	}
	
	void Update () 
	{
		float getDeltaTime = Time.deltaTime;
		
		inputAction ( getDeltaTime );

		checkBorder ( );

		if ( AllItem.Count > 0 && Vector3.Distance ( thisTrans.position, getBoxWeapon.position ) < DistToDropItem )
		{
			emptyBag ( );
		}
	}		
	#endregion
	
	#region Public Methods
	public void UpdateWeapon ( WeaponAbstract thisWeap = null )
	{
		if ( thisWeap != null )
		{
			thisWeapon = thisWeap;
		}
		else 
		{
			thisWeapon = null;
		}
	}
	#endregion

	#region Private Methods
	void checkBorder ( )
	{
		Vector3 getCamPos = getCam.WorldToViewportPoint ( thisTrans.position );
		
		if ( getCamPos.x > 1 )
		{
			thisTrans.position = new Vector3 ( getCam.ViewportToWorldPoint ( new Vector3 ( 1, getCamPos.y, getCamPos.z ) ).x, thisTrans.position.y, thisTrans.position.z );
		}
		else if ( getCamPos.x < 0 )
		{
			thisTrans.position = new Vector3 ( getCam.ViewportToWorldPoint ( new Vector3 ( 0, getCamPos.y, getCamPos.z ) ).x, thisTrans.position.y, thisTrans.position.z );
		}

		if ( getCamPos.y > 1 )
		{
			thisTrans.position = new Vector3 ( thisTrans.position.x, thisTrans.position.y, getCam.ViewportToWorldPoint ( new Vector3 ( getCamPos.x, 1, getCamPos.z ) ).z );
		}
		else if ( getCamPos.y < 0 )
		{
			thisTrans.position = new Vector3 ( thisTrans.position.x, thisTrans.position.y, getCam.ViewportToWorldPoint ( new Vector3 ( getCamPos.x, 0, getCamPos.z ) ).z );
		}
	}
	void inputAction ( float getDeltaTime )
	{
		if ( canShoot )
		{
			playerShoot ( getDeltaTime);
		}

		playerAim ( getDeltaTime);

		if ( canDash )
		{
			//playerDash ( );
		}

		if ( !dashing )
		{
			interactPlayer ( );
			playerMove ( getDeltaTime );
		}
	}

	void playerDash ( )
	{
		bool getDash = inputPlayer.GetButtonDown ( "Dash" );

		if ( !getDash )
		{
			return;
		}		

		float Xmove = inputPlayer.GetAxis("MoveX");
		float Ymove = inputPlayer.GetAxis("MoveY");

		Vector3 getDirect = new Vector3 ( Xmove, 0, Ymove );

		if ( getDirect == Vector3.zero )
		{
			return;
		}

		float getDist = DashDistance;
		string getTag;
		
		RaycastHit[] allHit;
		allHit = Physics.RaycastAll ( thisTrans.position, getDirect, DashDistance );

		foreach ( RaycastHit thisRay in allHit )
		{
			getTag = LayerMask.LayerToName (  thisRay.collider.gameObject.layer );
			
			if ( getTag != Constants._BulletPlayer && getTag != Constants._Charct )
			{
				if ( thisRay.distance < getDist )
				{
					getDist = thisRay.distance;
				}
			}
		}

		canDash = false;
		dashing = true;
		
		thisTrans.DOMove ( thisTrans.position + getDirect * getDist, DashTime * ( getDist / DashDistance) ).OnComplete ( () =>
		{
			canDash = true;
			dashing = false;
		});
	}

	void playerMove ( float getDeltaTime )
	{
		float Xmove = inputPlayer.GetAxis("MoveX");
		float Ymove = inputPlayer.GetAxis("MoveY");
		
		if ( shooting )
		{
			getDeltaTime *= SpeedReduce;
		}
		else if ( DriveBox )
		{
			getDeltaTime *= SpeedReduceOnBox;
		}

		thisRig.MovePosition ( thisTrans.position + getDeltaTime * MoveSpeed * new Vector3 ( Xmove, 0, Ymove )  );
	}

	void playerAim ( float getDeltaTime )
	{
		float Xaim = inputPlayer.GetAxis("AimX");
		float Yaim = inputPlayer.GetAxis("AimY");

		if ( Xaim != 0 && Yaim != 0 )
		{
			if ( DriveBox )
			{
				thisRig.MoveRotation ( Quaternion.Slerp ( thisTrans.rotation, Quaternion.LookRotation ( new Vector3 ( Xaim, 0, Yaim ), thisTrans.up ), SmoothRotateOnBox * getDeltaTime ) );
			}
			else
			{
				thisRig.MoveRotation ( Quaternion.LookRotation ( new Vector3 ( Xaim, 0, Yaim ), thisTrans.up ) );
			}
		}
	}

	void playerShoot ( float getDeltaTime )
	{
		float shootInput = inputPlayer.GetAxis("Shoot");

		/*if ( shootInput > 0 )
		{
			bool checkBox = false;
			if ( shooting == false )
			{
				RaycastHit[] allHit;
				string getTag;

				allHit = Physics.RaycastAll ( thisTrans.position, thisTrans.forward, 2 );
				foreach ( RaycastHit thisRay in allHit )
				{
					getTag = thisRay.collider.tag;

					if ( getTag == Constants._BoxTag )
					{
						if ( thisWeapon != null )
						{
							Destroy (thisWeapon.gameObject);
						}
						
						checkBox = true;
						Manager.GameCont.WeaponB.NewWeapon ( thisPC );
						break;
					}
				}
			}
			
			if ( !checkBox && thisWeapon != null )
			{
				thisWeapon.weaponShoot( thisTrans );
			}

			shooting = true;
		}*/

		if ( shootInput > 0 && thisWeapon != null  )
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
			if ( canEnterBox || DriveBox )
			{
				useBoxWeapon ( );
				return;
			}

			RaycastHit[] allHit;
			string getTag;
			
			allHit = Physics.RaycastAll ( thisTrans.position, thisTrans.forward, 1 );
			foreach ( RaycastHit thisRay in allHit )
			{
				getTag = thisRay.collider.tag;

				if ( getTag == Constants._BoxTag )
				{		
					useBoxWeapon ( );
					
					if ( thisWeapon != null )
					{
						Destroy (thisWeapon.gameObject);
					}
					
					Manager.GameCont.WeaponB.NewWeapon ( thisPC );
					
					break;
				}
				else if ( getTag == Constants._ContainerItem )
				{
					thisRay.collider.GetComponent<InteractAbstract>().OnInteract ( thisPC );
					break;
				}
			}
		}
	}

	void useBoxWeapon ( )
	{
		if ( Manager.GameCont.WeaponB.CanControl )
		{
			getCam.GetComponent<CameraFollow>().UpdateTarget(thisTrans);
			WeaponPos.gameObject.SetActive ( false );

			canShoot = false;
			Physics.IgnoreCollision ( GetComponent<Collider>(), getBoxWeapon.GetComponent<Collider>(), true );
			Manager.GameCont.WeaponB.CanControl = false;
			getBoxWeapon.SetParent ( BoxPlace );

			getBoxWeapon.DOLocalMove ( Vector3.zero, 0.5f );
			getBoxWeapon.DOLocalRotateQuaternion ( Quaternion.identity, 0.5f );

			DriveBox = true;
		}
		else if ( DriveBox )
		{
			WeaponPos.gameObject.SetActive ( true );
			getCam.GetComponent<CameraFollow>().UpdateTarget(getBoxWeapon);

			canShoot = true;
			Physics.IgnoreCollision ( GetComponent<Collider>(), getBoxWeapon.GetComponent<Collider>(), false );
			Manager.GameCont.WeaponB.CanControl = true;
			getBoxWeapon.SetParent ( null );

			DriveBox = false;
		}
	}

	void emptyBag ( )
	{
		GameObject[] getBagItems = AllItem.ToArray();
		Transform getBoxTrans = getBoxWeapon;
		Transform currTrans; 

		Manager.GameCont.WeaponB.AddItem ( getBagItems.Length );

		for ( int a = 0; a < getBagItems.Length; a ++ )
		{
			currTrans = getBagItems[a].transform;

			currTrans.gameObject.SetActive ( true );
			currTrans.SetParent ( null );
			currTrans.SetParent ( getBoxTrans );

			currTrans.position = BagPos.position + new Vector3 ( Random.Range(-0.2f, 0.21f), 0, Random.Range(-0.2f, 0.21f) );

			dropItem ( currTrans );
		}

		AllItem.Clear();
	}

	void dropItem ( Transform currTrans )
	{
		DOVirtual.DelayedCall ( Random.Range ( 0, 0.2f ), ( ) =>
		{
			currTrans.DOLocalMove ( Vector3.zero, 1.3f );
			currTrans.DOScale ( Vector3.one, 0.5f ).OnComplete( ( ) => 
			{
				currTrans.DOScale ( Vector3.zero, 0.7f ).OnComplete( ( ) => 
				{
					Destroy ( currTrans.gameObject );
				});
			});
		});
	}

	void OnTriggerEnter ( Collider thisColl )
	{
		if ( thisColl.tag == Constants._EnterCont )
		{
			canEnterBox = true;
		}
	}
	
	void OnTriggerExit ( Collider thisColl )
	{
		if ( thisColl.tag == Constants._EnterCont )
		{
			canEnterBox = false;
		}
	}

	void OnCollisionEnter ( Collision thisColl )
	{
		string getTag = thisColl.collider.tag;
		//if ( getTag == )
	}
	#endregion
}
