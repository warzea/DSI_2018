using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class GameController : ManagerParent 
{
	#region Variables
	public GameObject PlayerPrefab;
	public Transform PlayerPosSpawn;

	[HideInInspector]
	public Transform Gargabe;

	[HideInInspector]
	public PlayerInfoInput[] GetPlayersInput;
	#endregion

	#region Mono
	#endregion

	#region Public Methods
	public void SpawnPlayer ( )
	{
		PlayerInfoInput[] getPlayers = GetPlayersInput;
		GameObject getPlayer;

		for ( int a = 0; a < getPlayers.Length; a ++ )
		{
			getPlayers[a].ReadyPlayer = false;
			
			if ( getPlayers[a].EnablePlayer )
			{
				getPlayer = ( GameObject ) Instantiate ( PlayerPrefab );

				getPlayer.transform.position = PlayerPosSpawn.position;
				getPlayer.GetComponent<PlayerController>().IdPlayer = getPlayers[a].IdPlayer;
			}
		}
	}
	#endregion

	#region Private Methods
	protected override void InitializeManager ( )
	{
		Gargabe = transform.Find("Garbage");
		GetPlayersInput = new PlayerInfoInput[4];

		for ( int a = 0; a < 4; a ++ )
		{
			GetPlayersInput[a] = new PlayerInfoInput ( );
			GetPlayersInput[a].IdPlayer = a;
			GetPlayersInput[a].InputPlayer = ReInput.players.GetPlayer ( a );
			GetPlayersInput[a].EnablePlayer = false;
		}
	}
	#endregion
}

