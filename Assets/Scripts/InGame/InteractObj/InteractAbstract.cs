using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

public class InteractAbstract : MonoBehaviour
{
    #region Variables
    public string NameSongGetRes = "";
    public int NbrItem = 10;
    public int ValueOnDrop = 5;
    public int NbrDropByDrop = 2;
    public int NbrTouchToDrop = 2;
    public GameObject [] ItemDrop;
    Transform thisTrans;
    bool checkItem = true;
    int valDrop;
    Tween thisT;

    private Animator animChest;

    #endregion

    #region Mono
    void Awake ()
    {
        if (transform.childCount > 0)
        {
            thisTrans = transform.GetChild (0);
        }
        else
        {
            thisTrans = transform;
        }

        valDrop = ValueOnDrop;
    }

    void Start ()
    {
        animChest = transform.GetComponentInChildren<Animator> ();
        System.Action<ChestEvent> thisAct = delegate (ChestEvent thisEvnt)
        {
            Camera getCam = Manager.GameCont.MainCam;
            Vector3 getCamPos = getCam.WorldToViewportPoint (thisTrans.position);

            if (getCamPos.x > 1f || getCamPos.x < 0f || getCamPos.y > 1f || getCamPos.y < 0f)
            {

            }
            else
            {
                if (thisT != null)
                {
                    thisT.Kill ();
                }

                valDrop *= thisEvnt.Mult;
                multEffect (true);
                thisT = DOVirtual.DelayedCall (thisEvnt.TimeMult, () =>
                {
                    multEffect (false);
                });

                if (NbrItem > 0)
                {
                    foreach (Renderer thisMat in GetComponentsInChildren<Renderer> ())
                    {
                        if (thisMat.material.name == Constants._MatChest + " (Instance)")
                        {
                            DOVirtual.DelayedCall (Random.Range (0, 0.8f), () =>
                            {
                                StartCoroutine (waitRightValue (thisMat.material, "_GoldTransition", 1, 0));
                                thisMat.material.SetFloat ("_Desaturate", 0);
                            });

                        }
                    }
                }

            }

        };

        Manager.Event.Register (thisAct);
    }

    float Transition;
    IEnumerator waitRightValue (Material thisMat, string name, int maxValue, int currVal)
    {
        WaitForEndOfFrame thisF = new WaitForEndOfFrame ();
        Transition = currVal;

        DOTween.To (() => Transition, x => Transition = x, maxValue, 0.3f);

        while (Transition != maxValue)
        {
            thisMat.SetFloat (name, Transition);
            yield return thisF;
        }
    }
    #endregion

    #region Public Methods
    public void OnInteract (PlayerController thisPlayer)
    {
        if (NbrItem > 0)
        {
            NbrItem--;

            thisTrans.DOKill (true);

            thisTrans.DOShakeScale (.25f, .3f, 15, 0);

            float rdmRotZ = UnityEngine.Random.Range (-30, 30);
            float rdmPosZ = UnityEngine.Random.Range (-1, 1);

            thisTrans.DOPunchRotation (new Vector3 (0, 0, rdmRotZ), .3f, 3, 1).SetEase (Ease.InCirc);
            thisTrans.DOPunchPosition (new Vector3 (0, 1, rdmPosZ), .3f, 3, 1).SetEase (Ease.InCirc);

            thisPlayer.CurrItem += NbrDropByDrop * valDrop;

            Manager.Ui.AllPotGet [thisPlayer.IdPlayer].GetComponent<PotionFollowP> ().NewValue (thisPlayer.CurrItem);

            for (int a = 0; a < Random.Range (2, 8); a++)
            {
                DOVirtual.DelayedCall (Random.Range (0, 0.2f), () =>
                {
                    GameObject newItem = (GameObject) Instantiate (ItemDrop [Random.Range (0, ItemDrop.Length - 1)], thisPlayer.BagPos);
                    Transform getTrans = newItem.transform;
                    getTrans.position = thisTrans.position + new Vector3 (Random.Range (-0.5f, 0.51f), 0, Random.Range (-0.5f, 0.51f));

                    getTrans.DOLocalMove (Vector3.zero + Vector3.up * 3, 0.5f).OnComplete (() =>
                    {
                        getTrans.DOScale (Vector3.zero, 0.5f).OnComplete (() =>
                        {
                            newItem.SetActive (false);
                        });

                        getTrans.DOLocalMove (Vector3.zero, 0.5f);
                    });

                    if (thisPlayer.AllItem.Count < 20)
                    {
                        thisPlayer.AllItem.Add (newItem);
                    }
                    else
                    {
                        Destroy (newItem, 1.1f);
                    }
                });
            }

            if (NbrItem == 0 && checkItem)
            {
                animChest.SetTrigger ("Opening");
                checkItem = false;
                thisPlayer.NbrChest++;
                foreach (Renderer thisMat in GetComponentsInChildren<Renderer> ())
                {
                    if (thisMat.material.name == Constants._MatChest + " (Instance)")
                    {
                        StartCoroutine (waitRightValue (thisMat.material, "_Desaturate", 1, 0));
                    }
                }
            }
            else
            {
                Manager.Audm.OpenAudio (AudioType.OtherSound, NameSongGetRes);
            }
        }
    }
    #endregion

    #region Private Methods

    void multEffect (bool isEnable)
    {

    }
    #endregion

}