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
		Debug.Log("1");
		if ( equaContr.NbrAward > 2 )
		{
			gameObject.SetActive(false);
			
			return;
		}
		Debug.Log("2");

		Manager.Ui.EndScreenMedals(thisTrans, ThisPlayer.NbrAward);
		ThisPlayer.NbrAward ++;

		if ( equaContr != null )
		{
			equaContr.NbrAward ++;
			GameObject thisObj = (GameObject) Instantiate (gameObject, thisTrans.parent );
			thisObj.GetComponent<AbstractMedal>().ThisPlayer = equaContr;
			thisObj.GetComponent<AbstractMedal>().GoTarget();
		}
	}
	#endregion

	#region Private Methods
	#endregion

}
