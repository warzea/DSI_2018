using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour 
{
	#region Variables
	List<Transform> containerIt;
	public PlayerController thisPC;
	public Transform getPlayer;
	public Transform cauldron;
	bool canCauldron = true;
	#endregion
	
	
	#region Mono
	void Awake ( )
	{
		containerIt = new List<Transform>();
	}
	#endregion
	
	
	#region Public

	#endregion
	
	
	#region Private
	IEnumerator checkDist ( WaitForEndOfFrame thisF )
	{
		yield return thisF;

		if ( containerIt.Count == 0 )
		{
			thisPC.currInt = null;
			yield break;
		}

		Transform thisT = containerIt[0];

		if ( containerIt.Count > 1 )
		{
			for ( int a = 1; a < containerIt.Count; a ++ )
			{
				if ( Vector3.Distance(getPlayer.position, thisT.position) > Vector3.Distance(getPlayer.position, containerIt[a].position) )
				{
					thisT = containerIt[a];
				}
			}
		}

		StartCoroutine (checkDist(thisF));
		
		if ( cauldron != null && (thisT == null || Vector3.Distance(getPlayer.position, cauldron.position) < Vector3.Distance(getPlayer.position, thisT.position ) )  ) 
		{
			thisPC.canCauldron = true;
			thisPC.currInt = null;
		}
		else
		{
			thisPC.canCauldron = false;
			thisPC.currInt = containerIt[0].GetComponent<InteractAbstract>();
		}
	}

	void OnTriggerEnter(Collider thisColl)
    {
        if (thisColl.tag == Constants._ContainerItem)
        {
           containerIt.Add( thisColl.transform);

		   StartCoroutine (checkDist(new WaitForEndOfFrame()));
        }
		else if ( thisColl.tag == Constants._BoxTag)
		{
			cauldron = thisColl.transform;

			if ( containerIt.Count > 0 )
			{
				StartCoroutine (checkDist(new WaitForEndOfFrame()));
			}
			else
			{
				thisPC.canCauldron = true;
			}
		}
    }

	void OnTriggerExit (Collider thisColl)
    {
        if (thisColl.tag == Constants._ContainerItem)
        {
			Transform getTrans = thisColl.transform;
			for ( int a = 0; a < containerIt.Count; a ++ )
			{
				if ( containerIt[a] == getTrans )
				{
					containerIt.RemoveAt(a);
					break;
				}
			} 

			if ( containerIt.Count > 0 )
			{
		   		StartCoroutine (checkDist(new WaitForEndOfFrame()));
			}
			else
			{
				thisPC.currInt = null;
			}
        }
		else if ( thisColl.tag == Constants._BoxTag)
		{
			cauldron = null;
			thisPC.canCauldron = false;
		}
    }
	#endregion
}
