using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

public class BulletAbstract : MonoBehaviour
{
    #region Variables

    public GameObject PrefabExplosion;
    [Tooltip ("Only for explosion")]
    public GameObject GetEffect;
    public Trajectoir ThisTrajectoir;
    public float MoveSpeed = 10;
    public string NameAudio = "";
    public float DefinitiveDestroy = 5;
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
    [HideInInspector]
    public PlayerController thisPlayer;
    [HideInInspector]
    Transform thisTrans;
    Transform playerTrans;
    Vector3 startPos;
    Vector3 newPos;
    BoxCollider getBox;

    //bool checkEnd = false;
    [HideInInspector]
    public bool canExplose = false;
    bool checkEnd = false;
    bool blockUpdate = false;
    float getDistScale = 0;
    float timeEffect = 0.1f;
    bool checkUpdate = true;

    #endregion

    #region Mono

    protected virtual void Start ( )
    {
        thisTrans = transform;
        playerTrans = thisPlayer.transform;
        startPos = thisTrans.position;
        newPos = startPos;

        if (direction == Vector3.zero)
        {
            direction = thisTrans.forward;
        }

        if (!Projectil)
        {
            getBox = gameObject.AddComponent<BoxCollider> ( );
            getBox.isTrigger = true;
            playZone ( );
        }

        if (ThisTrajectoir == Trajectoir.Nothing)
        {
            checkUpdate = false;
        }

        if (GetEffect != null)
        {
            ParticleSystem thisPart = GetEffect.GetComponentInChildren<ParticleSystem> ( );

            if (thisPart != null)
            {
                timeEffect = thisPart.duration;
            }
        }

        checkRayCast ( );
    }

    #endregion

    #region Public Methods

    void Update ( )
    {
        if (blockUpdate || !checkUpdate)
        {
            return;
        }

        if (!Projectil)
        {
            getBox.center = new Vector3 (0, 0, getDistScale);
            getBox.size = startPos;
            //getBox.center =  * 0.5f;
            //getBox.size = ;
            return;
        }

        if (Vector3.Distance (startPos, thisTrans.position)< BulletRange)
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
            if (canExplose)
            {
                instExplo (thisTrans.position);

                if (Projectil)
                {
                    destObj (TimeStay);

                    blockUpdate = true;
                }
                else
                {
                    destObj (0);
                }
            }
            else if (Projectil)
            {
                destObj (0);
            }
            else
            {
                destObj (TimeStay);
            }
            //Destroy ( gameObject, TimeStay );
            checkEnd = true;
        }
    }

    #endregion

    #region Private Methods

    void checkRayCast ( )
    {
        RaycastHit [ ] allHit;
        string getTag;

        allHit = Physics.RaycastAll (playerTrans.position, playerTrans.forward, BulletRange);

        foreach (RaycastHit thisRay in allHit)
        {
            getTag = thisRay.collider.tag;

            if (getTag == Constants._Wall && thisRay.distance < BulletRange)
            {
                BulletRange = thisRay.distance - 0.5f;
            }
        }
    }

    Tween t1;
    Tween t2;

    void playZone ( )
    {
        GetComponent<Collider> ( ).enabled = false;
        startPos = Vector3.zero;

        t1 = DOTween.To (( )=> getDistScale, x => getDistScale = x, BulletRange * 0.5f * FarEffect, TimeFarEffect);
        t2 = DOTween.To (( )=> startPos, x => startPos = x, new Vector3 (WidthRange, 5, BulletRange), SpeedZone).OnComplete (( )=>
        {
            destObj (TimeStay);
        });
    }

    void instExplo (Vector3 thisPos)
    {
        GameObject thisObj = (GameObject)Instantiate (PrefabExplosion, thisPos, thisTrans.rotation);
        ExploScript getExplo = thisObj.GetComponent<ExploScript> ( );
        Destroy (thisObj, TimeStay);
        Manager.Audm.OpenAudio (AudioType.Other, NameAudio);

        getExplo.TimeEffect = timeEffect;
        if (getExplo.TimeStay == 0)
        {
            getExplo.TimeStay = TimeStay;
        }

        if (!getExplo.GetEffect)
        {
            getExplo.GetEffect = GetEffect;
        }
        getExplo.ScaleExplo = Diameter;
    }

    void OnTriggerEnter (Collider collision)
    {
        if (blockUpdate)
        {
            return;
        }

        if (collision.tag == Constants._Enemy)
        {
            thisPlayer.CurrScore++;
            thisPlayer.CurrKillScore++;
            thisPlayer.ShootSucceed++;
            thisPlayer.currentEnemy++;

            AgentController thisController;
            AgentControllerCac thisControllerCac;
            thisController = collision.GetComponent<AgentController> ( );
            thisControllerCac = collision.GetComponent<AgentControllerCac> ( );

            if (thisPlayer.currentEnemy > thisPlayer.NbrEnemy)
            {
                thisPlayer.NbrEnemy = thisPlayer.currentEnemy;
            }

            for (int a = 0; a < thisPlayer.AllEnemy.Count; a++)
            {
                if (thisController != null)
                {
                    if (thisPlayer.AllEnemy [a].ThisType == thisController.ThisType)
                    {
                        thisPlayer.AllEnemy [a].NbrEnemy++;
                        break;
                    }
                }
                else
                {
                    if (thisPlayer.AllEnemy [a].ThisType == thisControllerCac.ThisType)
                    {
                        thisPlayer.AllEnemy [a].NbrEnemy++;
                        break;
                    }
                }
            }

            if (canExplose)
            {
                if (Projectil)
                {
                    blockUpdate = true;
                }

                instExplo (collision.ClosestPoint (thisTrans.position));
            }

            if (!Through)
            {
                destObj ( );
            }
        }
        else if (collision.tag == Constants._Wall && Projectil)
        {
            destObj ( );
        }
    }

    void destObj (float delay = 0)
    {
        t1.Kill ( );
        t2.Kill ( );

        StartCoroutine (waitDest (delay));
    }

    IEnumerator waitDest (float delay)
    {
        yield return new WaitForSeconds (delay);

        MeshRenderer thisMr = GetComponentInChildren<MeshRenderer> ( );
        Collider thisC = GetComponent<Collider> ( );
        if (thisMr != null && thisC != null)
        {
            thisMr.enabled = false;
            thisC.enabled = false;
            Destroy (gameObject, DefinitiveDestroy);
        }
        else
        {
            Destroy (gameObject);
        }
    }

    #endregion
}