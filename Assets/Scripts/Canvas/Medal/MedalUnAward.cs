using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalUnAward : AbstractMedal 
{
	#region Variables
	#endregion
	
	#region Mono
	public override void StartCheck ( PlayerController[] allPlayer )
	{
		if ( allPlayer.Length > 1 )
		{
			for ( int a = 0; a < allPlayer.Length; a ++ )
			{
				if ( allPlayer[a].NbrAward == 0 )
				{
					GameObject thisObj = (GameObject) Instantiate (gameObject, thisTrans.parent );
					thisObj.GetComponent<AbstractMedal>().ThisPlayer = allPlayer[a];
					thisObj.GetComponent<AbstractMedal>().GoTarget();
				}
			}
		}
	}
	#endregion
	
	#region Public Methods
	
	#endregion

	#region Private Methods
	#endregion

}
