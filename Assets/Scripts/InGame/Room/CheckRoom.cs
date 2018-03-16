using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

public class CheckRoom : MonoBehaviour
{
    #region Variables
    public float TimerRoom = 50;
    public Door [] AllDoor;
    public Transform [] checkPoint;
    int nbrPlayer = 0;
    bool CauldronInside = false;
    public bool launch = false;

    Text getText;

    private bool isUse = false;
    #endregion

    #region Mono
    #endregion

    #region Public
    void Update ()
    {
        if (launch && getText != null)
        {
            getText.text = TimerRoom.ToString () [0].ToString () + TimerRoom.ToString () [1].ToString () + TimerRoom.ToString () [2].ToString () + TimerRoom.ToString () [3].ToString ();
        }
    }
    #endregion

    #region Private
    void launchRoom ()
    {
        if (!launch && !isUse)
        {
            Manager.Ui.Timer.DOFade (1, 0.1f);
            getText = Manager.Ui.Timer.GetComponent<Text> ();
            getText.text = TimerRoom.ToString ();

            launch = true;
            for (int a = 0; a < AllDoor.Length; a++)
            {
                AllDoor [a].OpenDoor (false);
            }

            DOVirtual.DelayedCall (TimerRoom, () =>
            {
                for (int a = 0; a < AllDoor.Length; a++)
                {
                    AllDoor [a].OpenDoor (true);
                    isUse = true;
                    launch = false;
                }

                getText.gameObject.SetActive (false);
            });
            DOTween.To (() => TimerRoom, x => TimerRoom = x, 0, TimerRoom);
        }
    }

    public bool GetEtatRoom ()
    {
        return launch;
    }

    void OnTriggerEnter (Collider thisColl)
    {
        if (thisColl.tag == Constants._Player)
        {
            nbrPlayer++;

            if (nbrPlayer == Manager.GameCont.NbrPlayer && CauldronInside)
            {
                launchRoom ();
            }
        }
        else if (thisColl.tag == Constants._BoxTag)
        {
            CauldronInside = true;
            if (nbrPlayer == Manager.GameCont.NbrPlayer && CauldronInside)
            {
                launchRoom ();
            }
        }
    }

    void OnTriggerExit (Collider thisColl)
    {
        if (thisColl.tag == Constants._Player)
        {
            nbrPlayer--;
        }
        else if (thisColl.tag == Constants._BoxTag)
        {
            CauldronInside = false;
        }
    }
    #endregion
}