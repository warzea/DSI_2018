using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : ManagerParent 
{
	#region Variables
	public GameObject PlayerPrefab;

	[HideInInspector]
	public Transform Gargabe;
	#endregion

	#region Mono
	#endregion

	#region Public Methods
	
	#endregion

	#region Private Methods
	protected override void InitializeManager ( )
	{
		Gargabe = transform.Find("Garbage");
	}
	#endregion
}
