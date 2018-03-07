using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class GameController : ManagerParent 
{
	#region Variables
	public GameObject PlayerPrefab;
	public GameObject StartWeapon;
	public Transform PlayerPosSpawn;

	public Camera MainCam;
	public WeaponBox WeaponB;

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
		if ( WeaponB == null )
		{
			WeaponB = (WeaponBox)FindObjectOfType(typeof(WeaponBox));
		}

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
		PlayerController getPC;
		GameObject getPlayer;
		GameObject getWeapon;

		for ( int a = 0; a < getPlayers.Length; a ++ )
		{
			getPlayers[a].ReadyPlayer = false;
			
			if ( getPlayers[a].EnablePlayer )
			{
				getPlayer = ( GameObject ) Instantiate ( PlayerPrefab );

				getPlayer.transform.position = PlayerPosSpawn.position + new Vector3 ( a * 1.5f, 0, 0 );
				getPC = getPlayer.GetComponent<PlayerController>();
				getPC.IdPlayer = getPlayers[a].IdPlayer;

				getWeapon = ( GameObject ) Instantiate ( StartWeapon, getPC.WeaponPos.transform );
				getWeapon.transform.localPosition = Vector3.zero;
				getWeapon.transform.localRotation = Quaternion.identity;

				getPC.UpdateWeapon ( getWeapon.GetComponent<WeaponAbstract>() );
				Players.Add ( getPlayer );
			}
		}

		Manager.AgentM.player = Players.ToArray ( );
		Manager.AgentM.InitGame();
	}
	#endregion
}

