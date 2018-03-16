using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MedalChest : AbstractMedal
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
				if (thisPlayer.NbrChest < allPlayer [a].NbrChest)
				{
					thisPlayer = allPlayer [a];
					thisPlayerEqua = null;
				}
				else if (thisPlayer.NbrChest == allPlayer [a].NbrChest)
				{
					thisPlayerEqua = allPlayer [a];
				}
			}

			Score = thisPlayer.NbrChest;
			GoTarget (thisPlayerEqua);
		}
		else
		{
			Score = thisPlayer.NbrChest;
			GoTarget ();
		}
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	#endregion

}