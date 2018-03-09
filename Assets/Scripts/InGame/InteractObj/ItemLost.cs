using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemLost : MonoBehaviour 
{
	#region Variables
	Transform thisTrans;
	Vector3 getScale;
	#endregion
	
	#region Mono
	void Awake ( )
	{
		Collider thisColl = GetComponent<Collider>();
		if ( !GetComponent<Collider>() )
		{
			thisColl = gameObject.AddComponent<Collider>();
		}
		thisColl.isTrigger = true;
		thisColl.enabled = false;

		thisTrans = transform;
		getScale = thisTrans.localScale;
	}
	#endregion
	
	#region Public Methods
	public void EnableColl ( bool active = true )
	{
		thisTrans.SetParent(null);
		thisTrans.gameObject.SetActive(true);
		//thisTrans.DOLocalMove ( thisTrans.localPosition + new Vector3 ( Random.Range ( -1f, 1f ), 0, Random.Range ( -1f, 1f ) ), 0.5f );
		thisTrans.DOScale(getScale, 1f).OnComplete( ()=>
		{
			Collider thisColl = GetComponent<Collider>();
			thisColl.enabled = active;
		});
	}
	#endregion

	#region Private Methods
	void OnTriggerEnter ( Collider thisColl )
	{
		if ( thisColl.tag == Constants._Player )
		{
			PlayerController thisPlayer = thisColl.GetComponent<PlayerController>();
			Transform getTrans = transform;
			getTrans.SetParent(thisPlayer.BagPos);
			
			getTrans.DOLocalMove(Vector3.zero + Vector3.up * 3, 0.5f).OnComplete ( () => 
			{
				getTrans.DOScale(Vector3.zero, 0.5f).OnComplete( () => 
				{
					gameObject.SetActive(false);
				});
				
				getTrans.DOLocalMove(Vector3.zero, 0.5f);
			});

			thisPlayer.AllItem.Add(gameObject);
			GetComponent<Collider>().enabled = false;
		}
	}
	#endregion

}
