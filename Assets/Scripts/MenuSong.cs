using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MenuSong : MonoBehaviour
{
	#region Variables
	public AudioManager ThisAudM;
	#endregion

	#region Mono
	void Awake ()
	{
		ThisAudM = GetComponent<AudioManager> ();
		ThisAudM.Initialize ();
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	#endregion

}