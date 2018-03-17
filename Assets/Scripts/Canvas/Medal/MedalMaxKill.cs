using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MedalMaxKill : AbstractMedal
{
	#region Variables
	#endregion

	#region Mono
	public override void StartCheck (PlayerController [ ] allPlayer)
	{
		thisPlayer = allPlayer [0];
		PlayerController thisPlayerEqua = null;

		int get = Manager.GameCont.NbrPlayer;

		if (get > 1)
		{
			for (int a = 1; a < get; a++)
			{
				if (thisPlayer.CurrKillScore < allPlayer [a].CurrKillScore)
				{
					thisPlayer = allPlayer [a];
					thisPlayerEqua = null;
				}
				else if (thisPlayer.CurrKillScore == allPlayer [a].CurrKillScore)
				{
					thisPlayerEqua = allPlayer [a];
				}
			}

			//Score = thisPlayer.CurrKillScore;
			GoTarget (thisPlayerEqua);
		}
		else
		{
			//Score = thisPlayer.CurrKillScore;
			GoTarget ( );
		}
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	#endregion

}