using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponBox : MonoBehaviour 
{
	#region Variables
	Transform GetTrans;
	public int pourcLoot = 10;
	public GameObject[] AllWeapon;
	public float DelayNewWeapon = 1;

	[HideInInspector]
	public int NbrItem = 0;

	[HideInInspector]
	public bool CanControl = true;

	List<PlayerWeapon> updateWeapon; 
	#endregion
	
	#region Mono
	void Awake ( )
	{
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

	public void AddItem ( int lenghtItem )
	{
		NbrItem += lenghtItem;

		Manager.Ui.GetScores.UpdateValue( lenghtItem, ScoreType.BoxWeapon );
	}

	public void TakeHit ( )
	{
		NbrItem /= pourcLoot;
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
