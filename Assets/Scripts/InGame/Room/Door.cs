using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Door : MonoBehaviour
{

	NavMeshObstacle mymesh;

	void Start ()
	{
		mymesh = transform.GetComponent<NavMeshObstacle> ();
	}

	#region Variables

	#endregion

	#region Mono

	#endregion

	#region Public

	public void OpenDoor (bool openIt)
	{

		if (openIt) {
			GetComponent<Collider> ().enabled = false;
			GetComponent<Animator> ().SetTrigger ("Opening");
			mymesh.enabled = false;
		} else {
			GetComponent<Collider> ().enabled = true;
			GetComponent<Animator> ().SetTrigger ("close");
			mymesh.enabled = true;
		}
	}

	#endregion

	#region Private

	#endregion
}