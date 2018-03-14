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
	void Start ()
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
		Debug.Log (thisPlayer.NbrAward);
		if (thisPlayer.NbrAward > 2)
		{
			gameObject.SetActive (false);

			return;
		}

		if (ThisText != null)
		{
			ThisText.text = ThisString;
		}

		Manager.Ui.EndScreenMedals (thisTrans, thisPlayer.IdPlayer, thisPlayer.NbrAward);
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