using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

public class UiManager : ManagerParent
{
    #region Variables
    public Transform GetInGame;
    public Scores GetScores;

    public GameObject [] PlayersHUD;
    public Image [] PlayersWeaponHUD;
    public GameObject [] PlayersAmmo;
    public Text [] textWeapon;
    public GameObject PotionGet;

    Tween ammoTwRot, ammoTwScale1, ammoTwScale2, ammoTwFade, ammoTwWait, shakeTwPos, shakeTwRot;

    public Text ScoreText;
    public Text Multiplier;
    public GameObject CauldronGauge;
    public Image GetGauge;
    public Image GaugeBackground;
    public GameObject GaugeButtonBonus;
    public Image [] GaugeFeedback;
    public Image WhiteBackground;

    [Header ("TUTO")]
    public GameObject ButtonsInteract;

    public GameObject [] PlayerText;

    public GameObject PotionsPlus;
    public GameObject PotionsLess;
    public GameObject Light;
    public GameObject Circle;
    public GameObject CircleMultiplier;

    [Header ("SCREENSHAKE")]
    public float ShakeMinPos;
    public float ShakeMaxPos;
    public float ShakeMinRot;
    public float ShakeMaxRot;
    public float ShakeDurationPos;
    public float ShakeDurationRot;

    [Header ("ENDSCREEN")]
    public CanvasGroup [] PlayersEndScreen;
    public CanvasGroup EndScreenContainer;
    public Text TextScoreTotal;
    public GameObject EndScreenFX;
    public GameObject EndScreenWeaponBox;
    public GameObject EndScreenMedal;
    public CanvasGroup EndScreenMenu;
    public Button EndScreenButton;

    [HideInInspector]
    public GameObject [] AllPotGet;

    Dictionary<MenuType, UiParent> AllMenu;
    MenuType menuOpen;
    #endregion

    #region Mono
    #endregion

    #region Public Methods
    public void OpenThisMenu (MenuType thisType, MenuTokenAbstract GetTok = null)
    {
        UiParent thisUi;
        //Debug.Log ( "open " + thisType );

        if (AllMenu.TryGetValue (thisType, out thisUi))
        {
            if (menuOpen == thisType)
            {
                return;
            }

            if (menuOpen != MenuType.Nothing)
            {
                CloseThisMenu ();
            }

            menuOpen = thisType;
            thisUi.OpenThis (GetTok);
        }

        DOVirtual.DelayedCall (22, () =>
        {
            WeaponChangeHUD (3);
        }).SetLoops (-1, LoopType.Restart);

        DOVirtual.DelayedCall (19, () =>
        {
            WeaponChangeHUD (2);
        }).SetLoops (-1, LoopType.Restart);

        DOVirtual.DelayedCall (17, () =>
        {
            WeaponChangeHUD (1);
        }).SetLoops (-1, LoopType.Restart);

        DOVirtual.DelayedCall (15, () =>
        {
            WeaponChangeHUD (0);
        }).SetLoops (-1, LoopType.Restart);

    }

    public void CloseThisMenu ()
    {
        UiParent thisUi;

        if (menuOpen != MenuType.Nothing && AllMenu.TryGetValue (menuOpen, out thisUi))
        {
            thisUi.CloseThis ();
            menuOpen = MenuType.Nothing;
        }
    }

    public void WeaponEmpty (int PlayerId)
    {
        PlayersAmmo [PlayerId].GetComponentsInChildren<RainbowColor> () [0].enabled = true;
        PlayersAmmo [PlayerId].GetComponentInChildren<RainbowScale> ().enabled = true;
    }

    public void ResetTween ()
    {
        ammoTwFade.Kill (true);
        ammoTwRot.Kill (true);
        ammoTwScale1.Kill (true);
        ammoTwScale2.Kill (true);
        ammoTwWait.Kill (true);
    }

    // 1
    public void EndScreenStart ()
    {
        EndScreenWeaponBox.SetActive (true);
        GetInGame.transform.DOLocalMoveY (200, .3f).SetEase (Ease.InOutSine);

        EndScreenContainer.DOFade (1, .2f);

        UnityEngine.Debug.Log ("EndScree");

        for (int i = 0; i < 4; i++)
        {
            PlayersEndScreen [i].transform.DOLocalMoveY (650, 0);

            DOVirtual.DelayedCall (.24f, () =>
            {
                PlayersEndScreen [0].DOFade (1, .25f);
                PlayersEndScreen [0].transform.DOLocalMoveY (330, .25f).SetEase (Ease.InOutSine);
            });

            DOVirtual.DelayedCall (.5f, () =>
            {
                PlayersEndScreen [1].DOFade (1, .25f);
                PlayersEndScreen [1].transform.DOLocalMoveY (68, .25f).SetEase (Ease.InOutSine);
            });

            DOVirtual.DelayedCall (.75f, () =>
            {
                PlayersEndScreen [2].DOFade (1, .25f);
                PlayersEndScreen [2].transform.DOLocalMoveY (-180, .25f).SetEase (Ease.InOutSine);
            });

            DOVirtual.DelayedCall (1f, () =>
            {
                PlayersEndScreen [3].DOFade (1, .25f);
                PlayersEndScreen [3].transform.DOLocalMoveY (-440, .25f).SetEase (Ease.InOutSine);
            });

            DOVirtual.DelayedCall (1.5f, () =>
            {

                TextScoreTotal.transform.parent.DOScale (4, 0);
                TextScoreTotal.transform.parent.DOScale (1, .25f);
                TextScoreTotal.transform.parent.GetComponent<CanvasGroup> ().DOFade (1, .25f);

                EndScreenWeaponBox.transform.DOLocalMoveX (3300, 0);
                EndScreenWeaponBox.transform.DOLocalMoveX (660, 1).SetEase (Ease.InBounce);
                EndScreenWeaponBox.transform.DOShakeScale (1f, .4f, 18, 0);
            });

        }

    }

    // 2
    public void EndScreenMedals (Transform thisObj, int thisID)
    {
        //thisObj = PlayersEndScreen [thisID].transform.GetChild (3).transform;
        //thisObj.localPosition += new Vector3 (0, 50, 0);
    }

    // 3
    public void EndScreenFinished ()
    {
        EndScreenFX.gameObject.SetActive (true);

        DOVirtual.DelayedCall (1f, () =>
        {
            EndScreenMenu.DOFade (1, .3f).OnComplete (() =>
            {

                EndScreenButton.Select ();
            });
        });

    }

    public void EndScreenAll ()
    {
        foreach (Transform trans in EndScreenContainer.transform)
        {
            if (trans.GetComponent<CanvasGroup> ())
            {
                trans.GetComponent<CanvasGroup> ().DOFade (1, .1f);
            }
        }
    }

    public void WeaponChangeIG (int PlayerId)
    {
        ResetTween ();

        WeaponChangeHUD (PlayerId);

        //HUD INGAME
        Transform getTrans = PlayersAmmo [PlayerId].transform;
        getTrans.GetComponentsInChildren<RainbowColor> () [0].enabled = false;
        getTrans.GetComponentInChildren<RainbowScale> ().enabled = false;
        getTrans.localScale = Vector3.one;

        float randomZrotate = UnityEngine.Random.Range (-17, 17);
        ammoTwRot = getTrans.DOPunchRotation (new Vector3 (1, 1, randomZrotate), 0.6f, 10, 1);

        //LE CHARGEUR DISPARAIT

        ammoTwScale1 = getTrans.DOPunchScale ((Vector3.one * .45f), 0.3f, 20, .1f);
        ammoTwFade = getTrans.GetComponent<CanvasGroup> ().DOFade (0, .3f);
        ammoTwScale2 = getTrans.DOScale (0, .3f);
    }

    public void WeaponChangeHUD (int PlayerId)
    {
        //HUD ABOVE ALL

        ResetTween ();

        Transform getTrans = PlayersWeaponHUD [PlayerId].transform;

        getTrans.GetChild (0).GetComponent<Image> ().DOFade (1, .25f).OnComplete (() =>
        {
            getTrans.GetChild (0).GetComponent<Image> ().DOFade (0, .25f);
        });

        var circle = Instantiate (Circle, getTrans.parent.position, Quaternion.identity, getTrans.parent.GetChild (0));
        getTrans.DOLocalRotate (new Vector3 (0, 0, 360), .5f, RotateMode.LocalAxisAdd).OnComplete (() =>
        {
            var light = Instantiate (Light, getTrans.position, Quaternion.identity, getTrans);
        });

    }

    public void WeaponNew (int PlayerId)
    {
        //LE CHARGEUR REAPARAIT VISIBLE
        Transform getTrans = PlayersAmmo [PlayerId].transform;
        ammoTwFade = getTrans.GetComponent<CanvasGroup> ().DOFade (1, .2f);
        getTrans.localScale = new Vector3 (3, 3, 3);
        ammoTwScale1 = getTrans.DOScale (1, .2f);

        //COURT EFFET LUMINEUX DE COULEUR SUR LE OUTLINE
        getTrans.Find ("Ammo Inside").GetComponent<Image> ().color = Color.white;
        getTrans.GetComponentsInChildren<RainbowColor> () [1].enabled = true;
        ammoTwWait = DOVirtual.DelayedCall (.3f, () =>
        {
            getTrans.GetComponentsInChildren<RainbowColor> () [1].enabled = false;
            getTrans.GetComponentsInChildren<RainbowColor> () [1].transform.GetComponent<Image> ().color = getTrans.GetComponentsInChildren<RainbowColor> () [1].colors [1];
        });
    }

    public void PotionsLost ()
    {

    }

    public void GaugeLevelGet (int whichLevel)
    {
        GaugeFeedback [whichLevel].transform.DOScale (4, 0);
        GaugeFeedback [whichLevel].transform.DOScale (1, .4f);
        GaugeFeedback [whichLevel].DOFade (1, .1f);
        DOVirtual.DelayedCall (.3f, () =>
        {

            GaugeFeedback [whichLevel].DOFade (0, .4f);
        });
    }

    int nbrCauld = 0;
    [HideInInspector]
    public bool checkDrive = false;

    public void CauldronButtonBonus (bool visible = false)
    {
        if (!visible)
        {
            nbrCauld--;
            if (nbrCauld <= 0 && !checkDrive)
            {
                GaugeButtonBonus.GetComponent<CanvasGroup> ().DOFade (0, .1f);
            }
        }
        else
        {
            nbrCauld++;
            GaugeButtonBonus.GetComponent<CanvasGroup> ().DOFade (1, .1f);
        }
    }

    public bool OnTuto = true;

    public void CauldronButtons (bool visible = false)
    {
        if (OnTuto)
        {
            if (!visible)
            {
                nbrCauld--;
                ButtonsInteract.GetComponent<CanvasGroup> ().DOFade (0, .1f);
            }
            else
            {
                nbrCauld++;
                ButtonsInteract.GetComponent<CanvasGroup> ().DOFade (1, .1f);
            }
        }
        else
        {
            CauldronButtonBonus (visible);
        }
    }

    public void PopPotions (PotionType type) // poser ressource : 20 - 40 - 60 - 80 et 100
    {
        if (type == PotionType.Plus)
        {
            var potion = Instantiate (PotionsPlus, GetInGame.position, Quaternion.identity, GetInGame);

            ScorePlus ();

            //potion.GetComponent<RainbowMove>().ObjectTransform = ici;
        }
        else if (type == PotionType.Less)
        {
            var potion = Instantiate (PotionsLess, GetInGame.position, Quaternion.identity, GetInGame);

            //potion.transform.DOLocalMove(Manager.GameCont.WeaponB.transform.position, 0);
            //potion.transform.DOLocalMoveY(Manager.GameCont.WeaponB.transform.localPosition.y + 220, 0);

            ScoreLess ();
            //potion.GetComponent<RainbowMove>().ObjectTransform = ici;
        }
    }

    public void MultiplierNew (int value = 0)
    {

        //WHITE FLASH

        WhiteBackground.DOFade (.7f, .2f).OnComplete (() =>
        {
            WhiteBackground.DOFade (0, .2f);
        });

        //GaugeLevelGet(0); GaugeLevelGet(1); GaugeLevelGet(2); GaugeLevelGet(3); GaugeLevelGet(4);

        GaugeBackground.GetComponent<RainbowMove> ().enabled = true;
        GetGauge.GetComponent<RainbowMove> ().enabled = true;
        GetGauge.GetComponentInChildren<RainbowColor> ().enabled = false;
        GetGauge.GetComponentInChildren<RainbowColor> ().transform.GetComponent<Image> ().DOColor (new Color (1, 1, 1, 0), 0);

        DOVirtual.DelayedCall (.8f, () =>
        {
            GaugeBackground.GetComponent<RainbowMove> ().enabled = false;
            GetGauge.GetComponent<RainbowMove> ().enabled = false;

            Multiplier.GetComponent<RainbowColor> ().enabled = false;
            Multiplier.GetComponent<RainbowMove> ().enabled = false;
            Multiplier.GetComponent<RainbowScale> ().enabled = false;
        });

        Multiplier.GetComponent<Text> ().text = "x" + value.ToString ();
        Multiplier.GetComponent<RainbowColor> ().enabled = true;
        Multiplier.GetComponent<RainbowMove> ().enabled = true;
        Multiplier.GetComponent<RainbowScale> ().enabled = true;

        var circle = Instantiate (CircleMultiplier, Multiplier.transform.position, Quaternion.identity, Multiplier.transform.parent);
        circle.transform.SetSiblingIndex (0);

    }

    #endregion

    #region Private Methods

    public void ScreenShake ()
    {
        shakeTwPos.Kill (true);
        shakeTwRot.Kill (true);

        float rdmX = UnityEngine.Random.Range (ShakeMinPos, ShakeMaxPos);
        float rdmZ = UnityEngine.Random.Range (ShakeMinRot, ShakeMaxRot);

        Transform getT = Manager.GameCont.MainCam.transform;

        shakeTwPos = getT.transform.DOPunchPosition (new Vector3 (rdmX, 0, rdmX), ShakeDurationPos, 2, 1);
        shakeTwRot = getT.transform.DOPunchRotation (new Vector3 (0, 0, rdmZ), ShakeDurationRot, 2, 1);

        //getT.DOPunchPosition(getT.localPosition + getT.right * rdmX + getT.up * rdmZ, .5f, 0, 0);
        //UnityEngine.Debug.Log(Camera.main.transform.localPosition);

        //Manager.GameCont.MainCam.transform.DOMove(Manager.GameCont.MainCam.transform.localPosition + new Vector3(rdmX, 0, rdmZ), .12f, 0, 0);

        //Manager.GameCont.MainCam.transform.DOPunchPosition(Manager.GameCont.MainCam.transform.localPosition + new Vector3(rdmX, 0, rdmZ), .2f, 0, 0);
    }

    void ScorePlus ()
    {
        ScoreText.transform.DOKill (true);
        ScoreText.transform.DOShakeScale (.4f, 1f, 12, 15);
        ScoreText.GetComponent<RainbowColor> ().enabled = true;
        DOVirtual.DelayedCall (.4f, () =>
        {
            ScoreText.GetComponent<RainbowColor> ().enabled = false;
        });
    }

    void ScoreLess ()
    {
        ScoreText.transform.DOKill (true);
        ScoreText.transform.DOShakeScale (.4f, 1f, 12, 15);
        /*  ScoreText.GetComponentsInChildren<RainbowColor>()[1].enabled = true;
          DOVirtual.DelayedCall(.4f, () => {
              ScoreText.GetComponentsInChildren<RainbowColor>()[1].enabled = false;
          });*/
    }

    private void Update ()
    {

#if UNITY_EDITOR

        if (Input.GetKeyDown (KeyCode.O))
        {
            PopPotions (PotionType.Plus);
            WeaponEmpty (0);
        }
        if (Input.GetKeyDown (KeyCode.P))
        {
            WeaponChangeIG (0);
        }
        if (Input.GetKeyDown (KeyCode.I))
        {
            ScorePlus ();
        }
        if (Input.GetKeyDown (KeyCode.U))
        {
            MultiplierNew ();
            GaugeLevelGet (2);
        }
        if (Input.GetKeyDown (KeyCode.Y))
        {
            PopPotions (PotionType.Less);
        }
        if (Input.GetKeyDown (KeyCode.T))
        {
            EndScreenStart ();
        }
        if (Input.GetKeyDown (KeyCode.L))
        {
            EndScreenFinished ();
        }
        if (Input.GetKeyDown (KeyCode.M))
        {
            EndScreenAll ();
        }

#endif

    }

    protected override void InitializeManager ()
    {
        Object [] getAllMenu = Resources.LoadAll ("Menu");
        Dictionary<MenuType, UiParent> setAllMenu = new Dictionary<MenuType, UiParent> (getAllMenu.Length);

        Transform Parent = transform.GetChild (0);;
        GameObject thisMenu;
        UiParent thisUi;

        for (int a = 0; a < getAllMenu.Length; a++)
        {
            thisMenu = (GameObject) Instantiate (getAllMenu [a], Parent);
            thisUi = thisMenu.GetComponent<UiParent> ();
            thisUi.Initialize ();
            thisMenu.SetActive (false);

            setAllMenu.Add (thisUi.ThisMenu, thisUi);
        }

        EndScreenContainer.transform.parent.GetComponent<Canvas> ().worldCamera = Manager.GameCont.MainCam;
        GaugeButtonBonus = (GameObject) Instantiate (GaugeButtonBonus, GetInGame);

        CauldronGauge = (GameObject) Instantiate (CauldronGauge, GetInGame);
        CauldronGauge.GetComponent<CanvasGroup> ().DOFade (0, 0);

        ButtonsInteract = (GameObject) Instantiate (ButtonsInteract, GetInGame);
        AllMenu = setAllMenu;

        OpenThisMenu (MenuType.SelectPlayer);
    }

    #endregion
}