using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

public class WeaponAbstract : MonoBehaviour
{
    #region Variables
    public GameObject SpeEffet;
    public GameObject ExploWeapon;
    public GameObject PrefabExplo;
    public string NameMusic;
    public float TimeBackPush = 0.1f;
    public int WeightRandom = 0;
    public bool AutoShoot = true;
    public bool Projectile = false;
    public bool Through = false;
    public bool Explosion = false;

    public int BulletCapacity;
    // -- siAuto
    public float FireRate;
    // -- fin auto

    // -- si manuel
    public float CoolDown;
    // -- fin manuel
    public float BackPush;

    [Range (0, 1)]
    [Tooltip ("Speed reduce pendant la phase de shoot")]
    public float SpeedReduce = 0.5f;

    public int Damage = 1;

    public float Range;
    // -- Si Projectile
    public int NbrBullet = 1;
    public float ScaleBullet = 1;
    public float SpeedBullet = 1;
    public bool Gust = true;
    // -- si rafale
    public float SpaceBullet = 0;
    // -- fin rafale

    // -- si spread
    public float Angle = 0;
    // -- fin spread
    // -- fin Projectile

    // -- si Zone
    public float TimeFarEffect;
    public float FarEffect;
    public float WidthRange;
    public float SpeedZone;
    public float TimeDest;
    // -- fin zone

    // -- si Explosion
    public float Diameter;
    // -- end Explosion

    //public float ForceProjection;
    //public float SpeedBullet = 10;

    public GameObject Bullet;
    public Transform SpawnBullet;

    [HideInInspector]
    public bool canShoot = true;

    bool blockShoot = false;
    public bool OnFloor = true;

    Transform getGargabe;
    [HideInInspector]
    public int getCapacity;

    IEnumerator GetEnumerator;

    #endregion

    #region Mono
    void Awake ()
    {
        getCapacity = BulletCapacity;
        getGargabe = Manager.GameCont.Garbage;
    }
    #endregion

    #region Public Methods

    public void weaponShoot (Transform playerTrans)
    {
        if (canShoot && getCapacity > 0 && !blockShoot)
        {
            Manager.Ui.ScreenShake ();

            //playerTrans.localPosition -= playerTrans.forward * BackPush * Time.deltaTime;
            string getTag;
            float getDist = BackPush * Time.deltaTime;
            int getDir = 1;
            if (getDist < 0)
            {
                getDir = -1;
                getDist = -getDist;
            }
            RaycastHit [] allHit;

            if (getDist > 0)
            {
                allHit = Physics.RaycastAll (playerTrans.position, -playerTrans.forward * getDir, getDist);

                foreach (RaycastHit thisRay in allHit)
                {
                    getTag = thisRay.collider.tag;

                    if (getTag == Constants._Wall && thisRay.distance < getDist)
                    {
                        getDist = thisRay.distance - 0.5f;
                    }
                }
            }

            if (getDist != 0)
            {
                playerTrans.DOLocalMove (playerTrans.localPosition - playerTrans.forward * getDist * getDir, TimeBackPush);
            }

            getCapacity--;
            canShoot = false;

            PlayerController getPC = playerTrans.GetComponent<PlayerController> ();
            getPC.UiAmmo.fillAmount = (float) getCapacity / BulletCapacity;

            if (getCapacity == 0)
            {
                Manager.Ui.WeaponEmpty (getPC.IdPlayer);
                getPC.autoShoot = false;
                getPC.CdShoot = 0;
                getPC.checkShoot = false;
                getPC.checkAuto = true;
            }

            if (SpeEffet == null)
            {
                Manager.Audm.OpenAudio (AudioType.Shoot, NameMusic);
            }

            customWeapon (playerTrans);
        }
        else if (getCapacity <= 0)
        {
            canShoot = false;

            PlayerController getPC = playerTrans.GetComponent<PlayerController> ();
            getPC.WeaponThrow++;
            Transform getTrans = transform;
            Vector3 getForward = playerTrans.forward;

            if (getPC.thisObjAudio != null)
            {
                Destroy (getPC.thisObjAudio);
            }

            getTrans.SetParent (null);
            getPC.UpdateWeapon ();

            //getRigid.AddForce(getForward * ForceProjection, ForceMode.VelocityChange);

            GetComponent<Collider> ().enabled = true;
            GetComponent<Collider> ().isTrigger = true;

            BulletAbstract thisBA = getTrans.gameObject.AddComponent<BulletAbstract> ();
            thisBA.direction = playerTrans.forward;
            thisBA.TimeStay = 0.2f;
            thisBA.thisPlayer = getPC;
            thisBA.Projectil = true;

            if (ExploWeapon != null && PrefabExplo != null)
            {
                thisBA.canExplose = true;
                thisBA.GetEffect = ExploWeapon;
                thisBA.PrefabExplosion = PrefabExplo;
            }

            thisBA.Diameter = 3;
            thisBA.BulletRange = playerTrans.GetComponent<PlayerController> ().DistThrowWeap;
            thisBA.MoveSpeed = playerTrans.GetComponent<PlayerController> ().SpeedThrow;
            thisBA.gameObject.tag = Constants._BulletPlayer;
            Destroy (GetComponent<WeaponAbstract> ());
            Manager.GameCont.WeaponB.NewWeapon (getPC);
        }
    }
    #endregion

    #region Private Methods
    void customWeapon (Transform thisPlayer)
    {
        if (Projectile)
        {
            if (Gust)
            {
                blockShoot = true;
                gustProjectile (NbrBullet, thisPlayer);
            }
            else
            {
                spreadProjectile (thisPlayer);
            }
        }
        else
        {
            zoneShoot (thisPlayer);
        }
    }

    void setNewProj (BulletAbstract thisBullet, PlayerController thisPlayer)
    {
        thisBullet.MoveSpeed = SpeedBullet;
        thisBullet.BulletDamage = Damage;
        thisBullet.BulletRange = Range;
        thisBullet.Through = Through;
        thisBullet.WidthRange = WidthRange;
        thisBullet.SpeedZone = SpeedZone;
        thisBullet.TimeStay = TimeDest;
        thisBullet.Projectil = Projectile;
        thisBullet.canExplose = Explosion;
        thisBullet.Diameter = Diameter;
        thisBullet.FarEffect = FarEffect;
        thisBullet.TimeFarEffect = TimeFarEffect;
        thisBullet.thisPlayer = thisPlayer;
        thisPlayer.ShootBullet++;
    }

    void zoneShoot (Transform thisPlayer)
    {
        GameObject getBullet = (GameObject) Instantiate (Bullet, SpawnBullet.position, thisPlayer.localRotation, getGargabe);

        setNewProj (getBullet.GetComponent<BulletAbstract> (), thisPlayer.GetComponent<PlayerController> ());
        waitNewShoot ();
    }

    void gustProjectile (int nbrLeft, Transform thisPlayer)
    {
        GameObject getBullet = (GameObject) Instantiate (Bullet, SpawnBullet.position, thisPlayer.localRotation, getGargabe);
        getBullet.transform.localScale *= ScaleBullet;
        setNewProj (getBullet.GetComponent<BulletAbstract> (), thisPlayer.GetComponent<PlayerController> ());

        DOVirtual.DelayedCall (SpaceBullet, () =>
        {
            nbrLeft--;
            if (nbrLeft > 0)
            {
                gustProjectile (nbrLeft, thisPlayer);
            }
            else
            {
                waitNewShoot ();
                blockShoot = false;
            }
        });
    }
    void spreadProjectile (Transform playerTrans)
    {
        float getAngle = Angle / NbrBullet;
        int nbrMidle = (int) (NbrBullet * 0.5f);
        int addNumb = 0;
        int addOnPair = 0;
        bool getPair = true;

        GameObject getBullet;
        BulletAbstract getScript;
        PlayerController thisPlayer = playerTrans.GetComponent<PlayerController> ();

        if (NbrBullet % 2 != 0)
        {
            addNumb++;
            getPair = false;
        }
        else
        {
            addOnPair = 1;
        }

        for (int a = addOnPair; a < NbrBullet + addOnPair; a++)
        {
            getBullet = (GameObject) Instantiate (Bullet, SpawnBullet.position, playerTrans.localRotation, getGargabe);
            getScript = getBullet.GetComponent<BulletAbstract> ();
            getBullet.transform.localScale *= ScaleBullet;

            setNewProj (getScript, thisPlayer);

            if (a == 0 && !getPair)
            {
                getScript.direction = playerTrans.forward;
            }
            else if (a < nbrMidle + addNumb + addOnPair)
            {
                getScript.direction = (playerTrans.forward * (75 - a * getAngle) + playerTrans.right * a * getAngle).normalized;
            }
            else
            {
                getScript.direction = (playerTrans.forward * (75 - (a - nbrMidle) * getAngle) - playerTrans.right * (a - nbrMidle) * getAngle).normalized;
            }
        }

        waitNewShoot ();
    }

    void waitNewShoot ()
    {
        if (FireRate > 0)
        {
            DOVirtual.DelayedCall (FireRate, () =>
            {
                canShoot = true;
            });
        }
        else
        {
            canShoot = true;
        }
    }

    void OnTriggerEnter (Collider thisColl)
    {
        string tag = thisColl.tag;
        if (tag == Constants._Player && OnFloor)
        {
            PlayerController getPlayer = thisColl.GetComponent<PlayerController> ();
            getPlayer.WeaponCatch++;
            Manager.GameCont.WeaponB.NewWeapon (getPlayer, gameObject);
        }
    }
    #endregion
}