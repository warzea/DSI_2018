using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour 
{
	#region Variables
	public Transform Target;
	public float TimeGoTarget = 1;
	Transform thisTrans;
	#endregion

	#region Mono
	void Start ( )
	{
		thisTrans = transform;
	}
	#endregion

	#region Public Methods
	public void UpdateTarget ( Transform newTarget )
	{
		Target = newTarget;
		thisTrans.SetParent(newTarget);

		thisTrans.DOLocalMoveX(0, TimeGoTarget, true);
		thisTrans.DOLocalMoveZ(0, TimeGoTarget, true);
	}
	#endregion

	#region Private Methods
	#endregion
}
