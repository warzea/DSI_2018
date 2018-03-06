using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	#region Variables
	public float MoveSpeed;

	Transform thisTrans;
	Rigidbody thisRig;

	#endregion
	
	#region Mono
	void Awake () 
	{
		thisTrans = transform;
		thisRig = GetComponent<Rigidbody>();
	}
	
	void Update () 
	{
		
	}
	#endregion
	
	#region Public Methods
	#endregion

	#region Private Methods
	void movePlayer ( float getDeltaTime, float Xmove, float Ymove )
	{
		thisRig.MovePosition ( thisTrans.localPosition + getDeltaTime * MoveSpeed * new Vector3 ( Xmove, 0, Ymove )  );
	}
	#endregion
}
