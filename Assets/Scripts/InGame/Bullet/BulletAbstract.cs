using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletAbstract : MonoBehaviour 
{
	#region Variables
	[Tooltip ("Only for explosion")]
	public GameObject GetEffect;
	public Trajectoir ThisTrajectoir;
	public float MoveSpeed = 10;
	[HideInInspector]
	public Vector3 direction = Vector3.zero;

	[HideInInspector]
	public int BulletDamage = 1;
	[HideInInspector]
	public float BulletRange = 10;

	[HideInInspector]
	public float TimeStay = 0;

	[HideInInspector]
	public float WidthRange;
	[HideInInspector]
	public float SpeedZone;
	
	[HideInInspector]
	public bool Through = false;
	[HideInInspector]
	public float Diameter = 0;
	[HideInInspector]
	public bool Projectil = true;
	Transform thisTrans;
	Vector3 startPos;
	BoxCollider getBox;
	//bool checkEnd = false;
	bool canExplose = false;

	#endregion
	
	#region Mono
	protected virtual void Start ( ) 
	{
		thisTrans = transform;
		startPos = thisTrans.position;

		if ( direction == Vector3.zero )
		{
			direction = thisTrans.forward;
		}
		
		Destroy ( gameObject, 5 );

		if (!Projectil)
		{
			getBox = gameObject.AddComponent<BoxCollider>();
			playZone ( );
		}
	}
	
	#endregion
	
	#region Public Methods
	void Update ( )
	{
		if ( !Projectil )
		{	
			getBox.center = startPos * 0.5f;
			getBox.size = startPos;
			return;
		}

		if ( Vector3.Distance ( startPos, thisTrans.position ) < BulletRange )
		{
			switch ( ThisTrajectoir )
			{
				case Trajectoir.Standard:
					thisTrans.localPosition += direction * Time.deltaTime * MoveSpeed;
					break;
			}
		}
		/*else if ( !checkEnd )
		{
			Destroy ( gameObject, TimeStay );
			checkEnd = true;
		}*/
	}
	#endregion

	#region Private Methods
	void playZone ( )
	{
		startPos = Vector3.zero;

		DOTween.To(()=> startPos, x=> startPos = x, new Vector3(WidthRange, 5, BulletRange), SpeedZone).OnComplete ( () =>
		{
			Destroy(gameObject, TimeStay);
		});
	}
	void OnTriggerEnter(Collider collision)
	{
		if ( canExplose )
		{
			Instantiate (GetEffect, thisTrans.position, Quaternion.identity);
			SphereCollider thisSphere =	gameObject.AddComponent<SphereCollider>();
			thisSphere.radius = Diameter;
		}
		
		if ( !Through || collision.tag == Constants._Untag )
		{
			if ( canExplose )
			{
				Destroy ( gameObject, GetEffect.GetComponent<ParticleSystem>().main.duration );
			}
			else
			{
				Destroy ( gameObject );
			}
		}
	}
	#endregion
}
