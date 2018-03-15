using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EndTrigger : MonoBehaviour
{
	#region Variables
	bool check = false;
	#endregion

	#region Mono
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	void OnTriggerEnter (Collider thisColl)
	{
		if (!check && thisColl.gameObject == Manager.GameCont.WeaponB.gameObject)
		{
			GameObject [] players = Manager.GameCont.Players.ToArray ();

			for (int a = 0; a < players.Length; a++)
			{
				Debug.Log ("test");
				Debug.Log (players [a].name);
				Destroy (players [a].GetComponent<PlayerController> ());
			}

			Destroy (thisColl.GetComponent<WeaponBox> ());

			check = true;
			Manager.GameCont.EndGame ();
		}
	}
	#endregion

}