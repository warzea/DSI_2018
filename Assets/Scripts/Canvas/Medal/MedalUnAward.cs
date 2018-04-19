using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MedalUnAward : AbstractMedal
{
	#region Variables
	#endregion

	#region Mono
	public override void StartCheck (PlayerController [] allPlayer)
	{
		int get = Manager.GameCont.NbrPlayer;

		if (get > 1)
		{
			for (int a = 1; a < get; a++)
			{
				if (allPlayer [a].NbrAward == 0)
				{
					GameObject thisObj = (GameObject) Instantiate (gameObject, thisTrans.parent);
					thisObj.GetComponent<AbstractMedal> ().thisPlayer = allPlayer [a];
					thisObj.GetComponent<AbstractMedal> ().GoTarget ();
				}
			}

			gameObject.SetActive (false);
		}
		else if (allPlayer [0].NbrAward == 0)
		{
			thisPlayer = allPlayer [0];
			GoTarget ();
		}
		else
		{
			gameObject.SetActive (false);
		}
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	#endregion

}