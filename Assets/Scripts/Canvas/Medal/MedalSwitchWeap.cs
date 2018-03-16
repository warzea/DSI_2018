using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MedalSwitchWeap : AbstractMedal
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
				if (thisPlayer.WeaponSwitch < allPlayer [a].WeaponSwitch)
				{
					thisPlayer = allPlayer [a];
					thisPlayerEqua = null;
				}
				else if (thisPlayer.WeaponSwitch == allPlayer [a].WeaponSwitch)
				{
					thisPlayerEqua = allPlayer [a];
				}
			}
			Score = thisPlayer.WeaponSwitch;
			GoTarget (thisPlayerEqua);
		}
		else
		{
			Score = thisPlayer.WeaponSwitch;
			GoTarget ();
		}
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	#endregion

}