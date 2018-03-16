using System.Collections;
using System.Collections.Generic;

using Rewired;

using UnityEngine;
using UnityEngine.SceneManagement;

public class NbrPlayerPlaying : MonoBehaviour
{
	#region Variables
	public static NbrPlayerPlaying NbrPP;
	public List<infoP> NbrPlayer;
	public Scene thisScene;
	#endregion

	#region Mono
	void Awake ()
	{
		if (NbrPP != null)
		{
			Destroy (gameObject);
		}
		else
		{
			DontDestroyOnLoad (gameObject);
			NbrPP = this;
		}
	}
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	#endregion

	public void DestAll ()
	{
		foreach (GameObject thisT in thisScene.GetRootGameObjects ())
		{
			Destroy (thisT);
		}
	}
}

public class infoP
{
	public Player thisP;
	public int ID;
	public bool ready = false;
}