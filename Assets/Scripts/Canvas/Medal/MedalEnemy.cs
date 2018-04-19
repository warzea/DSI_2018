using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MedalEnemy : AbstractMedal
{
	#region Variables
	#endregion

	#region Mono
	#endregion

	#region Public Methods
	public override void StartCheck (PlayerController [ ] allPlayer)
	{
		thisPlayer = allPlayer [0];
		PlayerController thisPlayerEqua = null;
		int get = Manager.GameCont.NbrPlayer;

		if (get > 1)
		{
			for (int a = 1; a < get; a++)
			{
				if (thisPlayer.NbrEnemy < allPlayer [a].NbrEnemy)
				{
					thisPlayer = allPlayer [a];
					thisPlayerEqua = null;
				}
				else if (thisPlayer.NbrEnemy == allPlayer [a].NbrEnemy)
				{
					thisPlayerEqua = allPlayer [a];
				}
			}
			//Score = thisPlayer.NbrEnemy;
			GoTarget (thisPlayerEqua);

			/*int b;
			for (int a = 1; a < allPlayer.Length; a++)
			{
				thisPlayer = allPlayer [0];
				thisPlayerEqua = null;

				for (b = 0; b < allPlayer.Length; b++)
				{
					for (int c = 0; c < allPlayer [a].AllEnemy.Count; c++)
					{
						if (thisPlayer.AllEnemy [b].NbrEnemy < allPlayer [a].AllEnemy [b].NbrEnemy)
						{
							thisPlayer = allPlayer [a];
							thisPlayerEqua = null;
						}
						else if (thisPlayer.AllEnemy [b].NbrEnemy == allPlayer [a].AllEnemy [b].NbrEnemy)
						{
							thisPlayerEqua = allPlayer [a];
						}
					}

				}

				GameObject thisObj = (GameObject)Instantiate (gameObject, thisTrans.parent);
				thisObj.GetComponent<AbstractMedal> ( ).thisPlayer = thisPlayerEqua;
				thisObj.GetComponent<AbstractMedal> ( ).GoTarget ( );
				thisObj.GetComponent<AbstractMedal> ( ).Score = thisPlayer.NbrEnemy;

				if (thisPlayerEqua != null)
				{
					thisObj = (GameObject)Instantiate (gameObject, thisTrans.parent);
					thisObj.GetComponent<AbstractMedal> ( ).thisPlayer = thisPlayerEqua;
					thisObj.GetComponent<AbstractMedal> ( ).GoTarget ( );
				}
			}*/
		}
		else
		{
			//Score = thisPlayer.NbrEnemy;
			GoTarget ( );
		}
	}

	#endregion

	#region Private Methods
	#endregion

}