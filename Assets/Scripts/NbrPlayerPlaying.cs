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

		NbrPlayer = new List<infoP> ();

		NbrPlayer.Add (new infoP ());
		NbrPlayer.Add (new infoP ());
		NbrPlayer.Add (new infoP ());
		NbrPlayer.Add (new infoP ());

		NbrPlayer [0].ID = 0;
		NbrPlayer [1].ID = 1;
		NbrPlayer [2].ID = 2;
		NbrPlayer [3].ID = 3;
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

[System.Serializable]
public class infoP
{
	public Player thisP;
	public int ID;
	public bool ready = false;
}