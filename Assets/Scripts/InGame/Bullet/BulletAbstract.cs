using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletAbstract : MonoBehaviour
{
    #region Variables
    [Tooltip("Only for explosion")]
    public GameObject GetEffect;
    public Trajectoir ThisTrajectoir;
    public float MoveSpeed = 10;
    [HideInInspector]
    public Vector3 direction = Vector3.zero;

    [HideInInspector]
    public int BulletDamage = 1;
    [HideInInspector]
    public float BulletRange = 10;

    [HideInInspector]
    public float TimeStay = 0;

    [HideInInspector]
    public float WidthRange;
    [HideInInspector]
    public float SpeedZone;

    [HideInInspector]
    public bool Through = false;
    [HideInInspector]
    public float Diameter = 0;
    [HideInInspector]
    public bool Projectil = true;
    [HideInInspector]
    public float FarEffect = 1;
    [HideInInspector]
    public float TimeFarEffect = 1;
    Transform thisTrans;
    Vector3 startPos;
    Vector3 newPos;
    BoxCollider getBox;
    //bool checkEnd = false;
    [HideInInspector]
    public bool canExplose;
    bool checkEnd = false;
    bool blockUpdate = false;
    float getDistScale = 0;
    #endregion

    #region Mono
    protected virtual void Start()
    {
        thisTrans = transform;
        startPos = thisTrans.position;
        newPos = startPos;

        if (direction == Vector3.zero)
        {
            direction = thisTrans.forward;
        }

        if (!Projectil)
        {
            getBox = gameObject.AddComponent<BoxCollider>();
            getBox.isTrigger = true;
            playZone();
        }
    }

    #endregion

    #region Public Methods
    void Update()
    {
        if (blockUpdate)
        {
            return;
        }

        if (!Projectil)
        {
            thisTrans.position = newPos + thisTrans.forward * getDistScale;
            thisTrans.localScale = startPos;
            //getBox.center =  * 0.5f;
            //getBox.size = ;
            return;
        }

        if (Vector3.Distance(startPos, thisTrans.position) < BulletRange)
        {
            switch (ThisTrajectoir)
            {
                case Trajectoir.Standard:
                    thisTrans.localPosition += direction * Time.deltaTime * MoveSpeed;
                    break;
            }
        }
        else if (!checkEnd)
        {
            Destroy(gameObject, 5);

            //Destroy ( gameObject, TimeStay );
            checkEnd = true;
        }
    }
    #endregion

    #region Private Methods
    void playZone()
    {
        GetComponent<SphereCollider>().enabled = false;
        startPos = Vector3.zero;

        DOTween.To(() => getDistScale, x => getDistScale = x, WidthRange * 0.5f * FarEffect, TimeFarEffect);
        DOTween.To(() => startPos, x => startPos = x, new Vector3(WidthRange, 5, BulletRange), SpeedZone).OnComplete(() =>
        {
            Destroy(gameObject, TimeStay);
        });
    }
    void OnTriggerEnter(Collider collision)
    {
        if (blockUpdate)
        {
            return;
        }
        if (collision.tag == Constants._Enemy)
        {
            if (canExplose)
            {
                blockUpdate = true;
                if (GetEffect != null)
                {
                    Instantiate(GetEffect, thisTrans.position, Quaternion.identity);
                }

                SphereCollider thisSphere = gameObject.AddComponent<SphereCollider>();
                //thisSphere.radius = Diameter;
                thisSphere.isTrigger = true;
                thisTrans.localScale = new Vector3(Diameter, Diameter, Diameter);
            }

            if (!Through)
            {
                if (canExplose)
                {
                    if (GetEffect != null && GetEffect.GetComponent<ParticleSystem>())
                    {
                        Destroy(gameObject, GetEffect.GetComponent<ParticleSystem>().main.duration);
                    }
                    else
                    {
                        Destroy(gameObject, 1.5f);
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (collision.tag == Constants._Wall)
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
