using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalAim : AbstractMedal 
{
	#region Variables
	#endregion
	
	#region Mono
	public override void StartCheck ( PlayerController[] allPlayer )
	{
		PlayerController thisPlayer = allPlayer[0];
		PlayerController thisPlayerEqua = null;

		float cal1;
		float cal2;

		if ( allPlayer.Length > 1 )
		{
			for ( int a = 1; a < allPlayer.Length; a ++ )
			{
				cal1 = thisPlayer.ShootSucceed / thisPlayer.ShootBullet;
				cal2 = allPlayer[a].ShootSucceed / allPlayer[a].ShootBullet;
				if ( cal1 < cal2 )
				{
					thisPlayer = allPlayer[a];
					thisPlayerEqua = null;
				}
				else if ( cal1 == cal2  )
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
