using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploScript : MonoBehaviour 
{
	#region Variables
    public GameObject GetEffect;
	public float ScaleExplo;

	public float TimeStay;
	public float TimeEffect;
	float currScale = 0;
	#endregion
	
	#region Mono
	void Start ( )
	{
		if ( TimeStay == 0 )
		{
			TimeStay = TimeEffect;
		}

		transform.localScale = new Vector3(ScaleExplo, ScaleExplo, ScaleExplo);

		if ( GetEffect != null )
		{
			Destroy ( (GameObject) Instantiate(GetEffect, transform.position, Quaternion.identity), TimeEffect );
		}
		
		Destroy ( gameObject, TimeStay );
	}
	#endregion
	
	#region Public Methods
	
	#endregion

	#region Private Methods

	#endregion

}
