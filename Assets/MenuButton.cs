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
        Debug.Log("Select + " + transform.gameObject.name);

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

       

    }



    // Update is called once per frame
    void Update () {
		
	}
}
