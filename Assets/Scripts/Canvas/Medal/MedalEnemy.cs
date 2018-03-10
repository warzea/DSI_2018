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
		bool equality = false;
		int getEqua = 0;
		if ( allPlayer.Length > 1 )
		{
			for ( int a = 1; a < allPlayer.Length; a ++ )
			{
				if ( thisPlayer.NbrEnemy > allPlayer[a].NbrEnemy )
				{

				}
				else if ( thisPlayer.NbrEnemy == allPlayer[a].NbrEnemy  )
				{
					
				}
			}
		}
		
	}
	#endregion

	#region Private Methods
	#endregion

}

