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

		if (allPlayer.Length > 1)
		{
			for (int a = 1; a < allPlayer.Length; a++)
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

			Score = thisPlayerEqua.LostItem;
			GoTarget (thisPlayerEqua);
		}
		else
		{
			Score = thisPlayerEqua.LostItem;
			GoTarget ( );
		}
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	#endregion

}