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
	Transform thisTrans;
	#endregion
	
	#region Mono
	protected virtual void Start ( ) 
	{
		thisTrans = transform;

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
		switch ( ThisTrajectoir )
		{
			case Trajectoir.Standard:
				thisTrans.localPosition += direction * Time.deltaTime * MoveSpeed;
			break;
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
