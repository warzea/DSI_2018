using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploScript : MonoBehaviour 
{
	#region Variables
	public float ScaleExplo;
	public float TimeStay;
	float currScale = 0;
	#endregion
	
	#region Mono
	void Start ( )
	{
		transform.localScale = new Vector3(ScaleExplo, ScaleExplo, ScaleExplo);
		Destroy ( gameObject, TimeStay );
	}
	#endregion
	
	#region Public Methods
	
	#endregion

	#region Private Methods

	#endregion

}
