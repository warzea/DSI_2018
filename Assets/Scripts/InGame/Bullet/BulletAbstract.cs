using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAbstract : MonoBehaviour 
{
	#region Variables
	public Trajectoir ThisTrajectoir;
	public float MoveSpeed = 10;
	Transform thisTrans;
	#endregion
	
	#region Mono
	protected virtual void Start ( ) 
	{
		thisTrans = transform;
		Destroy ( gameObject, 5 );
	}
	
	#endregion
	
	#region Public Methods
	void Update ( )
	{
		switch ( ThisTrajectoir )
		{
			case Trajectoir.Standard:
				thisTrans.localPosition += thisTrans.forward * Time.deltaTime * MoveSpeed;
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
