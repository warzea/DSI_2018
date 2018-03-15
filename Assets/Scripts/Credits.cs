using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Rewired;

public class Credits : MonoBehaviour {

    public bool opened, canPressCredits = true;

    public Player player1;

    public static Credits Singleton;
	// Use this for initialization
	void Start () {
		if (Singleton == null)
        {
            Singleton = this;
        } else
        {
            Destroy(this);
        }


        player1 = ReInput.players.GetPlayer(0);
    }
	
	// Update is called once per frame
	void Update () {
        if (player1.GetButtonDown("UISubmit"))
        {
            if (opened && canPressCredits)
            {
                canPressCredits = false;

                MenuManager.Singleton.canvasMenu.transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(1, .35f);
                MenuManager.Singleton.canvasMenu.transform.GetChild(1).GetComponent<CanvasGroup>().DOFade(1, .35f);
                MenuManager.Singleton.canvasMenu.transform.GetChild(2).GetComponent<CanvasGroup>().DOFade(1, .35f);
                MenuManager.Singleton.canvasMenu.transform.GetChild(3).GetComponent<CanvasGroup>().DOFade(1, .35f);

                MenuManager.Singleton.Credits.DOFade(0, .35f).OnComplete(() => {
                    canPressCredits = true;
                    MenuManager.Singleton.creditsItem.Select();
                });
            }
        }
	}
}
