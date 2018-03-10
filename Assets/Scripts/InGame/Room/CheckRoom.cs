using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CheckRoom : MonoBehaviour
{
    #region Variables
    public float TimerRoom = 50;
    public Door[] AllDoor;
    public Transform[] checkPoint;
    int nbrPlayer = 0;
    bool CauldronInside = false;
    public bool launch = false;
    #endregion

    #region Mono
    #endregion

    #region Public
    #endregion

    #region Private
    void launchRoom()
    {
        if (!launch)
        {
            launch = true;
            for (int a = 0; a < AllDoor.Length; a++)
            {
                AllDoor[a].gameObject.SetActive(true);
            }

            DOVirtual.DelayedCall(TimerRoom, () =>
            {
                for (int a = 0; a < AllDoor.Length; a++)
                {
                    AllDoor[a].gameObject.SetActive(false);
                }
            });
        }
    }

    public bool GetEtatRoom()
    {
        return launch;
    }

    void OnTriggerEnter(Collider thisColl)
    {
        if (thisColl.tag == Constants._Player)
        {
            nbrPlayer++;
            if (nbrPlayer == Manager.GameCont.Players.Count && CauldronInside)
            {
                launchRoom();
            }
        }
        else if (thisColl.tag == Constants._BoxTag)
        {
            CauldronInside = true;
            if (nbrPlayer == Manager.GameCont.Players.Count && CauldronInside)
            {
                launchRoom();
            }
        }
    }

    void OnTriggerExit(Collider thisColl)
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
