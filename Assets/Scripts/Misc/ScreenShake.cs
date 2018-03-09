using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenShake : MonoBehaviour
{
	public static ScreenShake Singleton;

    Tween punchPos;
    Tween shakePos;
    Tween shakeFall;

    float dir = 1;

	void Awake ()
	{
		
		if (ScreenShake.Singleton == null) {
			ScreenShake.Singleton = this;
		} else {
			Destroy (gameObject);
		}
	}


    public void ShakeFall()
    {
        shakeFall.Kill(true);
        shakeFall = transform.DOPunchPosition(new Vector3(1 * 2, 1, 0), .9f, 10, 1.5f);
    }

	public void ShakeHitSimple ()
	{
        punchPos.Kill(true);
        
        dir *= -1; 
        punchPos = transform.DOPunchPosition(new Vector3(1 * .85f * dir, 0, 1 * 1.5f), .5f, 2, 1);
    }

    public void ShakeEnemy()
    {
        punchPos.Kill(true);
        shakePos.Kill(true);
        //side = UnityEngine.Random.RandomRange(-2, 2);
        //transform.DOPunchRotation (Vector3.one * .5f, .3f, 3, 1);

        shakePos = transform.DOShakePosition(.15f, .15f, 12, 180);
        //transform.DOPunchPosition(new Vector3(1*1.5f, 0, 1*.5f), .25f, 4, 1);
    }

    public void ShakeIntro()
    {
        //transform.GetComponent<RainbowRotate>().enabled = false;
        punchPos.Kill(true);
        shakePos.Kill(true);

       // float rdmRot = UnityEngine.Random.Range(-60, 60);
       // transform.DOLocalRotate(new Vector3(0, 0, rdmRot), 0.05f);

        dir *= -1;
        punchPos = transform.DOPunchPosition(new Vector3(1 * .85f * dir, 0, 1 * 1.5f), .5f, 2, 1);
        shakePos = transform.DOShakeRotation(.5f, 35f, 25, 120);
        //punchPos = transform.DOPunchPosition(new Vector3(1 * .85f * dir, 0, 1 * 1.5f), .5f, 2, 1);
        //punchPos = transform.DOShakePosition(.5f, 1.5f, 15, 150);
    }

    public void ShakeMad()
    {
        transform.DOShakePosition(.4f, .65f, 22, 90);
    }

	public void StopShake ( )
	{
		shakePos.Kill ( true );
		punchPos.Kill(true);
		shakeFall.Kill(true);
	}

    public void ShakeGameOver()
    {


        //transform.DOKill(false);

        //transform.DOShakeRotation(1f, 2f, 22, 90);
        shakePos = transform.DOShakePosition(1f, 2f, 22, 90);
    }

}