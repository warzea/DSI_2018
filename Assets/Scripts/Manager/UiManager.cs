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
    public Image[] PlayersWeapon;

    public Text ScoreText;

    public GameObject PotionsPlus;
    public GameObject PotionsLess;
    public GameObject Light;
    public GameObject Circle;

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

    public void ScorePlus()
    {
        ScoreText.transform.DOKill(true);
        ScoreText.transform.DOShakeScale(.4f, 1f, 12, 15);
        ScoreText.GetComponent<RainbowColor>().enabled = true;
        DOVirtual.DelayedCall(.4f, () => {
            ScoreText.GetComponent<RainbowColor>().enabled = false;
        });
    }

    public void ChangeWeapons(int PlayerId)
    {
        PlayersWeapon[PlayerId].transform.DOKill(true);
        PlayersWeapon[PlayerId].transform.GetChild(0).GetComponent<Image>().DOFade(1, .25f).OnComplete(()=> {
            PlayersWeapon[PlayerId].transform.GetChild(0).GetComponent<Image>().DOFade(0, .25f);
        });

        var circle = Instantiate(Circle, PlayersWeapon[PlayerId].transform.parent.position, Quaternion.identity, PlayersWeapon[PlayerId].transform.parent);
        PlayersWeapon[PlayerId].transform.DOLocalRotate(new Vector3(0, 0, 360), .5f, RotateMode.LocalAxisAdd).OnComplete(() => {

            var light = Instantiate(Light, PlayersWeapon[PlayerId].transform.position, Quaternion.identity, PlayersWeapon[PlayerId].transform);



        });


    }

    public void PopPotions(string type)
    {
        if(type == "Plus")
        {
            var potion = Instantiate(PotionsPlus, GetInGame.position, Quaternion.identity, GetInGame);
            //potion.GetComponent<RainbowMove>().ObjectTransform = ici;
        }
    }

    #endregion

    #region Private Methods
    private void Update()
    {

#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.O))
        {
            PopPotions("Plus");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeWeapons(0);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ScorePlus();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            ScorePlus();
        }

#endif

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
