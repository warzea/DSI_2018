using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalEnemy : AbstractMedal 
{
	#region Variables
	public PlayerController ThisPlayer;
	public float Score;
	#endregion
	
	#region Mono
	void Awake ( )
	{
	}
	#endregion
	
	#region Public Methods
	public override void StartCheck ( PlayerController[] allPlayer )
	{
		PlayerController thisPlayer = allPlayer[0];
		for ( int a = 0; a < allPlayer.Length; a ++ )
		{

		}
	}
	#endregion

	#region Private Methods
	#endregion

}

