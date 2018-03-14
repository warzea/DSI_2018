using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MedalUnDead : AbstractMedal
{
	#region Variables
	#endregion

	#region Mono
	public override void StartCheck (PlayerController [] allPlayer)
	{

		if (allPlayer.Length > 1)
		{
			for (int a = 0; a < allPlayer.Length; a++)
			{
				if (allPlayer [a].NbrDead == 0)
				{
					GameObject thisObj = (GameObject) Instantiate (gameObject, thisTrans.parent);
					thisObj.GetComponent<AbstractMedal> ().thisPlayer = allPlayer [a];
					thisObj.GetComponent<AbstractMedal> ().GoTarget ();
				}
			}
		}
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	#endregion

}