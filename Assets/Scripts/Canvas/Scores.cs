using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
	#region Variables
	public List<ScoreInfo> AllScore;
	#endregion

	#region Mono
	#endregion

	#region Public Methods
	public void UpdateValue (int scoreValue, ScoreType thisType, bool addValue = true, System.Action thisAct = null)
	{
		ScoreInfo [] getAllScore = AllScore.ToArray ();
		ScoreInfo getScoreinf;

		for (int a = 0; a < getAllScore.Length; a++)
		{
			if (getAllScore [a].ScoreTpe == thisType)
			{
				getScoreinf = getAllScore [a];
				getScoreinf.ThisTween.Kill (true);
				if (addValue)
				{
					getScoreinf.FinalScore = scoreValue + getScoreinf.ScoreValue;
					getScoreinf.ThisTween = DOTween.To (() => getScoreinf.ScoreValue, x => getScoreinf.ScoreValue = x, getScoreinf.FinalScore, 1);
					//getScoreinf.ScoreValue = scoreValue + getScoreinf.ScoreValue;
					getScoreinf.ScoreText.text = getScoreinf.ScoreValue.ToString ();
				}
				else
				{
					getScoreinf.FinalScore = scoreValue;
					getScoreinf.ThisTween = DOTween.To (() => getScoreinf.ScoreValue, x => getScoreinf.ScoreValue = x, scoreValue, 1);
					//getScoreinf.ScoreValue = scoreValue;
					getScoreinf.ScoreText.text = scoreValue.ToString ();
				}

				StartCoroutine (updateValue (new WaitForEndOfFrame (), getScoreinf, thisAct));

				break;
			}
		}
	}
	#endregion

	#region Private Methods
	IEnumerator updateValue (WaitForEndOfFrame thisF, ScoreInfo thisInfo, System.Action thisAct = null)
	{
		while (thisInfo.FinalScore != thisInfo.ScoreValue)
		{
			thisInfo.ScoreText.text = thisInfo.ScoreValue.ToString ();
			yield return thisF;
		}

        try {
		    thisAct.Invoke ();
        }
        catch { }
    }
	#endregion

}