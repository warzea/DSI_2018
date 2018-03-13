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

	public void GoTarget ( PlayerController equaContr = null, string Text = "" )
	{
		ThisPlayer.checkAward = true;

		if ( equaContr != null )
		{
			equaContr.checkAward = true;
			GameObject thisObj = (GameObject) Instantiate (gameObject, thisTrans.parent );
			thisObj.GetComponent<AbstractMedal>().ThisPlayer = equaContr;
			thisObj.GetComponent<AbstractMedal>().GoTarget();
		}
	}
	#endregion

	#region Private Methods
	#endregion

}
