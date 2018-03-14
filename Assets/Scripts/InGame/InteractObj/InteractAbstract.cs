using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractAbstract : MonoBehaviour 
{
	#region Variables
	public int NbrItem = 10;
	public int ValueOnDrop = 5;
	public int NbrDropByDrop = 2;
	public int NbrTouchToDrop = 2;
	public GameObject[] ItemDrop;
	Transform thisTrans;
	bool checkItem = true;
	int valDrop;
	Tween thisT;
	#endregion
	
	#region Mono
	void Awake () 
	{
		thisTrans = transform;
		valDrop = ValueOnDrop;
	}
	void Start ( )
	{
		System.Action<ChestEvent> thisAct = delegate (ChestEvent thisEvnt)
        {
			Camera getCam = Manager.GameCont.MainCam;
			Vector3 getCamPos = getCam.WorldToViewportPoint(thisTrans.position);

            if (getCamPos.x > 1f || getCamPos.x < 0f || getCamPos.y > 1f || getCamPos.y < 0f)
            {

			}
			else
			{
				if ( thisT != null)
				{
					thisT.Kill();
				}
				
           		valDrop *= thisEvnt.Mult;
				multEffect (true);
				thisT = DOVirtual.DelayedCall(thisEvnt.TimeMult, () =>
				{
					multEffect (false);
				});

				if ( thisEvnt.ThisMat != null )
				{
					foreach ( Renderer thisMat in GetComponentsInChildren<Renderer>() )
					{
						thisMat.material = thisEvnt.ThisMat;
					}
				}
			}

			
        };

        Manager.Event.Register(thisAct);
	}
	#endregion
	
	#region Public Methods
	public void OnInteract ( PlayerController thisPlayer )
	{
		if ( NbrTouchToDrop > 0 )
		{
			NbrTouchToDrop --;
		}
		else if ( NbrItem > 0 )
		{
            NbrItem--;

            thisPlayer.CurrItem += NbrDropByDrop * valDrop;

			Manager.Ui.AllPotGet[thisPlayer.IdPlayer].GetComponent<PotionFollowP>().NewValue ( thisPlayer.CurrItem );

			int b;
			for ( int a = 0; a < Random.Range(5, 20); a ++ )
			{
				DOVirtual.DelayedCall ( Random.Range(0, 0.2f), ()=> 
				{
					for ( b = 0; b < valDrop; b ++ )
					{
						GameObject newItem = (GameObject) Instantiate (ItemDrop[Random.Range(0, ItemDrop.Length - 1)], thisPlayer.BagPos);
						Transform getTrans = newItem.transform;
						getTrans.position = thisTrans.position + new Vector3 ( Random.Range(-0.5f, 0.51f), 0, Random.Range(-0.5f, 0.51f) );
							
						getTrans.DOLocalMove(Vector3.zero + Vector3.up * 3, 0.5f).OnComplete ( () => 
						{
							getTrans.DOScale(Vector3.zero, 0.5f).OnComplete( () => 
							{
								newItem.SetActive(false);
							});
								
							getTrans.DOLocalMove(Vector3.zero, 0.5f);
						});

						if ( thisPlayer.AllItem.Count < 20 )
						{
							thisPlayer.AllItem.Add(newItem);
						}
						else
						{
							Destroy(newItem, 1.1f);
						}
					}
				});
					
			}

			if ( NbrItem == 0 && checkItem )
			{
				checkItem = false;
				thisPlayer.NbrChest ++;
			}
		}
	}
	#endregion

	#region Private Methods

	void multEffect ( bool isEnable )
	{

	}
	#endregion

}
