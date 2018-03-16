using System.Collections;
using System.Collections.Generic;

using Rewired;

using UnityEngine;

public class NbrPlayerPlaying : MonoBehaviour
{
	#region Variables
	public List<infoP> NbrPlayer;
	#endregion

	#region Mono
	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	#endregion

}

public class infoP
{
	public Player thisP;
	public int ID;
	public bool ready = false;
}