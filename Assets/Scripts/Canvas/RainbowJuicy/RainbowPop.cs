using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RainbowPop : MonoBehaviour
{
    public float popFadeInDuration;
    public float popFadeOutDuration;
    public float popDurationBeforeFadeOut;

    Transform currT;
    private float LocalY;

    Vector3 startPos;

    void OnEnable()
    {
        if (GetComponent<CanvasGroup>())
        {
            GetComponent<CanvasGroup>().DOFade(1, popFadeInDuration);
            DOVirtual.DelayedCall(popDurationBeforeFadeOut, () =>
            {
                GetComponent<CanvasGroup>().DOFade(0, popFadeOutDuration).OnComplete(() =>
                {
                    Destroy(transform.gameObject);
//Z                    Debug.Log(transform.gameObject);
                });
            });
        }
    }
    void OnDisable()
    {
    }
}