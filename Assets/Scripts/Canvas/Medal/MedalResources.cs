using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MedalResources : AbstractMedal
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
				if (thisPlayer.CurrLootScore < allPlayer [a].CurrLootScore)
				{
					thisPlayer = allPlayer [a];
					thisPlayerEqua = null;
				}
				else if (thisPlayer.CurrLootScore == allPlayer [a].CurrLootScore)
				{
					thisPlayerEqua = allPlayer [a];
				}
			}

			//Score = thisPlayer.CurrLootScore;
			GoTarget (thisPlayerEqua);
		}
		else
		{
			//Score = thisPlayer.CurrLootScore;
			GoTarget ( );
		}
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	#endregion

}