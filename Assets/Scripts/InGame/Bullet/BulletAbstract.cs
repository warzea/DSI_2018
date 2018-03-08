using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAbstract : MonoBehaviour 
{
	#region Variables
	public Trajectoir ThisTrajectoir;
	public float MoveSpeed = 10;
	[HideInInspector]
	public Vector3 direction = Vector3.zero;

	[HideInInspector]
	public int BulletDamage = 1;
	[HideInInspector]
	public float BulletRange = 10;
	Transform thisTrans;
	Vector3 startPos;
	bool checkEnd = false;
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
			checkEnd = true;
		}
	}
	#endregion

	#region Private Methods
	protected virtual void OnTriggerEnter(Collider collision)
	{
		Destroy ( gameObject );
	}
	#endregion
}
