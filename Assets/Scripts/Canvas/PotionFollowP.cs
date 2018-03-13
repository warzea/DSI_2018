using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PotionFollowP : MonoBehaviour 
{
	#region Variables
	public int Nbr = -1;
	public float XPos = 1;
	public float YPos = 1;
	public Transform ThisPlayer;
	public Camera getCam;
	Transform thisTrans;
	PlayerController thisPC;
	Text getText;
	bool checkPotion = false;
	CanvasGroup thisCanvas;
	#endregion
	
	
	#region Mono
	void Start ( )
	{
		thisTrans = transform;
		thisPC = ThisPlayer.GetComponent<PlayerController>();
		thisCanvas = gameObject.GetComponent<CanvasGroup>();
		getText = thisTrans.Find("Text").GetComponent<Text>();
	}
	#endregion
	
	
	#region Public
	void Update ()
	{
		thisTrans.position = getCam.WorldToScreenPoint(ThisPlayer.position + Vector3.up * YPos + Vector3.right * XPos );

		if ( thisPC != null )
		{
			getText.text = "+" + thisPC.AllItem.Count.ToString();
			if ( thisPC.AllItem.Count - 1 > 0 && !checkPotion )
			{
				checkPotion = true;
				thisCanvas.DOFade(1,0.5f);
			}
			else if ( thisPC.AllItem.Count - 1< 0 && checkPotion )
			{
				checkPotion = false;
				thisCanvas.DOFade(0,0.5f);
			}
		}
		else
		{
			getText.text = "+" + Nbr.ToString();

		}
	}
	#endregion
	
	
	#region Private

	#endregion
}
