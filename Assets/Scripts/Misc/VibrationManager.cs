using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Rewired;
using DG.Tweening;


public class VibrationManager : MonoBehaviour
{
    public static VibrationManager Singleton;

    public int playerID;

    Player player;

    [Range(0, 3)]
    public float ObsPower = .12f, ObsDuration = .2f, ShootLowPower = .15f, ShootLowDuration = .25f, ShootMediumPower = .5f, ShootMediumDuration = .5f, ShootHighPower = .5f, ShootHighDuration = .5f, StunPower = 2.5f, StunDuration = 1f;

    void Awake()
    {
        if (VibrationManager.Singleton == null)
        {
            VibrationManager.Singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        player = ReInput.players.GetPlayer(0);
    }

    public void StunVibration()
    {
        foreach (Joystick j in player.controllers.Joysticks)
        {
            j.StopVibration();
            j.SetVibration(0, StunPower, true);
            DOVirtual.DelayedCall(StunDuration, () =>
            {
                j.StopVibration();
            });
        }
        
    }


    public void ShootLowVibration()
    {

        foreach (Joystick j in player.controllers.Joysticks)
            {
                j.SetVibration(0, ShootLowPower,false);
                DOVirtual.DelayedCall(ShootLowDuration, () =>
                {
                   j.StopVibration();
              });
          }
    }


    public void ShootMediumVibration()
    {

        foreach (Joystick j in player.controllers.Joysticks)
        {
            j.SetVibration(1, ShootMediumPower,false);
            DOVirtual.DelayedCall(ShootMediumDuration, () =>
            {
                j.StopVibration();
            });
        }
    }

    public void ShootHighVibration()
    {

        foreach (Joystick j in player.controllers.Joysticks)
        {
            j.SetVibration(1, ShootHighPower, false);
            DOVirtual.DelayedCall(ShootHighDuration, () =>
            {
                j.StopVibration();
            });
        }
    }



   
}
