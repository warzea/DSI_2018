using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using System.Diagnostics;
using System.Runtime.CompilerServices;

public class UiManager : ManagerParent
{
	#region Variables
	public Transform MenuParent;
	Dictionary <MenuType, UiParent> AllMenu;

	#endregion

	#region Mono
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	protected override void InitializeManager ( )
	{
		Object[] getAllMenu = Resources.LoadAll ( "Menu" );
		Dictionary<MenuType, UiParent> setAllMenu = new Dictionary<MenuType, UiParent> ( getAllMenu.Length );

		GameObject thisMenu;
		UiParent thisUi;

		for ( int a = 0; a < getAllMenu.Length; a++ )
		{
			thisMenu = ( GameObject ) Instantiate ( getAllMenu [ a ], MenuParent );
			thisUi = thisMenu.GetComponent<UiParent> ( );
			thisUi.Initialize ( );
			setAllMenu.Add ( thisUi.ThisMenu, thisUi );
		}

		AllMenu = setAllMenu;
	}

	#endregion
}
