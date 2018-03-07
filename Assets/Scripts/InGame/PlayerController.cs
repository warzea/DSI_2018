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
	public Transform WeaponPos;
	public Transform BagPos;

	[Range(0,1)]
	[Tooltip("Speed reduce pendant la phase de shoot")]
	public float SpeedReduce;

	[HideInInspector]
	public List<GameObject> AllItem;

	PlayerController thisPC;
	WeaponAbstract thisWeapon;
	Transform thisTrans;
	Rigidbody thisRig;

	Player inputPlayer;

	Camera getCam;

	bool shooting = false;
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
		
		getCam = Manager.GameCont.MainCam;
	}
	
	void Update () 
	{
		float getDeltaTime = Time.deltaTime;
		
		inputAction ( getDeltaTime );

		checkBorder ( );
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

	void emptyBag ( )
	{

	}

	
	#endregion
}
