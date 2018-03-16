using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MedalCauldron : AbstractMedal
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
				if (thisPlayer.TimeWBox < allPlayer [a].TimeWBox)
				{
					thisPlayer = allPlayer [a];
					thisPlayerEqua = null;
				}
				else if (thisPlayer.TimeWBox == allPlayer [a].TimeWBox)
				{
					thisPlayerEqua = allPlayer [a];
				}
			}

			Score = thisPlayer.TimeWBox;
			GoTarget (thisPlayerEqua);
		}
		else
		{
			GoTarget ();
		}
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	#endregion

}