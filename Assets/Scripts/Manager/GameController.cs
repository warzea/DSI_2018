using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using DG.Tweening;

public class GameController : ManagerParent 
{
	#region Variables
	public GameObject PlayerPrefab;
	public GameObject StartWeapon;
	public Transform PlayerPosSpawn;

	public Camera MainCam;
	public WeaponBox WeaponB;
	public float TimerCheckPlayer = 1;

	[HideInInspector]
	public Transform Garbage;

	[HideInInspector]
	public PlayerInfoInput[] GetPlayersInput;

	[HideInInspector]
	public List<GameObject> Players;

	List<PlayerController> getPlayerCont;
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
		getPlayerCont = new List<PlayerController>();
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
				getPlayerCont.Add(getPlayer.GetComponent<PlayerController>());
			}
		}

		Manager.AgentM.player = Players.ToArray ( );
		Manager.AgentM.InitGame();
	}

	void checkPlayer ( ) 
	{
		PlayerController[] playerCont = getPlayerCont.ToArray();
		GameObject lowLife = Players[0];
		GameObject maxLife = Players[0];
		GameObject boxWeapon = Players[0];

		int getID = 0;
		int a;

		for ( a = 0; a < playerCont.Length; a ++ )
		{
			if ( playerCont[a].LifePlayer < playerCont[getID].LifePlayer )
			{
				getID = a;
			}
			else if ( playerCont[a].LifePlayer == playerCont[getID].LifePlayer )
			{
				if(Random.Range(0,2) == 0)
				{
					getID = a;
				}
			}
		}

		lowLife = playerCont[getID].gameObject;
		
		for ( a = 0; a < playerCont.Length; a ++ )
		{
			if ( playerCont[a].LifePlayer > playerCont[getID].LifePlayer )
			{
				getID = a;
			}
			else if ( playerCont[a].LifePlayer == playerCont[getID].LifePlayer )
			{
				if(Random.Range(0,2) == 0)
				{
					getID = a;
				}
			}
		}

		maxLife = playerCont[getID].gameObject;
		
		for ( a = 0; a < playerCont.Length; a ++ )
		{
			if ( playerCont[a].driveBox )
			{
				boxWeapon = playerCont[a].gameObject;
				
				break;
			}
		}

		Manager.AgentM.ChangeEtatFocus(lowLife, maxLife, boxWeapon);

		DOVirtual.DelayedCall(TimerCheckPlayer, () => 
		{
			checkPlayer();
		});
	}
	#endregion
}

