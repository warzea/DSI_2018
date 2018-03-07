using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAbstract : MonoBehaviour 
{
	#region Variables

	#endregion
	
	#region Mono
	protected virtual void Start ( ) 
	{
		Destroy ( gameObject, 5 );
	}
	
	#endregion
	
	#region Public Methods
	#endregion

	#region Private Methods
	protected virtual void OnCollisionEnter(Collision collision)
	{
		Destroy ( gameObject );
	}
	#endregion
}
