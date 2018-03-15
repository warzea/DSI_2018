using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MenuSong : MonoBehaviour
{
	#region Variables
	AudioManager ThisAudM;
	#endregion

	#region Mono
	void Awake ()
	{
		ThisAudM = GetComponent<AudioManager> ();
		ThisAudM.Initialize ();
	}
	#endregion

	#region Public Methods
	public void LaunchSong (string NameSong)
	{
		ThisAudM.OpenAudio (AudioType.Other, NameSong);
	}
	#endregion

	#region Private Methods
	#endregion

}