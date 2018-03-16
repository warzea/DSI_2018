
    bool checkGold = false;

    void Start ()


        animChest = transform.GetComponentInChildren<Animator> ();
                    checkGold = true;

                    foreach (Renderer thisMat in GetComponentsInChildren<Renderer> ())
            if(checkGold)
                Manager.VibM.ShootMediumVibration(thisPlayer.inputPlayer);
            else
                Manager.VibM.ShootLowVibration(thisPlayer.inputPlayer);

            thisTrans.DOKill (true);
    bool checkGold = false;

    void Start ()


        animChest = transform.GetComponentInChildren<Animator> ();
                    checkGold = true;

                    foreach (Renderer thisMat in GetComponentsInChildren<Renderer> ())
            if(checkGold)
                Manager.VibM.ShootMediumVibration(thisPlayer.inputPlayer);
            else
                Manager.VibM.ShootLowVibration(thisPlayer.inputPlayer);

            thisTrans.DOKill (true);