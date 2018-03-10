using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RainbowMove : MonoBehaviour
{
    public enum TypeMoves{
        LocalVertical,
        LocalHorizontal,
        GlobalVertical,
        GlobalHorizontal,
        ShakePosition,
        ObjectTransform
    }
    /*
    public enum TypePop
    {
        Yes, 
        No
    }
    */
    
    [Header("MOVES DATA")]
    int index = -1;
    public float time = .2f;
    public TypeMoves movesType;
    public float[] moves;
    public Transform[] ObjectTransform;
    public Ease easeType;
    public bool breakMove;
    public int breakNumber;
    /*
    [Header("POP DATA")]
    public TypePop popType;
    public float popFadeInDuration;
    public float popFadeOutDuration;
    public float popDurationBeforeFadeOut;
    */
    Transform currT;
    Transform stockedPos;
    private float LocalY;

	Vector3 startPos;

	public void reStart()
	{
		index = - 1;
	}

    private void Update()
    {
        if (transform.name == "2D Light Effect")
        {
            Debug.Log(startPos);
            Debug.Log(index);
        }
    }

    private void Start()
    {
        stockedPos = transform;
    }

    void OnEnable()
    {
		currT = transform;
		startPos = currT.localPosition;
        Next();
        /*
        if(popType == TypePop.Yes)
        {
            if (GetComponent<CanvasGroup>())
            {
                GetComponent<CanvasGroup>().DOFade(1, popFadeInDuration);
                DOVirtual.DelayedCall(popDurationBeforeFadeOut, ()=>
                {
                    this.enabled = false;
                });
            }
        }*/
        //transform.DOMoveY(LocalY, 0);
        //Debug.Log(LocalY);
    }

    void Next()
    {

        //if(breakNumber != index)
        //{

            if (movesType == TypeMoves.ObjectTransform)
                index = (index + 1) % ObjectTransform.Length;
            else
                index = (index + 1) % moves.Length;


            if (movesType == TypeMoves.ObjectTransform)
                currT.DOLocalMove(ObjectTransform[index].localPosition, time).SetEase(easeType).OnComplete(() => Next());

            if (movesType == TypeMoves.LocalHorizontal)
                currT.DOLocalMoveX(moves[index], time).SetEase(easeType).OnComplete(() => Next());

            if (movesType == TypeMoves.LocalVertical)
                currT.DOLocalMoveY(moves[index], time).SetEase(easeType).OnComplete(() => Next());

            if (movesType == TypeMoves.GlobalHorizontal)
                currT.DOMoveX(moves[index], time).SetEase(easeType).OnComplete(() => Next());

            if (movesType == TypeMoves.GlobalVertical)
                currT.DOMoveY(moves[index], time).SetEase(easeType).OnComplete(() => Next());

            if (movesType == TypeMoves.ShakePosition)
                currT.DOShakePosition(time, moves[index]).SetEase(easeType).OnComplete(() => Next());

        //}


        if (breakMove && breakNumber == index)
        {
            if (transform.name == "2D Light Effect")
                Debug.Log("Yes");
            currT.DOKill(true);
            currT.DOLocalMove(stockedPos.localPosition, 0);
            currT.localPosition = startPos;
            index = 0;
            //reStart();
        }
    }

    void OnDisable()
    {
        /*
        if (popType == TypePop.Yes)
        {
            if (GetComponent<CanvasGroup>())
            {
                GetComponent<CanvasGroup>().DOFade(0, popFadeOutDuration).OnComplete(() =>
                {

                    currT.DOKill();
                    currT.localPosition = startPos;
                });
            }
        }
        else
        {*/
            currT.DOKill();
            currT.localPosition = startPos;
        //}

      /*  if (movesType == Type.LocalHorizontal)
			currT.DOLocalMoveX(moves[index], time).SetEase(easeType).OnComplete(() => Next());

        if (movesType == Type.LocalVertical)
        {
			currT.DOLocalMoveY(moves[index], time).SetEase(easeType).OnComplete(() => Next());

            //transform.DOMoveY(LocalY, 0);
            //Debug.Log("Disable " + LocalY);
			//
			//EmptyChild.DOKill();

            //LocalY = transform.position.y;
        }

        if (movesType == Type.GlobalHorizontal)
			currT.DOMoveX(moves[index], time).SetEase(easeType).OnComplete(() => Next());

        if (movesType == Type.GlobalVertical)
			currT.DOMoveY(moves[index], time).SetEase(easeType).OnComplete(() => Next());
		*/
    }
}