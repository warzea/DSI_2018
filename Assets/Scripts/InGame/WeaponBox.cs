using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponBox : MonoBehaviour 
{
	#region Variables
	Transform GetTrans;
	public GameObject[] AllWeapon;
	public float DelayNewWeapon = 1;

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
	public void NewWeapon ( PlayerController thisPlayer )
	{
		GameObject newObj = (GameObject) Instantiate ( AllWeapon[Random.Range(0, AllWeapon.Length)], thisPlayer.WeaponPos );
		Transform objTrans = newObj.transform;
		objTrans.position = GetTrans.position;

		int currId = thisPlayer.IdPlayer;

		if ( updateWeapon[currId] != null )
		{
			Destroy ( updateWeapon[currId].CurrObj );
		}

		updateWeapon[currId].CurrObj = newObj;

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
	#endregion

	#region Private Methods
	#endregion

}
