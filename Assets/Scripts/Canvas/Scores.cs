using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Scores : MonoBehaviour 
{
	#region Variables
	public List<ScoreInfo> AllScore;
	#endregion
	
	#region Mono
	#endregion
	
	#region Public Methods
	public void UpdateValue ( int scoreValue, ScoreType thisType, bool addValue = true )
	{
		ScoreInfo[] getAllScore = AllScore.ToArray();
		ScoreInfo getScoreinf;

		for ( int a = 0; a < getAllScore.Length; a ++ )
		{
			if ( getAllScore[a].ScoreTpe == thisType )
			{
				getScoreinf = getAllScore[a];
				
				if ( addValue )
				{
					getScoreinf.ScoreValue = scoreValue + getScoreinf.ScoreValue;
					getScoreinf.ScoreText.text = getScoreinf.ScoreValue.ToString();
				}
				else
				{
					getScoreinf.ScoreValue = scoreValue;
					getScoreinf.ScoreText.text = scoreValue.ToString();
				}

				break;
			}
		}	
	}
	#endregion

	#region Private Methods
	#endregion

}
