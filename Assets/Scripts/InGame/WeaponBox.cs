using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WeaponBox : MonoBehaviour 
{
	#region Variables
	public float InvincibleTime = 1;
	Transform GetTrans;
	public int pourcLoot = 10;
	public GameObject[] AllWeapon;
	public float DelayNewWeapon = 1;

	[HideInInspector]
	public int NbrItem = 0;

	[HideInInspector]
	public bool CanControl = true;

	List<PlayerWeapon> updateWeapon; 
	List<Tween> getAllTween;

	int nbrTotalSlide = 1;
	bool invc = false;
	#endregion
	
	#region Mono
	void Awake ( )
	{
		getAllTween = new List<Tween>();
		updateWeapon = new List<PlayerWeapon>();
		GetTrans = transform;

		for ( int a = 0; a < 4; a ++)
		{
			updateWeapon.Add ( new PlayerWeapon () );
			updateWeapon[a].IDPlayer = a;
		}
	}
	#endregion
	
	#region Public Methods
	public void NewWeapon ( PlayerController thisPlayer, GameObject newObj = null )
	{
        //TRANSFO CANON
        //transform.DOScaleX(1, .15f).SetEase(Ease.InSine);

        //transform.DOScaleY(1, .15f).SetEase(Ease.InSine);

        //transform.DOScaleZ(2.5f, .15f).SetEase(Ease.InSine).OnComplete(()=> {
        transform.DOShakeScale(.15f, .8f, 25, 0).OnComplete(() => { 

            transform.DOScaleZ(1.75f, .1f).SetEase(Ease.Linear).OnComplete(() => {

                transform.DOScaleZ(4.5f, .1f).SetEase(Ease.Linear);

                DOVirtual.DelayedCall(.1f, () => {

                    transform.DOScaleZ(1.38f, .1f).SetEase(Ease.OutSine).OnComplete(()=> {

                        transform.DOShakeScale(.2f, .3f, 18, 0);

                    });

                });
            });
        });




        Manager.Ui.WeaponChange(thisPlayer.IdPlayer);

		if ( newObj == null )
		{
			newObj = (GameObject) Instantiate ( AllWeapon[Random.Range(0, AllWeapon.Length)], thisPlayer.WeaponPos );
		}
		else
		{
			List<GameObject> getWeap = new List<GameObject>(AllWeapon);
			getWeap.Add ( newObj );
			AllWeapon = getWeap.ToArray();
		}

		thisPlayer.WeapText.text = newObj.name;

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
		objTrans.DOScale ( Vector3.one, DelayNewWeapon );
		DOVirtual.DelayedCall ( 0.1f, ( ) => 
		{
			objTrans.DOLocalRotateQuaternion ( Quaternion.identity, DelayNewWeapon );
			objTrans.DOLocalMove(Vector3.zero, DelayNewWeapon).OnComplete ( () =>
			{
				thisPlayer.UpdateWeapon ( newObj.GetComponent<WeaponAbstract>() );

				updateWeapon[currId].CurrObj = null;
			});
		});
	}

	public void AddItem ( int lenghtItem, bool inv = false )
	{
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

		while ( currNbr > 100 )
		{
			checkCurr = true;
			nbrTotalSlide ++;
			Manager.Ui.MultiplierNew( nbrTotalSlide );
			currNbr -=100;
		}	
		
		if ( checkCurr )
		{
			Tween getTween = DOVirtual.DelayedCall(0.5f, () =>
			{
				Manager.Ui.GetGauge.DOKill(true);
				Manager.Ui.GetGauge.DOFillAmount ( currNbr * 0.01f, 0.5f );
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

		NbrItem -= (int)((NbrItem * pourcLoot) * 0.01f);

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