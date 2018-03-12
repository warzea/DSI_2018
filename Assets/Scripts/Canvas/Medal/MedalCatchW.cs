using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalCatchW : AbstractMedal 
{
	#region Variables
	#endregion
	
	#region Mono
	public override void StartCheck ( PlayerController[] allPlayer )
	{
		PlayerController thisPlayer = allPlayer[0];
		PlayerController thisPlayerEqua = null;

		if ( allPlayer.Length > 1 )
		{
			for ( int a = 1; a < allPlayer.Length; a ++ )
			{
				if ( thisPlayer.WeaponCatch < allPlayer[a].WeaponCatch )
				{
					thisPlayer = allPlayer[a];
					thisPlayerEqua = null;
				}
				else if ( thisPlayer.WeaponCatch == allPlayer[a].WeaponCatch  )
				{
					thisPlayerEqua = allPlayer[a];
				}
			}
			
			GoTarget(thisPlayerEqua);
		}
	}
	#endregion
	
	#region Public Methods
	
	#endregion

	#region Private Methods
	#endregion

}
