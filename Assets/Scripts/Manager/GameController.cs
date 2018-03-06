using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class GameController : ManagerParent 
{
	#region Variables
	public GameObject PlayerPrefab;
	public Transform PlayerPosSpawn;

	public Camera MainCam;

	[HideInInspector]
	public Transform Garbage;

	[HideInInspector]
	public PlayerInfoInput[] GetPlayersInput;

	[HideInInspector]
	public List<GameObject> Players;
	#endregion

	#region Mono
	#endregion

	#region Public Methods

	public void StartGame ( )
	{
		SpawnPlayer ( );
		MainCam.GetComponent<CameraFollow>().InitGame();
	}

	public void EndGame ( )
	{
		Players.Clear ( );

		Manager.Ui.OpenThisMenu ( MenuType.SelectPlayer );
	}
	#endregion

	#region Private Methods
	protected override void InitializeManager ( )
	{
		Garbage = transform.Find("Garbage");
		GetPlayersInput = new PlayerInfoInput[4];

		for ( int a = 0; a < 4; a ++ )
		{
			GetPlayersInput[a] = new PlayerInfoInput ( );
			GetPlayersInput[a].IdPlayer = a;
			GetPlayersInput[a].InputPlayer = ReInput.players.GetPlayer ( a );
			GetPlayersInput[a].EnablePlayer = false;
		}
	}

	void SpawnPlayer ( )
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

				Players.Add ( getPlayer );
			}
		}
	}
	#endregion
}

