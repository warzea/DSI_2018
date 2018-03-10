using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMedal : MonoBehaviour 
{
	#region Variables
	public PlayerController ThisPlayer;
	protected Transform thisTrans;
	public float Score;
	#endregion
	
	#region Mono
	void Awake ( )
	{
		thisTrans = transform;
	}
	#endregion
	
	#region Public Methods
	public abstract void StartCheck ( PlayerController[] allPlayer );
	#endregion

	#region Private Methods
	#endregion

}
