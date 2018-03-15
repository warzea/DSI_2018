using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractMedal : MonoBehaviour
{
	#region Variables
	public PlayerController thisPlayer;
	protected Transform thisTrans;
	public float Score;
	public string ThisString;
	public Text ThisText;
	#endregion

	#region Mono
	void Awake ()
	{
		thisTrans = transform;
	}
	#endregion

	#region Public Methods
	public virtual void StartCheck (PlayerController [] allPlayer)
	{

	}

	public void GoTarget (PlayerController equaContr = null, string Text = "")
	{
		if (ThisText != null)
		{
			ThisText.text = ThisString + Score.ToString ();
		}

		if (Score > 1)
		{
			Score = (int) Score;
		}

		Manager.GameCont.MedalInfo [thisPlayer.IdPlayer].ThisMedal.Add (this);
		thisPlayer.NbrAward++;

		if (equaContr != null)
		{
			equaContr.NbrAward++;
			GameObject thisObj = (GameObject) Instantiate (gameObject, thisTrans.parent);
			thisObj.GetComponent<AbstractMedal> ().thisPlayer = equaContr;
			thisObj.GetComponent<AbstractMedal> ().GoTarget ();
		}
	}
	#endregion

	#region Private Methods
	#endregion

}