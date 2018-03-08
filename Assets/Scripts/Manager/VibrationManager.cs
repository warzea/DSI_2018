using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Rewired;
using DG.Tweening;


public class VibrationManager : ManagerParent
{
    [Range(0, 3)]
    public float ObsPower = .12f, ObsDuration = .2f, ShootLowPower = .15f, ShootLowDuration = .25f, ShootMediumPower = .5f, ShootMediumDuration = .5f, ShootHighPower = .5f, ShootHighDuration = .5f, StunPower = 2.5f, StunDuration = 1f;

    protected override void InitializeManager ( )
	{
    }

    public void StunVibration( Player thisPlayer )
    {
        foreach (Joystick j in thisPlayer.controllers.Joysticks)
        {
            j.StopVibration();
            j.SetVibration(0, StunPower, true);
            DOVirtual.DelayedCall(StunDuration, () =>
            {
                j.StopVibration();
            });
        }
    }

    public void ShootLowVibration( Player thisPlayer )
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

    public void ShootMediumVibration( Player thisPlayer )
    {

        foreach (Joystick j in thisPlayer.controllers.Joysticks)
        {
            j.SetVibration(1, ShootMediumPower,false);
            DOVirtual.DelayedCall(ShootMediumDuration, () =>
            {
                j.StopVibration();
            });
        }
    }

    public void ShootHighVibration( Player thisPlayer )
    {

        foreach (Joystick j in thisPlayer.controllers.Joysticks)
        {
            j.SetVibration(1, ShootHighPower, false);
            DOVirtual.DelayedCall(ShootHighDuration, () =>
            {
                j.StopVibration();
            });
        }
    }
}
