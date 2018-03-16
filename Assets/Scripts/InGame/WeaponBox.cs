using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

public class WeaponBox : MonoBehaviour
{
    #region Variables
    public Material BonusMat;
    public Image ThisGauge;
    public string AddResSong = "ressources_added";
    public string GiveWeapSong = "give_weapon";
    public string CauldHitSong = "cauldron_attack";
    public float SpeedAttack = 1;
    public float RangeAttack = 1;
    public float DelayAttack = 1;
    public int SpeMultRessources = 2;
    public int StayMult = 5;
    public float InvincibleTime = 1;
    Transform GetTrans;
    public int pourcLoot = 10;
    public int MinLost = 10;
    public int ItemOneGauge = 100;
    public GameObject[] AllStartWeap;
    public GameObject[] AllOtherWeap;

    public float DelayNewWeapon = 1;
    [Tooltip("Time to full fill")]
    public float TimeFullFill = 6;
    [HideInInspector]
    public float CurrTime = 0;

    [HideInInspector]
    public int NbrItem = 0;

    [HideInInspector]
    public bool CanControl = true;

    public SpriteRenderer circleEffect;
    public GameObject FXSpecial;

    List<PlayerWeapon> updateWeapon;
    List<Tween> getAllTween;
    Transform getChild;
    int nbrTotalSlide = 1;
    bool invc = false;
    bool checkAttack = false;
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.DOKill(true);
            getChild.DOKill(true);

            /*
            var fx = Instantiate(FXSpecial, GetTrans.transform.position, Quaternion.identity, GetTrans.transform);
            fx.transform.DOLocalMove(Vector3.zero, 0);
            */


            float rdmY = UnityEngine.Random.Range (-30, 30);
            float rdmZ = UnityEngine.Random.Range (-30, 30);

            Material mat = getChild.GetComponent<Renderer>().material;
            mat.DOKill(true);
            Debug.Log(mat);
            mat.DOColor(Color.red, .15f).OnComplete(() =>
          {
              mat.DOColor(Color.white, .15f);
          });

            transform.DOPunchRotation (new Vector3 (0, rdmY, rdmZ), .3f, 3, 1).SetEase (Ease.InBounce);
            getChild.transform.DOPunchPosition (new Vector3 (rdmY / 16, rdmZ / 16, 0), .3f, 3, 1).SetEase (Ease.InBounce);



        }
    }

    #region Mono
    void Awake()
    {
        getAllTween = new List<Tween>();
        updateWeapon = new List<PlayerWeapon>();
        GetTrans = transform;
        getChild = GetTrans.Find("cauldron");
        for (int a = 0; a < 4; a++)
        {
            updateWeapon.Add(new PlayerWeapon());
            updateWeapon[a].IDPlayer = a;
        }
    }
    #endregion

    #region Public Methods
    public void AttackCauld(Transform thisT)
    {
        if (!checkAttack)
        {
            GetComponent<Collider>().isTrigger = true;
            checkAttack = true;

            gameObject.tag = Constants._PlayerBullet;
            float getRange = RangeAttack;

            RaycastHit[] allHit;
            string getTag;
            Debug.DrawRay(GetTrans.position, GetTrans.forward, Color.black, 10);
            allHit = Physics.RaycastAll(GetTrans.position, thisT.forward);

            foreach (RaycastHit thisRay in allHit)
            {
                getTag = thisRay.collider.tag;

                if (getTag == Constants._Wall)
                {
                    if (thisRay.distance - 1f < getRange)
                    {
                        getRange = thisRay.distance - 1;
                    }
                }
            }

            GetTrans.DOLocalRotate (new Vector3 (0, 360, 0), SpeedAttack * 0.5f + SpeedAttack * 0.5f, RotateMode.LocalAxisAdd);
            GetTrans.DOLocalMoveZ (getRange, SpeedAttack * 0.5f).OnComplete (() =>
            {
                GetTrans.DOLocalMoveZ (0, SpeedAttack * 0.5f).OnComplete (() =>
                {
                    GetComponent<Collider> ().isTrigger = false;

                  gameObject.tag = Constants._BoxTag;
              });
          });
            DOVirtual.DelayedCall(DelayAttack + SpeedAttack, () =>
           {
               checkAttack = false;
           });
        }
    }

    public void ActionSpe()
    {
        if (ThisGauge.fillAmount >= .05f)
        {
            GetTrans.DOKill(true);
            getChild.DOKill(true);

            var fx = Instantiate(FXSpecial, GetTrans.transform.position, Quaternion.identity, GetTrans.transform);
            fx.transform.DOLocalMove(Vector3.zero, 0);

            Destroy(fx, 5);

            GetTrans.transform.DOShakeScale (1f, .7f, 20, 0);

            ThisGauge.transform.parent.GetComponentInChildren<RainbowColor>().enabled = false;
            ThisGauge.transform.parent.GetComponentInChildren<RainbowScale>().enabled = false;

            ThisGauge.transform.parent.GetComponentInChildren<RainbowColor>().transform.GetComponent<Image>().color = ThisGauge.transform.parent.GetComponentInChildren<RainbowColor>().colors[1];

            var circle = Instantiate(circleEffect, transform.position, Quaternion.identity, transform);
            circle.transform.DOLocalMove(Vector3.zero, 0);
            circle.transform.DORotate(new Vector3(-90, 0, 0), 0, RotateMode.LocalAxisAdd);
            circle.DOFade(0, 1f);
            circle.transform.DOScale(6, 1);

            Destroy(circle, 4);

            GetTrans.GetChild(1).DOShakeScale(1f, .7f, 20, 0);

            ThisGauge.fillAmount = 0;
            CurrTime = 0;

            var newMult = new ChestEvent();
            newMult.Mult = SpeMultRessources;
            newMult.TimeMult = StayMult;
            newMult.Raise();
        }
    }

    public void GetWeapon(PlayerController thisPlayer, GameObject newObj = null)
    {
        getChild.DOKill(true);
        transform.GetChild(1).DOKill(true);
        getChild.DORotate(new Vector3(0, 0, 1080), 2, RotateMode.LocalAxisAdd).SetEase(Ease.InSine);
        getChild.DOLocalMoveY(3, 2).SetEase(Ease.InSine).OnComplete(() =>
     {
         getChild.DOShakeScale(.5f, .3f, 18, 0);
         getChild.DOLocalMoveY(0.5f, .6f).SetEase(Ease.InElastic).OnComplete(() =>
         {

             getChild.DOShakeScale(1f, .4f, 18, 0);
         });
     });
    }

    public void NewWeapon(PlayerController thisPlayer, GameObject newObj = null)
    {
        bool checkNew = false;

        Manager.Audm.OpenAudio(AudioType.OtherSound, GiveWeapSong);

        if (newObj == null)
        {
            newObj = (GameObject)Instantiate(AllStartWeap[Random.Range(0, AllStartWeap.Length)], GetTrans);
        }
        else
        {
            checkNew = true;
            for (int a = 0; a < AllOtherWeap.Length; a++)
            {
                if (AllOtherWeap[a].name == newObj.name)
                {
                    List<GameObject> getWeap = new List<GameObject>(AllStartWeap);
                    getWeap.Add(AllOtherWeap[a]);
                    AllStartWeap = getWeap.ToArray();
                    break;
                }
            }
        }

        Manager.Ui.WeaponChangeIG(thisPlayer.IdPlayer);

        Transform objTrans = newObj.transform;

        Vector3 scaleWeapon = objTrans.localScale;

        objTrans.localScale = Vector3.zero;

        thisPlayer.WeaponSwitch++;
        int currId = thisPlayer.IdPlayer;

        if (updateWeapon[currId] != null)
        {
            Destroy(updateWeapon[currId].CurrObj);
        }

        updateWeapon[currId].CurrObj = newObj;

        if (checkNew)
        {
            GameObject otherWeap = (GameObject)Instantiate(newObj);
            Transform otherWTrans = otherWeap.transform;

            thisPlayer.UiAmmo.fillAmount = 1;

            if (!thisPlayer.driveBox)
            {
                Manager.Ui.WeaponNew(thisPlayer.IdPlayer);
            }
            else
            {
                thisPlayer.getWeaponBox = false;

            }

            otherWTrans.SetParent(thisPlayer.WeaponPos);
            otherWTrans.localPosition = Vector3.zero;

            thisPlayer.thisWeapon.ThrowWeap(thisPlayer.transform);

            otherWTrans.localScale = Vector3.one;
            thisPlayer.UpdateWeapon(otherWeap.GetComponent<WeaponAbstract>());
            updateWeapon[currId].CurrObj = null;
        }
        else
        {
            objTrans.position = GetTrans.position;
            //TRANSFO CANON
            getChild.DOKill(true);
            getChild.DOShakeScale(.15f, .8f, 25, 0).OnComplete(() =>
          {
              getChild.DOScaleZ(1.75f, .1f).SetEase(Ease.Linear).OnComplete(() =>
             {
                 getChild.DOScaleZ(3.5f, .1f).SetEase(Ease.Linear);

                 DOVirtual.DelayedCall(.1f, () =>
                  {
                      getChild.DOScaleZ(1.3f, .1f).SetEase(Ease.OutSine).OnComplete(() =>
                      {
                          getChild.DOShakeScale(.2f, .3f, 18, 0);
                      });
                  });
             });
          });

            DOVirtual.DelayedCall(DelayNewWeapon, () =>
           {
               thisPlayer.UiAmmo.fillAmount = 1;
               Manager.Ui.WeaponNew(thisPlayer.IdPlayer);
           });
        }

        DOVirtual.DelayedCall(DelayNewWeapon * 0.25f, () =>
       {
           objTrans.DOScale(scaleWeapon * 2, DelayNewWeapon * 0.5f).OnComplete(() =>
          {
              objTrans.DOScale(scaleWeapon, DelayNewWeapon * 0.15f);
          });

           if (!checkNew)
           {
               objTrans.DOLocalMove(Vector3.zero + Vector3.up * 8, DelayNewWeapon * 0.65f).OnComplete(() =>
              {

                  objTrans.SetParent(thisPlayer.WeaponPos);
                  //    objTrans.DOLocalRotateQuaternion(Quaternion.identity, DelayNewWeapon * 0.1f);
                  objTrans.DOLocalMove(Vector3.zero, DelayNewWeapon * 0.1f).OnComplete(() =>
                {
                    thisPlayer.UpdateWeapon(newObj.GetComponent<WeaponAbstract>());

                    updateWeapon[currId].CurrObj = null;
                });
              });
           }
           else
           {
               objTrans.SetParent(GetTrans);

               objTrans.DOLocalMove(Vector3.zero + Vector3.up * 9, 0.2f).OnComplete(() =>
              {
                  objTrans.DOLocalMove(Vector3.zero, 1);
                  objTrans.DOScale(Vector3.zero, 1.5f).OnComplete(() =>
                 {
                     Destroy(objTrans.gameObject);
                 });
              });
           }
       });
    }

    int lastNbr = 1;

    public void AddItem(int lenghtItem, bool inv = false)
    {
        if (inv)
        {
            Manager.Audm.OpenAudio(AudioType.OtherSound, CauldHitSong);
            Manager.Ui.PopPotions(PotionType.Less);

            GetTrans.DOKill(true);
            getChild.DOKill(true);

            float rdmY = UnityEngine.Random.Range(-30, 30);
            float rdmZ = UnityEngine.Random.Range(-30, 30);

            Material mat = getChild.GetComponent<Renderer>().material;
            mat.DOKill(true);
            //Debug.Log (mat);
            mat.DOColor(Color.red, .15f).OnComplete(() =>
          {
              mat.DOColor(Color.white, .15f);
          });

            GetTrans.DOPunchRotation(new Vector3(0, rdmY, rdmZ), .3f, 3, 1).SetEase(Ease.InBounce);
            GetTrans.DOPunchPosition(new Vector3(rdmY / 10, rdmZ / 10, 0), .3f, 3, 1).SetEase(Ease.InBounce);

        }
        else
        {
            Manager.Audm.OpenAudio(AudioType.OtherSound, AddResSong);
        }

        NbrItem += lenghtItem;

        int currNbr = NbrItem - (nbrTotalSlide - 1) * ItemOneGauge;
        Image[] getFeedBack = Manager.Ui.GaugeFeedback;
        float getWait = 0;
        bool checkCurr = false;

        for (int a = 0; a < getAllTween.Count; a++)
        {
            getAllTween[a].Kill(true);
        }
        getAllTween.Clear();

        Manager.Ui.GetGauge.DOKill(true);
        Manager.Ui.GetGauge.DOFillAmount((float)currNbr / ItemOneGauge, 0.5f).OnComplete(() =>
     {
         if (currNbr > (float)ItemOneGauge * 0.9f)
         {
             Manager.Ui.GetGauge.GetComponentsInChildren<RainbowColor>()[0].enabled = false;
             Manager.Ui.GetGauge.GetComponentsInChildren<RainbowColor>()[1].enabled = true;
         }
         else if (currNbr < (float)ItemOneGauge * 0.1f)
         {
             Manager.Ui.GetGauge.GetComponentsInChildren<RainbowColor>()[1].enabled = false;
             Manager.Ui.GetGauge.GetComponentsInChildren<RainbowColor>()[0].enabled = true;
         }
         else
         {
             Manager.Ui.GetGauge.GetComponentsInChildren<Image>()[1].DOColor(new Color32(1, 1, 1, 0), 0);
             Manager.Ui.GetGauge.GetComponentsInChildren<RainbowColor>()[1].enabled = false;
             Manager.Ui.GetGauge.GetComponentsInChildren<RainbowColor>()[0].enabled = false;
         }
     });

        Manager.Ui.GetScores.UpdateValue(NbrItem, ScoreType.BoxWeapon, false);
        int getCal = lastNbr;
        while (currNbr >= lastNbr * (ItemOneGauge * 0.2) && lastNbr * (ItemOneGauge * 0.2) <= ItemOneGauge)
        {
            int thisNbr = lastNbr;
            DOVirtual.DelayedCall(0.1f * (lastNbr - getCal + 0.5f), () =>
           {
               Manager.Ui.GaugeLevelGet(thisNbr - 1);
           });
            lastNbr++;
        }

        while (currNbr > ItemOneGauge)
        {
            lastNbr = 1;
            checkCurr = true;
            nbrTotalSlide++;
            Manager.Ui.MultiplierNew(nbrTotalSlide);
            currNbr -= ItemOneGauge;
        }

        if (checkCurr)
        {
            Tween getTween;
            getTween = DOVirtual.DelayedCall(0.5f, () =>
           {
               getTween = Manager.Ui.GetGauge.DOFillAmount(0, 0.5f).OnComplete(() =>
              {
                  lastNbr = 1;
                  while (currNbr >= lastNbr * (ItemOneGauge * 0.2))
                  {
                      int thisNbr = lastNbr;
                      getTween = DOVirtual.DelayedCall(0.1f * lastNbr, () =>
                      {
                          Manager.Ui.GaugeLevelGet(thisNbr - 1);
                      });
                      lastNbr++;
                  }

                  getTween = Manager.Ui.GetGauge.DOFillAmount((float)currNbr / ItemOneGauge, 0.5f).OnComplete(() =>
                {
                    if (currNbr > (float)ItemOneGauge * 0.9f)
                    {
                        Manager.Ui.GetGauge.GetComponentsInChildren<RainbowColor>()[0].enabled = false;
                        Manager.Ui.GetGauge.GetComponentsInChildren<RainbowColor>()[1].enabled = true;
                    }
                    else if (currNbr < (float)ItemOneGauge * 0.1f)
                    {
                        Manager.Ui.GetGauge.GetComponentsInChildren<RainbowColor>()[1].enabled = false;
                        Manager.Ui.GetGauge.GetComponentsInChildren<RainbowColor>()[0].enabled = true;
                    }
                    else
                    {
                        Manager.Ui.GetGauge.GetComponentsInChildren<Image>()[1].DOColor(new Color32(1, 1, 1, 0), 0);
                        Manager.Ui.GetGauge.GetComponentsInChildren<RainbowColor>()[1].enabled = false;
                        Manager.Ui.GetGauge.GetComponentsInChildren<RainbowColor>()[0].enabled = false;
                    }
                });

              });
           });
            getAllTween.Add(getTween);
        }
    }

    public void TakeHit()
    {
        if (invc)
        {
            transform.DOKill(true);
            getChild.DOKill(true);

            float rdmY = UnityEngine.Random.Range(-30, 30);
            float rdmZ = UnityEngine.Random.Range(-30, 30);

            Material mat = getChild.GetComponent<Renderer>().material;
            mat.DOKill(true);
            Debug.Log(mat);
            mat.DOColor(Color.red, .15f).OnComplete(() =>
          {
              mat.DOColor(Color.white, .15f);
          });

            transform.DOPunchRotation(new Vector3(0, rdmY, rdmZ), .3f, 3, 1).SetEase(Ease.InBounce);
            getChild.transform.DOPunchPosition(new Vector3(rdmY / 16, rdmZ / 16, 0), .3f, 3, 1).SetEase(Ease.InBounce);

            return;
        }

        invc = true;

        DOVirtual.DelayedCall(InvincibleTime, () =>
       {
           invc = false;
       });

        int calLost = (int)((NbrItem * pourcLoot) * 0.01f);

        if (calLost < MinLost)
        {
            calLost = MinLost;
        }

        if (calLost > NbrItem - (nbrTotalSlide - 1) * ItemOneGauge)
        {
            if (nbrTotalSlide > 1)
            {
                NbrItem = ItemOneGauge + (NbrItem - (nbrTotalSlide - 1) * ItemOneGauge - calLost);
                nbrTotalSlide--;
                Manager.Ui.MultiplierNew(nbrTotalSlide);
            }
        }
        else
        {
            NbrItem -= calLost;
        }

        int getMult = (int)NbrItem / ItemOneGauge + 1;
        if (getMult > nbrTotalSlide)
        {
            Manager.Ui.MultiplierNew(getMult);
            nbrTotalSlide = getMult;
        }
        else if (getMult < nbrTotalSlide)
        {
            nbrTotalSlide = getMult;
        }

        AddItem(0, true);
    }
    #endregion

    #region Private Methods
    void OnTriggerEnter(Collider thisColl)
    {

        string tag = thisColl.tag;
        if (tag == Constants._EnemyBullet)
        {
            TakeHit();
        }
    }
    #endregion

}