using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjLost : MonoBehaviour 
{
	#region Variables
	public GameObject ThisObj;
	public int NbrItem;
	Transform thisTrans;
	Camera GetCamera;
	bool check = false;
	#endregion
	
	
	#region Mono
	void Awake ( )
	{
		thisTrans = transform;
		GetCamera = Manager.GameCont.MainCam;

		StartCoroutine(waitCheck());
	}
	#endregion
	
	
	#region Public
	void Update ()
	{
		Vector3 getCamPos = GetCamera.WorldToViewportPoint(thisTrans.position + Vector3.up * 5);

		if (getCamPos.x > 1 || getCamPos.x < 0 || getCamPos.y > 1 || getCamPos.y < 0)
		{
			Destroy(ThisObj);
			Destroy(gameObject);
		}
	}
	#endregion
	
	
	#region Private
	IEnumerator waitCheck ( )
	{
		yield return new WaitForSeconds(1f);

		check = true;
	}
	void OnTriggerEnter (Collider thisColl)
	{
		if ( thisColl.tag == Constants._Player && check )
		{
			PlayerController getPC = thisColl.GetComponent<PlayerController>();
			getPC.CurrItem = NbrItem;
			Manager.Ui.AllPotGet[getPC.IdPlayer].GetComponent<PotionFollowP>().Nbr = NbrItem;
			
			foreach ( ItemLost thisT in thisTrans.GetComponentsInChildren<ItemLost>() )
			{
				thisT.GoTarget(getPC);
			}

			Destroy(ThisObj);
			Destroy(gameObject);
		}
	}
	#endregion
}
