using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class MenuSelect : UiParent 
{
	#region Variables
	public override MenuType ThisMenu
	{
		get 
		{
			return MenuType.SelectPlayer;
		}
	}

	PlayerInfoInput[] getPlayers;
	#endregion
	
	#region Mono
	void Update ( )
	{
		bool allPlayerRdy = true;
		int nbrPlayer = 0;

		for ( int a = 0; a < 4; a ++ )
		{
			if ( getPlayers[a].InputPlayer.GetButtonDown("Cauldron") )
			{
				Debug.Log("interact");
				getPlayers[a].EnablePlayer = true;
			}
			else if ( getPlayers[a].InputPlayer.GetButtonDown("Cancel") )
			{
				getPlayers[a].EnablePlayer = false;
				getPlayers[a].ReadyPlayer = false;
			}
			else if ( getPlayers[a].InputPlayer.GetButtonDown("Start") )
			{
				Debug.Log("Start");
				if ( getPlayers[a].EnablePlayer )
				{
					getPlayers[a].ReadyPlayer = true;
				}
			}

			if ( getPlayers[a].EnablePlayer )
			{
				nbrPlayer++;
				if ( !getPlayers[a].ReadyPlayer )
				{
					allPlayerRdy = false;
				}
			}
		}

		if ( allPlayerRdy && nbrPlayer > 0 )
		{
			Manager.Ui.CloseThisMenu ( );
		}
	}	
	#endregion
	
	#region Public Methods
	public override void OpenThis ( MenuTokenAbstract GetTok = null )
	{
		gameObject.SetActive ( true );
	}

	public override void CloseThis ( )
	{
		gameObject.SetActive ( false );

		Manager.GameCont.StartGame ( );
	}
	#endregion

	#region Private Methods
	protected override void InitializeUi ( )
	{
		getPlayers = Manager.GameCont.GetPlayersInput;
	}
	#endregion
}

