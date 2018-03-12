﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WeaponBox : MonoBehaviour 
{
	#region Variables
	public Slider ThisGauge;
	public GameObject AttackZone;
	public float DelayAttack = 1;
	public float DelayStay = 1;
	public int SpeMultRessources = 2;
	public int StayMult = 5;
	public float InvincibleTime = 1;
	Transform GetTrans;
	public int pourcLoot = 10;
	public int MinLost = 10;
	public GameObject[] AllWeapon;
	public float DelayNewWeapon = 1;
	[Tooltip("Time to full fill")]
	public float TimeFullFill = 6;
	[HideInInspector]
	public float CurrTime = 0;

	[HideInInspector]
	public int NbrItem = 0;

	[HideInInspector]
	public bool CanControl = true;

	List<PlayerWeapon> updateWeapon; 
	List<Tween> getAllTween;
	Transform getChild;
	int nbrTotalSlide = 1;
	bool invc = false;
	bool checkAttack = false;
	#endregion
	
	#region Mono
	void Awake ( )
	{
		getAllTween = new List<Tween>();
		updateWeapon = new List<PlayerWeapon>();
		GetTrans = transform;
		getChild = GetTrans.Find("Inside");
		for ( int a = 0; a < 4; a ++)
		{
			updateWeapon.Add ( new PlayerWeapon () );
			updateWeapon[a].IDPlayer = a;
		}

		if ( DelayStay < DelayAttack )
		{
			DelayStay = DelayAttack;
		}
	}
	#endregion
	
	#region Public Methods
	public void AttackCauld ( )
	{
		if ( !checkAttack )
		{
			checkAttack = true;
			AttackZone.SetActive(true);

			DOVirtual.DelayedCall(DelayAttack, ( ) => 
			{
				checkAttack = false;
			});

			DOVirtual.DelayedCall(DelayStay, ( ) => 
			{
				AttackZone.SetActive(false);
			});
		}
	}

	public void ActionSpe ( )
	{
		if ( CurrTime >= TimeFullFill )
		{
			CurrTime = 0;

			var newMult = new ChestEvent ( );
			newMult.Mult = SpeMultRessources;
			newMult.TimeMult = StayMult;
			newMult.Raise ( );
		}
	}

    public void GetWeapon (PlayerController thisPlayer, GameObject newObj = null)
    {
        getChild.DOKill(true);
        getChild.DORotate(new Vector3(0,0,1080), 2, RotateMode.LocalAxisAdd).SetEase(Ease.InSine);
        getChild.DOLocalMoveY(3, 2).SetEase(Ease.InSine).OnComplete(()=> {
            getChild.DOShakeScale(.5f, .3f, 18, 0);
            getChild.DOLocalMoveY(0.5f, .6f).SetEase(Ease.InElastic).OnComplete(()=> {

                getChild.DOShakeScale(1f, .4f, 18, 0);
            });
        });

    }


    public void NewWeapon ( PlayerController thisPlayer, GameObject newObj = null )
	{

        //TRANSFO CANON

		getChild.DOKill(true);
        getChild.DOShakeScale(.15f, .8f, 25, 0).OnComplete(() => { 

            getChild.DOScaleZ(1.75f, .1f).SetEase(Ease.Linear).OnComplete(() => {

                getChild.DOScaleZ(3.5f, .1f).SetEase(Ease.Linear);

                DOVirtual.DelayedCall(.1f, () => {

                    getChild.DOScaleZ(1.3f, .1f).SetEase(Ease.OutSine).OnComplete(()=> {

                        getChild.DOShakeScale(.2f, .3f, 18, 0);
                    });
                });
            });
        });

        Manager.Ui.WeaponChange(thisPlayer.IdPlayer);

		if ( newObj == null )
		{
			newObj = (GameObject) Instantiate ( AllWeapon[Random.Range(0, AllWeapon.Length)], GetTrans );
		}
		else
		{
			List<GameObject> getWeap = new List<GameObject>(AllWeapon);
			getWeap.Add ( newObj );
			AllWeapon = getWeap.ToArray();
		}
		//thisPlayer.WeapText.text = newObj.name;

		Transform objTrans = newObj.transform;
		objTrans.position = GetTrans.position;
		objTrans.localScale = Vector3.zero;

		thisPlayer.WeaponSwitch ++;
		int currId = thisPlayer.IdPlayer;

		if ( updateWeapon[currId] != null )
		{
			Destroy ( updateWeapon[currId].CurrObj );
		}

		updateWeapon[currId].CurrObj = newObj;

		//thisPlayer.UiAmmo.DOFillAmount (1, 0.1f + DelayNewWeapon);
        DOVirtual.DelayedCall(DelayNewWeapon, () =>
        {
            thisPlayer.UiAmmo.fillAmount = 1;
            Manager.Ui.WeaponNew(thisPlayer.IdPlayer);
        });

		DOVirtual.DelayedCall ( DelayNewWeapon * 0.25f, () =>
		{
			objTrans.DOScale ( Vector3.one, DelayNewWeapon * 0.5f );
			objTrans.DOLocalRotateQuaternion ( Quaternion.identity, DelayNewWeapon * 0.65f );
			objTrans.DOLocalMove ( Vector3.zero + Vector3.up * 5, DelayNewWeapon * 0.65f).OnComplete ( () =>
			{
				objTrans.SetParent(thisPlayer.WeaponPos);
				objTrans.DOLocalRotateQuaternion ( Quaternion.identity, DelayNewWeapon * 0.1f );
				objTrans.DOLocalMove ( Vector3.zero, DelayNewWeapon * 0.1f ).OnComplete ( () =>
				{
					thisPlayer.UpdateWeapon ( newObj.GetComponent<WeaponAbstract>() );

					updateWeapon[currId].CurrObj = null;
				});
			});
		} );
	}

	int lastNbr = 1;

	public void AddItem ( int lenghtItem, bool inv = false )
	{

        if(inv)
            Manager.Ui.PopPotions(PotionType.Less);

        NbrItem += lenghtItem;

		int currNbr = NbrItem - (nbrTotalSlide - 1) * 100;
		Image[] getFeedBack = Manager.Ui.GaugeFeedback;
		float getWait = 0;
		bool checkCurr = false;
		
		for ( int a = 0; a < getAllTween.Count; a ++ )
		{
			getAllTween[a].Kill(true);
		}
		getAllTween.Clear();

		Manager.Ui.GetGauge.DOKill(true);
//		Debug.Log( NbrItem + " / " + currNbr + " / " + currNbr * 0.01f);
		Manager.Ui.GetGauge.DOFillAmount ( currNbr * 0.01f, 0.5f );
		
		/*if ( !inv )
		{
			//updateFeed ( getFeedBack, 0, currNbr, inv );
		}
		else
		{
			//updateFeed ( getFeedBack, getFeedBack.Length - 1, currNbr, inv );
		}*/
		
		//Manager.Ui.ScoreText.text = NbrItem.ToString();

		Manager.Ui.GetScores.UpdateValue( NbrItem, ScoreType.BoxWeapon, false );
		int getCal = lastNbr;
		while ( currNbr >= lastNbr * 20 && lastNbr * 20 <= 100 )
		{
			int thisNbr = lastNbr;
			DOVirtual.DelayedCall (0.1f * (lastNbr - getCal + 0.5f), () => 
			{
				Manager.Ui.GaugeLevelGet(thisNbr - 1);
			});
			lastNbr++;
		}
	
		while ( currNbr > 100 )
		{
			lastNbr = 1;
			checkCurr = true;
			nbrTotalSlide ++;
			Manager.Ui.MultiplierNew( nbrTotalSlide );
			currNbr -=100;
		}	
		
		if ( checkCurr )
		{
			Tween getTween;
			getTween = DOVirtual.DelayedCall(0.5f, () =>
			{
				getTween = Manager.Ui.GetGauge.DOFillAmount ( 0, 0.5f ).OnComplete ( () =>
				{
					lastNbr = 1;
					while ( currNbr >= lastNbr * 20 )
					{
						int thisNbr = lastNbr;
						getTween = DOVirtual.DelayedCall (0.1f * lastNbr, () => 
						{
							Manager.Ui.GaugeLevelGet(thisNbr - 1);
						});
						lastNbr++;
					}
			
					getTween = Manager.Ui.GetGauge.DOFillAmount ( currNbr * 0.01f, 0.5f );

				});
			});
			getAllTween.Add ( getTween );
			
			/*Tween getTween;
			Tween getTween = DOVirtual.DelayedCall(0.5f, () =>
			{
				Manager.Ui.GetGauge.DOKill(true);
				getTween = Manager.Ui.GetGauge.DOFillAmount ( 0, 0.5f ).OnComplete ( ( ) =>
				{
					getTween = Manager.Ui.GetGauge.DOFillAmount ( currNbr * 0.01f, 0.5f );
					getAllTween.Add ( getTween );
				});
				getAllTween.Add ( getTween );
			});
			getAllTween.Add ( getTween );
			
			/*Tween getTween;
			getTween = DOVirtual.DelayedCall(0.5f, () => 
			{
				getWait = 0.5f;
				resetFeed ( getFeedBack, getFeedBack.Length - 1);

				getTween = DOVirtual.DelayedCall(0.5f, () => 
				{
					if ( !inv )
					{
						updateFeed ( getFeedBack, 0, currNbr, inv );
					}
					else
					{
						updateFeed ( getFeedBack, getFeedBack.Length - 1, currNbr, inv );
					}
				});
				getAllTween.Add ( getTween );
			});

			getAllTween.Add ( getTween );*/
		}
	}

    /*
    private void Update()
    {
    }
    */

    void updateFeed ( Image[] getFeedBack, int currInd, int currNbr, bool inv = false )
	{


        float getTime = 0.1f;
		float getCal = (currNbr - 20 * currInd ) * 0.05f;
		if ( getCal < 0 )
		{
			getCal = 0;
		}
		if ( getFeedBack[currInd].fillAmount == getCal )
		{
			if ( !inv )
			{
				currInd ++;
				if ( currInd < getFeedBack.Length )
				{
					updateFeed ( getFeedBack, currInd, currNbr, inv );
				}
			}
			else
			{
				currInd --;
				if ( currInd >= 0 )
				{
					updateFeed ( getFeedBack, currInd, currNbr, inv );
				}
			}
		}
		else
		{
			Tween getTween;
			getFeedBack[currInd].DOKill();
			getTween = getFeedBack[currInd].DOFillAmount(getCal, getTime).OnComplete (() => 
			{
				if ( !inv )
				{
					currInd ++;

					if ( currInd < getFeedBack.Length )
					{
						updateFeed ( getFeedBack, currInd, currNbr, inv );
					}
				}
				else
				{
					currInd --;

					if ( currInd >= 0 )
					{
						updateFeed ( getFeedBack, currInd, currNbr, inv );
					}
				}
				
			});
			getAllTween.Add(getTween);
		}
	}

	void resetFeed ( Image[] getFeedBack, int currInd )
	{
		Tween getTween;
		getFeedBack[currInd].DOKill();
		getTween = getFeedBack[currInd].DOFillAmount(0, 0.1f).OnComplete (() => 
		{
			currInd --;

			if ( currInd >= 0 )
			{
				resetFeed ( getFeedBack, currInd );
			}
		});
		getAllTween.Add(getTween);
	}

	public void TakeHit ( )
	{
		if ( invc )
		{
			return;
		}

		invc = true;

		DOVirtual.DelayedCall(InvincibleTime, () =>
		{
			invc = false;
		});
		
		int calLost = (int)((NbrItem * pourcLoot) * 0.01f);

		if ( calLost < MinLost )
		{
			calLost = MinLost;

			if ( calLost > NbrItem )
			{
				calLost = NbrItem;
			}
		}

		NbrItem -= calLost;
		
		
		//Manager.Ui.ScoreText.text = NbrItem.ToString();
		
		int getMult = (int)NbrItem / 100 + 1;
		if ( getMult > nbrTotalSlide )
		{
			Manager.Ui.MultiplierNew(getMult);
			nbrTotalSlide = getMult;
		}
		else if ( getMult < nbrTotalSlide )
		{
			nbrTotalSlide = getMult;
		}
		
		
		AddItem(0, true);
	}
	#endregion

	#region Private Methods
	void OnTriggerEnter ( Collider thisColl )
	{

		string tag = thisColl.tag;
		if ( tag == Constants._EnemyBullet )
		{
			TakeHit ();
		}
	}
	#endregion

}

		//Debug.Log( NbrItem + " / " + currNbr + " / " + currNbr * 0.01f);