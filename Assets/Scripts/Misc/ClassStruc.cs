using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using DG.Tweening;

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
	public int ScoreValue;
	public int FinalScore;

	public Text ScoreText;
	public ScoreType ScoreTpe;
	public Tween ThisTween;
}

// ------- 


// ------- PlayerController
public class EnemyInfo
{
	public TypeEnemy ThisType;
	public int NbrEnemy = 0;	
}

// -------


