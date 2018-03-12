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
	#endregion
	
	#region Mono
	void Awake () 
	{
		thisTrans = transform;
	}
	#endregion
	
	#region Public Methods
	public void OnInteract ( PlayerController thisPlayer )
	{
		if ( NbrTouchToDrop > 0 )
		{
			NbrTouchToDrop --;
		}
		else 
		{
			int b;
			for ( int a = 0; a < NbrDropByDrop; a ++ )
			{
				if ( NbrItem > 0 )
				{
					DOVirtual.DelayedCall ( Random.Range(0, 0.2f), ()=> 
					{
						for ( b = 0; b < ValueOnDrop; b ++ )
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

							thisPlayer.AllItem.Add(newItem);
							}
						
					});
					
					NbrItem --;
				}
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
	#endregion

}
