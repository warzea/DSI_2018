using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Door : MonoBehaviour
{
	#region Variables

	#endregion

	#region Mono

	#endregion

	#region Public
	public void OpenDoor (bool openIt)
	{

		if (openIt)
		{
			GetComponent<Collider> ().enabled = false;
			GetComponent<Animator> ().SetTrigger ("Opening");
		}
		else
		{
			GetComponent<Collider> ().enabled = true;
			GetComponent<Animator> ().SetTrigger ("close");
		}
	}
	#endregion

	#region Private

	#endregion
}