using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractAbstract : MonoBehaviour 
{
	#region Variables
	public int NbrItem = 10;
	public int NbrDropByTouch = 2;
	public GameObject[] ItemDrop;

	Transform thisTrans;
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
		for ( int a = 0; a < NbrDropByTouch; a ++ )
		{
			if ( NbrItem > 0 )
			{
				GameObject newItem = (GameObject) Instantiate (ItemDrop[Random.Range(0, ItemDrop.Length - 1)], thisPlayer.BagPos);
				Transform getTrans = newItem.transform;

				getTrans.position = thisTrans.position;
				
				getTrans.DOLocalMove(Vector3.zero, 1);
				getTrans.DOScale(Vector3.zero,1).OnComplete( () => 
				{
					newItem.SetActive(false);
				});

				thisPlayer.AllItem.Add(newItem);
				NbrItem --;
			}
		}
	}
	#endregion

	#region Private Methods
	#endregion

}
