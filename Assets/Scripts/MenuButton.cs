using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using DG.Tweening;
using UnityEngine.UI;

public class MenuButton : Button {

    public CanvasGroup canvasMenu { get; set; }

    // Use this for initialization
    void Start () {
		
	}

    public override void OnSelect(UnityEngine.EventSystems.BaseEventData eventData)
    {
        //Debug.Log("Select + " + transform.gameObject.name);

        GetComponentsInChildren<RainbowColor>()[0].enabled = true;
        GetComponentsInChildren<RainbowMove>()[0].enabled = true;
    }

    public override void OnDeselect(UnityEngine.EventSystems.BaseEventData eventData)
    {
        Debug.Log("Deselect + " + transform.gameObject.name);

        GetComponentsInChildren<RainbowColor>()[0].enabled = false;
        GetComponentsInChildren<RainbowMove>()[0].enabled = false;
    }

    public override void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData)
    {
        if(gameObject.name == "Play")
        {
            MenuManager.Singleton.PlayReady();

            GameObject myEventSystem = GameObject.Find("Rewired Event System");
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }

        if (gameObject.name == "Restart")
        {
            Debug.Log("Restart");

            GameObject myEventSystem = GameObject.Find("Rewired Event System");
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);

            Manager.Ui.EndScreenTransition.DOFade(1, .5f).OnComplete(() => {
                DOVirtual.DelayedCall(1.5f, () => {

                    StartCoroutine(MenuManager.Singleton.LoadLevel(false));
                });
            });

        }


        if (gameObject.name == "Back To Menu")
        {
            Debug.Log("Menu");

            GameObject myEventSystem = GameObject.Find("Rewired Event System");
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);

            Manager.Ui.EndScreenTransition.DOFade(1, .5f).OnComplete(() => {
                DOVirtual.DelayedCall(1.5f, () => {

                    StartCoroutine(MenuManager.Singleton.LoadMenu());
                });
            });

        }

        if (gameObject.name == "Credits" && Credits.Singleton.canPressCredits)
        {

            Credits.Singleton.canPressCredits = false;
            MenuManager.Singleton.canvasMenu.transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(0, .2f);
            MenuManager.Singleton.canvasMenu.transform.GetChild(1).GetComponent<CanvasGroup>().DOFade(0, .2f);
            MenuManager.Singleton.canvasMenu.transform.GetChild(2).GetComponent<CanvasGroup>().DOFade(0, .2f);
            MenuManager.Singleton.canvasMenu.transform.GetChild(3).GetComponent<CanvasGroup>().DOFade(0, .2f);

            MenuManager.Singleton.Credits.DOFade(1, .35f).OnComplete(() => {

                Credits.Singleton.canPressCredits = true;
            });


            Credits.Singleton.opened = true;

            GameObject myEventSystem = GameObject.Find("Rewired Event System");
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }

        if (gameObject.name == "Quit")
        {
            if (!Application.isEditor)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }



    }



    // Update is called once per frame
    void Update () {
		
	}
}
