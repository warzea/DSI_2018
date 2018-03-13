using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteEtatAgent : MonoBehaviour 
{
	#region Variables
	public bool NewEtatAgent;
	bool check = true;
	#endregion
	
	#region Mono
	#endregion
	
	#region Public Methods
	
	#endregion

	#region Private Methods
	void OnTriggerExit (Collider thisColl)
	{
		if (thisColl.tag == Constants._Player && check) 
		{
			check = false;
			Manager.GameCont.EtatAgent(NewEtatAgent);
		}
	}
	#endregion

}
