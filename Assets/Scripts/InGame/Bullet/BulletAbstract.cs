using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAbstract : MonoBehaviour 
{
	#region Variables
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
	public bool Through = false;
	Transform thisTrans;
	Vector3 startPos;
	bool checkEnd = false;
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
	}
	
	#endregion
	
	#region Public Methods
	void Update ( )
	{
		if ( Vector3.Distance ( startPos, thisTrans.position ) < BulletRange )
		{
			switch ( ThisTrajectoir )
			{
				case Trajectoir.Standard:
					thisTrans.localPosition += direction * Time.deltaTime * MoveSpeed;
				break;
			}
		}
		else if ( !checkEnd )
		{
			Destroy ( gameObject, TimeStay );
			checkEnd = true;
		}
	}
	#endregion

	#region Private Methods
	void OnTriggerEnter(Collider collision)
	{
		if ( canExplose )
		{
			Instantiate (GetEffect, thisTrans.position, Quaternion.identity);
		}
		
		if ( !Through || collision.tag == Constants._Untag )
		{
			Destroy ( gameObject );
		}
	}
	#endregion
}
