using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MedalStun : AbstractMedal
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
				if (thisPlayer.NbrDead < allPlayer [a].NbrDead)
				{
					thisPlayer = allPlayer [a];
					thisPlayerEqua = null;
				}
				else if (thisPlayer.NbrDead == allPlayer [a].NbrDead)
				{
					thisPlayerEqua = allPlayer [a];
				}
			}

			//Score = thisPlayer.NbrDead;
			GoTarget (thisPlayerEqua);
		}
		else
		{
			//Score = thisPlayer.NbrDead;

			GoTarget ( );
		}
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	#endregion

}