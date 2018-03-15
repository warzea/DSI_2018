using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MedalTouchAct : AbstractMedal
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
				if (thisPlayer.NbrTouchInteract < allPlayer [a].NbrTouchInteract)
				{
					thisPlayer = allPlayer [a];
					thisPlayerEqua = null;
				}
				else if (thisPlayer.NbrTouchInteract == allPlayer [a].NbrTouchInteract)
				{
					thisPlayerEqua = allPlayer [a];
				}
			}

			Score = thisPlayer.NbrTouchInteract;
			GoTarget (thisPlayerEqua);
		}
		else
		{
			Score = thisPlayer.NbrTouchInteract;
			GoTarget ( );
		}
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	#endregion

}