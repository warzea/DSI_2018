using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    #region Variables
    public float smoothTime = 0.3F;
    Vector3 velocity = Vector3.zero;
    Transform Target;
    Transform thisTrans;
    #endregion

    #region Mono
    void Start()
    {
        thisTrans = transform;

        if ( Target == null )
        {
            Target = Manager.GameCont.WeaponB.transform;
        }
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position, smoothTime * Time.deltaTime);
    }
    #endregion

    #region Public Methods
    public void InitGame(Transform setTarget = null)
    {
        if (setTarget != null)
        {
            Target = setTarget;
        }
    }

    public void UpdateTarget(Transform newTarget)
    {
        Target = newTarget;
    }
    #endregion

    #region Private Methods
    #endregion
}
