using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MedalDist : AbstractMedal
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
				if (thisPlayer.TotalDist < allPlayer [a].TotalDist)
				{
					thisPlayer = allPlayer [a];
					thisPlayerEqua = null;
				}
				else if (thisPlayer.TotalDist == allPlayer [a].TotalDist)
				{
					thisPlayerEqua = allPlayer [a];
				}
			}

			Score = thisPlayer.TotalDist;
			GoTarget (thisPlayerEqua);
		}
		else
		{
			Score = thisPlayer.TotalDist;
			GoTarget ();
		}
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	#endregion

}