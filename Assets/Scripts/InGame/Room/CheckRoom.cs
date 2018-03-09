using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CheckRoom : MonoBehaviour 
{
	#region Variables
	public float TimerRoom = 4;
	public Door[] AllDoor;
	int nbrPlayer = 0;
	bool launch = false;
	#endregion
	
	#region Mono
	#endregion
	
	#region Public
	#endregion
	
	#region Private
	void launchRoom () 
	{
		if ( !launch )
		{
			launch = true;

			for (int a = 0; a < AllDoor.Length; a++)
			{
				AllDoor[a].OpenDoor(false);
			}

			DOVirtual.DelayedCall(TimerRoom, () =>
			{
				for (int a = 0; a < AllDoor.Length; a++)
				{
					AllDoor[a].OpenDoor(true);
				}
			});
		}
	}

	void OnTriggerEnter ( Collider thisColl )
	{
		if ( thisColl.tag == Constants._Player )
		{
			nbrPlayer ++;
			if ( nbrPlayer == Manager.GameCont.Players.Count )
			{
				launchRoom ( );
			}
		}
	}
	
	void OnTriggerExit ( Collider thisColl )
	{
		if ( thisColl.tag == Constants._Player )
		{
			nbrPlayer --;
		}
	}
	#endregion
}
