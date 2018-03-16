using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MedalAim : AbstractMedal
{
	#region Variables
	#endregion

	#region Mono
	public override void StartCheck (PlayerController [ ] allPlayer)
	{
		thisPlayer = allPlayer [0];
		PlayerController thisPlayerEqua = null;

		float cal1;
		float cal2;
		int get = Manager.GameCont.NbrPlayer;

		if (get > 1)
		{
			for (int a = 1; a < get; a++)
			{
				if (thisPlayer.ShootBullet == 0)
				{
					thisPlayer = allPlayer [a];
					continue;
				}

				cal1 = thisPlayer.ShootSucceed / thisPlayer.ShootBullet;
				cal2 = allPlayer [a].ShootSucceed / allPlayer [a].ShootBullet;
				if (cal1 > cal2)
				{
					thisPlayer = allPlayer [a];
					thisPlayerEqua = null;
				}
				else if (cal1 == cal2)
				{
					thisPlayerEqua = allPlayer [a];
				}
			}
			//Score = thisPlayer.ShootSucceed / thisPlayer.ShootBullet;
			GoTarget (thisPlayerEqua);
		}
		else
		{
			//Score = thisPlayer.ShootSucceed / thisPlayer.ShootBullet;
			GoTarget ( );
		}
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	#endregion

}