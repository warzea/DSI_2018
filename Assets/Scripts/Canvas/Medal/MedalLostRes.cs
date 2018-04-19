using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MedalLostRes : AbstractMedal
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
				if (thisPlayer.LostItem < allPlayer [a].LostItem)
				{
					thisPlayer = allPlayer [a];
					thisPlayerEqua = null;
				}
				else if (thisPlayer.LostItem == allPlayer [a].LostItem)
				{
					thisPlayerEqua = allPlayer [a];
				}
			}

			//Score = thisPlayerEqua.LostItem;
			GoTarget (thisPlayerEqua);
		}
		else
		{
			//Score = thisPlayer.LostItem;
			GoTarget ( );
		}
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	#endregion

}