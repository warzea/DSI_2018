using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

using System.Diagnostics;
using System.Runtime.CompilerServices;

public class UiManager : ManagerParent
{
	#region Variables
	public Transform GetInGame;
	public Scores GetScores;

    public GameObject[] PlayersHUD;
    public Image[] PlayersWeaponHUD;
    public GameObject[] PlayersAmmo;

    Tween ammoTwRot, ammoTwScale1, ammoTwScale2, ammoTwFade, ammoTwWait;

    public Text ScoreText;
    public Text Multiplier;
    public Image GetGauge;
    public Image[] GaugeFeedback;

    public GameObject[] PlayerText;

    public GameObject PotionsPlus;
    public GameObject PotionsLess;
    public GameObject Light;
    public GameObject Circle;
    public GameObject CircleMultiplier;


    Dictionary <MenuType, UiParent> AllMenu;
	MenuType menuOpen;
	#endregion

	#region Mono
	#endregion

	#region Public Methods
	public void OpenThisMenu ( MenuType thisType, MenuTokenAbstract GetTok = null )
	{
		UiParent thisUi;
		//Debug.Log ( "open " + thisType );

		if ( AllMenu.TryGetValue ( thisType, out thisUi ) )
		{
			if ( menuOpen == thisType )
			{
				return;
			}
            
			if ( menuOpen != MenuType.Nothing )
			{
				CloseThisMenu ( );
			}

			menuOpen = thisType;
			thisUi.OpenThis ( GetTok );
		}
    }

	public void CloseThisMenu ( )
	{
		UiParent thisUi;

		if ( menuOpen != MenuType.Nothing && AllMenu.TryGetValue ( menuOpen, out thisUi ) )
		{
			thisUi.CloseThis ( );
			menuOpen = MenuType.Nothing;
		}
	}

    public void WeaponEmpty(int PlayerId)
    {
        PlayersAmmo[PlayerId].GetComponentsInChildren<RainbowColor>()[0].enabled = true;
        PlayersAmmo[PlayerId].GetComponentInChildren<RainbowScale>().enabled = true;
    }

    public void ResetTween()
    {
        ammoTwFade.Kill(true);
        ammoTwRot.Kill(true);
        ammoTwScale1.Kill(true);
        ammoTwScale2.Kill(true);
        ammoTwWait.Kill(true);
    }

    public void WeaponChange(int PlayerId)
    {
        ResetTween();

        //HUD INGAME

        PlayersAmmo[PlayerId].GetComponentsInChildren<RainbowColor>()[0].enabled = false;
        PlayersAmmo[PlayerId].GetComponentInChildren<RainbowScale>().enabled = false;

        float randomZrotate = UnityEngine.Random.Range(-17, 17);
        ammoTwRot = PlayersAmmo[PlayerId].transform.DOPunchRotation(new Vector3(1, 1, randomZrotate), 0.6f, 10, 1);

        //LE CHARGEUR DISPARAIT

        ammoTwScale1 = PlayersAmmo[PlayerId].transform.DOPunchScale((Vector3.one * .45f), 0.3f, 20, .1f);
        ammoTwFade = PlayersAmmo[PlayerId].transform.GetComponent<CanvasGroup>().DOFade(0, .3f);
        ammoTwScale2 = PlayersAmmo[PlayerId].transform.DOScale(0, .3f);
        
        //HUD ABOVE ALL
        
        PlayersWeaponHUD[PlayerId].transform.DOKill(true);
        PlayersWeaponHUD[PlayerId].transform.GetChild(0).GetComponent<Image>().DOFade(1, .25f).OnComplete(()=> {
            PlayersWeaponHUD[PlayerId].transform.GetChild(0).GetComponent<Image>().DOFade(0, .25f);
        });

        var circle = Instantiate(Circle, PlayersWeaponHUD[PlayerId].transform.parent.position, Quaternion.identity, PlayersWeaponHUD[PlayerId].transform.parent.GetChild(0));
        PlayersWeaponHUD[PlayerId].transform.DOLocalRotate(new Vector3(0, 0, 360), .5f, RotateMode.LocalAxisAdd).OnComplete(() => {

            var light = Instantiate(Light, PlayersWeaponHUD[PlayerId].transform.position, Quaternion.identity, PlayersWeaponHUD[PlayerId].transform);
        });


    }

    public void WeaponNew(int PlayerId)
    {
        //LE CHARGEUR REAPARAIT VISIBLE

        ammoTwFade = PlayersAmmo[PlayerId].transform.GetComponent<CanvasGroup>().DOFade(1, .2f);
        PlayersAmmo[PlayerId].transform.DOScale(3, 0);
        ammoTwScale1 = PlayersAmmo[PlayerId].transform.DOScale(1, .2f);


        //COURT EFFET LUMINEUX DE COULEUR SUR LE OUTLINE

        PlayersAmmo[PlayerId].GetComponentsInChildren<RainbowColor>()[1].enabled = true;
        ammoTwWait = DOVirtual.DelayedCall(.3f, () =>
        {
            PlayersAmmo[PlayerId].GetComponentsInChildren<RainbowColor>()[1].enabled = false;
            PlayersAmmo[PlayerId].GetComponentsInChildren<RainbowColor>()[1].transform.GetComponent<Image>().color = PlayersAmmo[PlayerId].GetComponentsInChildren<RainbowColor>()[1].colors[1];
        });
    }


    public void GaugeLevelGet(int whichLevel)
    {
        GaugeFeedback[whichLevel].transform.DOScale(4, 0);
        GaugeFeedback[whichLevel].transform.DOScale(1, .4f);
        GaugeFeedback[whichLevel].DOFade(1, .1f);
        DOVirtual.DelayedCall(.3f, () => {
        
            GaugeFeedback[whichLevel].DOFade(0, .4f);
        });
    }


    public void PopPotions(PotionType type) // poser ressource : 20 - 40 - 60 - 80 et 100
    {
        if(type == PotionType.Plus)
        {
            var potion = Instantiate(PotionsPlus, GetInGame.position, Quaternion.identity, GetInGame);

            ScorePlus();

            //potion.GetComponent<RainbowMove>().ObjectTransform = ici;
        }
    }

    public void MultiplierNew( int value = 0 )
    {
        Multiplier.GetComponent<Text>().text = "x"+ value.ToString();
        Multiplier.GetComponent<RainbowColor>().enabled = true;
        Multiplier.GetComponent<RainbowMove>().enabled = true;
        Multiplier.GetComponent<RainbowScale>().enabled = true;

                var circle = Instantiate(CircleMultiplier, Multiplier.transform.position, Quaternion.identity, Multiplier.transform.parent);
        circle.transform.SetSiblingIndex(0);

        DOVirtual.DelayedCall(.8f, () => {

            Multiplier.GetComponent<RainbowColor>().enabled = false;
            Multiplier.GetComponent<RainbowMove>().enabled = false;
            Multiplier.GetComponent<RainbowScale>().enabled = false;

        });
    }

    #endregion

    #region Private Methods
    void ScorePlus()
    {
        ScoreText.transform.DOKill(true);
        ScoreText.transform.DOShakeScale(.4f, 1f, 12, 15);
        ScoreText.GetComponent<RainbowColor>().enabled = true;
        DOVirtual.DelayedCall(.4f, () => {
            ScoreText.GetComponent<RainbowColor>().enabled = false;
        });
    }
    private void Update()
    {

#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.O))
        {
            PopPotions(PotionType.Plus);
            WeaponEmpty(0);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            WeaponChange(0);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ScorePlus();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            MultiplierNew();
            GaugeLevelGet(2);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GaugeLevelGet(0);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
        }

#endif

    }

    private void FixedUpdate()
    {
        //PlayersAmmo[0].transform.position = Manager.GameCont.Players[0].transform.position;
    }

    protected override void InitializeManager ( )
	{
		Object[] getAllMenu = Resources.LoadAll ( "Menu" );
		Dictionary<MenuType, UiParent> setAllMenu = new Dictionary<MenuType, UiParent> ( getAllMenu.Length );

		Transform Parent = transform.GetChild(0);;
		GameObject thisMenu;
		UiParent thisUi;

		for ( int a = 0; a < getAllMenu.Length; a++ )
		{
			thisMenu = ( GameObject ) Instantiate ( getAllMenu [ a ], Parent );
			thisUi = thisMenu.GetComponent<UiParent> ( );
			thisUi.Initialize ( );
			thisMenu.SetActive(false);
			
			setAllMenu.Add ( thisUi.ThisMenu, thisUi );
		}

		AllMenu = setAllMenu;

		OpenThisMenu(MenuType.SelectPlayer);
	}

	#endregion
}
