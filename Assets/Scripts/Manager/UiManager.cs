using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using System.Diagnostics;
using System.Runtime.CompilerServices;

public class UiManager : ManagerParent
{
	#region Variables
	Dictionary <MenuType, UiParent> AllMenu;
	MenuType menuOpen;
	#endregion

	#region Mono
	#endregion

	#region Public Methods
	public void OpenThisMenu ( MenuType thisType, MenuTokenAbstract GetTok = null )
	{
		UiParent thisUi;
		//Debug.Log ( "open " + thisType );

		if ( AllMenu.TryGetValue ( thisType, out thisUi ) )
		{
			if ( menuOpen == thisType )
			{
				return;
			}
            
			if ( menuOpen != MenuType.Nothing )
			{
				CloseThisMenu ( );
			}

			menuOpen = thisType;
			thisUi.OpenThis ( GetTok );
		}
    }

	public void CloseThisMenu ( )
	{
		UiParent thisUi;

		if ( menuOpen != MenuType.Nothing && AllMenu.TryGetValue ( menuOpen, out thisUi ) )
		{
			thisUi.CloseThis ( );
			menuOpen = MenuType.Nothing;
		}
	}
	#endregion

	#region Private Methods
	protected override void InitializeManager ( )
	{
		Object[] getAllMenu = Resources.LoadAll ( "Menu" );
		Dictionary<MenuType, UiParent> setAllMenu = new Dictionary<MenuType, UiParent> ( getAllMenu.Length );

		Transform Parent = transform.GetChild(0);;
		GameObject thisMenu;
		UiParent thisUi;

		for ( int a = 0; a < getAllMenu.Length; a++ )
		{
			thisMenu = ( GameObject ) Instantiate ( getAllMenu [ a ], Parent );
			thisUi = thisMenu.GetComponent<UiParent> ( );
			thisUi.Initialize ( );
			setAllMenu.Add ( thisUi.ThisMenu, thisUi );
		}

		AllMenu = setAllMenu;
	}

	#endregion
}
