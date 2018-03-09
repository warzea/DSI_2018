using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

// ------- GameController
public class PlayerInfoInput 
{
	public Player InputPlayer;
	public int IdPlayer;
	public bool EnablePlayer;
	public bool ReadyPlayer;
}
// ------- 


// ------- WeaponBox
public class PlayerWeapon
{
	public int IDPlayer;

	public GameObject CurrObj;
}

// ------- 

// ------- WeaponBox

[System.Serializable]
public class ScoreInfo
{
	[HideInInspector]
	public int ScoreValue;

	public Text ScoreText;
	public ScoreType ScoreTpe;
}

// ------- 


// ------- PlayerController
public class EnemyInfo
{
	public TypeEnemy ThisType;
	public int NbrEnemy = 0;	
}

// -------


