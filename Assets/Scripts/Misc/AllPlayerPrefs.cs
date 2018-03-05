using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;

public static class AllPlayerPrefs
{
    #region Get Methods
	public static int GetIntValue ( string thisString )
	{
		return PlayerPrefs.GetInt ( thisString, 0 );
	}

	public static bool GetBoolValue ( string thisString )
	{
		string getVal = PlayerPrefs.GetString ( thisString, "Nope" );

		if ( getVal == "Nope" )
		{
			return false;
		}
		else
		{
			return true;
		}
	}
    #endregion

	#region Set Methods
	public static void SetIntValue ( string thisString, int thisValue, bool addition = true )
	{
		if ( addition )
		{
			PlayerPrefs.SetInt ( thisString, GetIntValue ( thisString ) + thisValue );
		}
		else
		{
			PlayerPrefs.SetInt ( thisString, thisValue );
		}
	}

	public static void SetStringValue ( string thisName, string value = "ok" )
	{
		PlayerPrefs.SetString ( thisName, value );
	}
	#endregion
}
