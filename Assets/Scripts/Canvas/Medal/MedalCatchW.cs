using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MedalCatchW : AbstractMedal
{
	#region Variables
	#endregion

	#region Mono
	public override void StartCheck (PlayerController [] allPlayer)
	{
		thisPlayer = allPlayer [0];
		PlayerController thisPlayerEqua = null;
		int get = Manager.GameCont.NbrPlayer;

		if (get > 1)
		{
			for (int a = 1; a < get; a++)
			{
				if (thisPlayer.WeaponCatch < allPlayer [a].WeaponCatch)
				{
					thisPlayer = allPlayer [a];
					thisPlayerEqua = null;
				}
				else if (thisPlayer.WeaponCatch == allPlayer [a].WeaponCatch)
				{
					thisPlayerEqua = allPlayer [a];
				}
			}

			Score = thisPlayer.WeaponCatch;
			GoTarget (thisPlayerEqua);
		}
		else
		{
			Score = thisPlayer.WeaponCatch;
			GoTarget ();
		}
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	#endregion

}