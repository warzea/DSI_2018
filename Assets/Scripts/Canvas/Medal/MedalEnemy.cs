using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalEnemy : AbstractMedal 
{
	#region Variables
	#endregion
	
	#region Mono
	#endregion
	
	#region Public Methods
	public override void StartCheck ( PlayerController[] allPlayer )
	{
		PlayerController thisPlayer = allPlayer[0];
		PlayerController thisPlayerEqua = null;

		if ( allPlayer.Length > 1 )
		{
			for ( int a = 1; a < allPlayer.Length; a ++ )
			{
				if ( thisPlayer.NbrEnemy < allPlayer[a].NbrEnemy )
				{
					thisPlayer = allPlayer[a];
					thisPlayerEqua = null;
				}
				else if ( thisPlayer.NbrEnemy == allPlayer[a].NbrEnemy  )
				{
					thisPlayerEqua = allPlayer[a];
				}
			}
			GoTarget(thisPlayerEqua);

			int b;
			for ( int a = 1; a < allPlayer.Length; a ++ )
			{
				thisPlayer = allPlayer[0];
				thisPlayerEqua = null;

				for ( b = 0; b < allPlayer.Length; b ++ )
				{
					if ( thisPlayer.AllEnemy[b].NbrEnemy < allPlayer[a].AllEnemy[b].NbrEnemy )
					{
						thisPlayer = allPlayer[a];
						thisPlayerEqua = null;
					}
					else if ( thisPlayer.NbrEnemy == allPlayer[a].NbrEnemy  )
					{
						thisPlayerEqua = allPlayer[a];
					}
				}

				GameObject thisObj = (GameObject) Instantiate (gameObject, thisTrans.parent );
				thisObj.GetComponent<AbstractMedal>().ThisPlayer = thisPlayerEqua;
				thisObj.GetComponent<AbstractMedal>().GoTarget();
				
				if ( thisPlayerEqua != null )
				{
					thisObj = (GameObject) Instantiate (gameObject, thisTrans.parent );
					thisObj.GetComponent<AbstractMedal>().ThisPlayer = thisPlayerEqua;
					thisObj.GetComponent<AbstractMedal>().GoTarget();
				}
			}
		}
	}

	#endregion

	#region Private Methods
	#endregion

}

