using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using Rewired;

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    bool canPress;

    public static MenuManager Singleton;

    [Header ("CANVAS")]
    public CanvasGroup canvasSelect;
    public CanvasGroup canvasMenu;

    bool player1Ready, player2Ready, player3Ready, player4Ready;

    public Image backgroundFlash;

    [Header ("PLAYER MODELS")]
    public GameObject [] PlayersMesh;
    public float [] PlayersRotate;
    public GameObject Cauldron;
    public GameObject CamCinemachine;

    public PlayableDirector timelineDirector;
    public MenuButton firstItemMenu;
    public MenuButton creditsItem;

    public CanvasGroup Credits;
    public CanvasGroup PressStart;
    public CanvasGroup HowToPlay;

    int PlayersReady;

    bool CreditsOpened;

    NbrPlayerPlaying thisNPP;
    Player player1, player2, player3, player4;

    private void Awake ()
    {
        try
        {
            thisNPP = GameObject.Find ("NbrPlayer").GetComponent<NbrPlayerPlaying> ();
            thisNPP.NbrPlayer = new List<infoP> ();

            thisNPP.NbrPlayer.Add (new infoP ());
            thisNPP.NbrPlayer.Add (new infoP ());
            thisNPP.NbrPlayer.Add (new infoP ());
            thisNPP.NbrPlayer.Add (new infoP ());

            thisNPP.NbrPlayer [0].ID = 0;
            thisNPP.NbrPlayer [1].ID = 1;
            thisNPP.NbrPlayer [2].ID = 2;
            thisNPP.NbrPlayer [3].ID = 3;
        }
        catch
        {

        }

        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy (this);
        }

    }

    // Use this for initialization
    void Start ()
    {

        player1 = ReInput.players.GetPlayer (0);
        player2 = ReInput.players.GetPlayer (1);
        player3 = ReInput.players.GetPlayer (2);
        player4 = ReInput.players.GetPlayer (3);

        DOVirtual.DelayedCall (3, () =>
        {
            if (firstItemMenu != null)
                firstItemMenu.Select ();
        });

        Cursor.visible = false;
    }

    public void PlayReady ()
    {
        canvasSelect.DOFade (1, .25f);
        canvasMenu.DOFade (0, .1f);
        Debug.Log ("playready");

        canvasSelect.transform.GetChild (0).transform.DOScale (3, 0);
        canvasSelect.transform.GetChild (0).transform.DOScale (1, .2f);
        canvasSelect.transform.GetChild (0).GetComponent<CanvasGroup> ().DOFade (1, .2f);

        DOVirtual.DelayedCall (.2f, () =>
        {
            canvasSelect.transform.GetChild (1).transform.DOScale (3, 0);
            canvasSelect.transform.GetChild (1).transform.DOScale (1, .2f);
            canvasSelect.transform.GetChild (1).GetComponent<CanvasGroup> ().DOFade (1, .2f);
        });

        DOVirtual.DelayedCall (.4f, () =>
        {
            canvasSelect.transform.GetChild (2).transform.DOScale (3, 0);
            canvasSelect.transform.GetChild (2).transform.DOScale (1, .2f);
            canvasSelect.transform.GetChild (2).GetComponent<CanvasGroup> ().DOFade (1, .2f);
        });

        DOVirtual.DelayedCall (.6f, () =>
        {
            canvasSelect.transform.GetChild (3).transform.DOScale (3, 0);
            canvasSelect.transform.GetChild (3).transform.DOScale (1, .2f);
            canvasSelect.transform.GetChild (3).GetComponent<CanvasGroup> ().DOFade (1, .2f);
        });

        DOVirtual.DelayedCall (1, () =>
        {

            canPress = true;

        });

    }

    public void PlayStart ()
    {
        canPress = false;

        for (int i = 0; i < 4; i++)
        {
            PlayersMesh [i].transform.DOLocalRotate (new Vector3 (0, PlayersRotate [i], 0), .4f, RotateMode.LocalAxisAdd).SetEase (Ease.InCirc);
        }

        Cauldron.GetComponentInChildren<Animator> ().SetTrigger ("More");

        DOVirtual.DelayedCall (1, () =>
        {

            PlayersMesh [4].SetActive (true);

            ScreenShake (1);

            DOVirtual.DelayedCall (.5f, () =>
            {

                PlayersMesh [5].SetActive (true);
                ScreenShake (2);

                DOVirtual.DelayedCall (.4f, () =>
                {

                    PlayersMesh [6].SetActive (true);
                    ScreenShake (3);

                    DOVirtual.DelayedCall (.3f, () =>
                    {

                        PlayersMesh [7].SetActive (true);
                        ScreenShake (4);

                        DOVirtual.DelayedCall (.3f, () =>
                        {

                            HowToPlay.DOFade (1, 1f);

                            backgroundFlash.GetComponent<CanvasGroup> ().DOFade (1, 1f).OnComplete (() =>
                            {

                                StartCoroutine (LoadLevel (true));

                            });

                        });
                    });
                });
            });
        });

    }

    public IEnumerator LoadLevel (bool menu)
    {
        AsyncOperation opLevel = SceneManager.LoadSceneAsync ("FINAL", LoadSceneMode.Single);

        opLevel.allowSceneActivation = false;
        while (opLevel.progress < .9f)
        {
            yield return null;
        }

        // NbrPlayerPlaying.NbrPP.transform.GetChild (0).gameObject.SetActive (false);
        if (menu)
        {
            DOVirtual.DelayedCall (12, () =>
            {

                HowToPlay.DOFade (0, .25f);

                backgroundFlash.DOFade (0, .25f).OnComplete (() =>
                {

                    opLevel.allowSceneActivation = true;
                });
            });
        }
        else
        {
            DOVirtual.DelayedCall (2, () =>
            {

                SceneManager.LoadSceneAsync ("FINAL", LoadSceneMode.Single);

                opLevel.allowSceneActivation = true;
            });

        }

    }

    public IEnumerator LoadMenu ()
    {

        AsyncOperation opMenu = SceneManager.LoadSceneAsync ("Menu_Enviro", LoadSceneMode.Single);

        opMenu.allowSceneActivation = false;

        while (opMenu.progress < .9f)
        {
            yield return null;
        }

        DOVirtual.DelayedCall (2, () =>
        {

            SceneManager.UnloadSceneAsync ("Alex");

            opMenu.allowSceneActivation = true;

        });

    }

    void ScreenShake (int number)
    {

        Transform cam = Camera.main.transform;
        Destroy (CamCinemachine.gameObject);

        if (number == 1)
        {
            float rdmX = UnityEngine.Random.Range (-.5f, .5f);
            float rdmZ = UnityEngine.Random.Range (-.5f, .5f);
            cam.transform.DOPunchPosition (new Vector3 (rdmX, 0, rdmZ), .5f, 2, 1);
            cam.transform.DOPunchRotation (new Vector3 (0, 0, rdmZ), .5f, 2, 1);
        }

        if (number == 2)
        {
            float rdmX = UnityEngine.Random.Range (-1, 1);
            float rdmZ = UnityEngine.Random.Range (-1, 1);
            cam.transform.DOPunchPosition (new Vector3 (rdmX, 0, rdmX), .35f, 2, 1);
            cam.transform.DOPunchRotation (new Vector3 (0, 0, rdmZ), .35f, 2, 1);
        }

        if (number == 3)
        {
            float rdmX = UnityEngine.Random.Range (-2, 2);
            float rdmZ = UnityEngine.Random.Range (-2, 2);
            cam.transform.DOPunchPosition (new Vector3 (rdmX, 0, rdmX), .25f, 2, 1);
            cam.transform.DOPunchRotation (new Vector3 (0, 0, rdmZ), .25f, 2, 1);
        }

        if (number == 4)
        {
            transform.DOKill (true);
            Camera.main.transform.DORotate (Vector3.zero, 1f).SetEase (Ease.InOutExpo);
            Camera.main.transform.DOShakeRotation (2f, 2f, 15, 90);
            Camera.main.transform.DOShakePosition (2f, 2f, 15, 90);
        }

    }

    // Update is called once per frame
    void Update ()
    {

#if UNITY_EDITOR
        if (Input.GetKeyDown (KeyCode.J))
        {
            PlayStart ();
        }

        if (Input.GetKeyDown (KeyCode.K))
        {
            ScreenShake (1);
        }
#endif

        if (canPress)
        {
            if (player1.GetButtonDown ("UISubmit"))
            {

                if (!player1Ready)
                {
                    thisNPP.NbrPlayer [0].ready = true;

                    PlayersReady += 1;

                    DOVirtual.DelayedCall (.05f, () =>
                    {
                        player1Ready = true;
                    });

                    var fx = Instantiate (PlayersMesh [8].gameObject, PlayersMesh [0].transform.position, Quaternion.identity, PlayersMesh [0].transform);

                    Transform p1 = canvasSelect.transform.GetChild (0).transform;

                    p1.GetComponentsInChildren<Text> () [1].text = "READY !";
                    p1.GetComponentInChildren<Image> ().DOFade (0, .05f);

                    PlayersMesh [0].gameObject.SetActive (true);
                }
                else
                {

                    PlayersReady -= 1;
                    thisNPP.NbrPlayer [0].ready = false;

                    DOVirtual.DelayedCall (.05f, () =>
                    {
                        player1Ready = false;
                    });

                    Transform p1 = canvasSelect.transform.GetChild (0).transform;

                    p1.GetComponentsInChildren<Text> () [1].text = "GET READY \n PRESS";
                    p1.GetComponentInChildren<Image> ().DOFade (1, .05f);

                    var fx = Instantiate (PlayersMesh [8].gameObject, PlayersMesh [0].transform.position, Quaternion.identity, PlayersMesh [0].transform);

                    PlayersMesh [0].gameObject.SetActive (false);

                }
            }

            if (player2.GetButtonDown ("UISubmit"))
            {
                if (!player2Ready)
                {

                    PlayersReady += 1;
                    thisNPP.NbrPlayer [3].ready = true;

                    DOVirtual.DelayedCall (.05f, () =>
                    {
                        player2Ready = true;
                    });

                    Transform p2 = canvasSelect.transform.GetChild (1).transform;

                    p2.GetComponentsInChildren<Text> () [1].text = "READY !";
                    p2.GetComponentInChildren<Image> ().DOFade (0, .05f);

                    var fx = Instantiate (PlayersMesh [8].gameObject, PlayersMesh [1].transform.position, Quaternion.identity, PlayersMesh [1].transform);

                    PlayersMesh [1].gameObject.SetActive (true);
                }
                else
                {

                    PlayersReady -= 1;
                    thisNPP.NbrPlayer [1].ready = false;

                    DOVirtual.DelayedCall (.05f, () =>
                    {
                        player2Ready = false;
                    });

                    Transform p2 = canvasSelect.transform.GetChild (1).transform;

                    p2.GetComponentsInChildren<Text> () [1].text = "GET READY \n PRESS";
                    p2.GetComponentInChildren<Image> ().DOFade (1, .05f);

                    var fx = Instantiate (PlayersMesh [8].gameObject, PlayersMesh [1].transform.position, Quaternion.identity, PlayersMesh [1].transform);

                    PlayersMesh [1].gameObject.SetActive (false);

                }
            }

            if (player3.GetButtonDown ("UISubmit"))
            {
                if (!player3Ready)
                {

                    PlayersReady += 1;
                    thisNPP.NbrPlayer [2].ready = true;

                    DOVirtual.DelayedCall (.05f, () =>
                    {
                        player3Ready = true;
                    });

                    Transform p3 = canvasSelect.transform.GetChild (2).transform;

                    p3.GetComponentsInChildren<Text> () [1].text = "READY !";
                    p3.GetComponentInChildren<Image> ().DOFade (0, .05f);

                    var fx = Instantiate (PlayersMesh [8].gameObject, PlayersMesh [2].transform.position, Quaternion.identity, PlayersMesh [2].transform);

                    PlayersMesh [2].gameObject.SetActive (true);
                }
                else
                {
                    PlayersReady -= 1;
                    thisNPP.NbrPlayer [2].ready = false;

                    DOVirtual.DelayedCall (.05f, () =>
                    {
                        player3Ready = false;
                    });

                    Transform p3 = canvasSelect.transform.GetChild (2).transform;

                    p3.GetComponentsInChildren<Text> () [1].text = "GET READY \n PRESS";
                    p3.GetComponentInChildren<Image> ().DOFade (1, .05f);

                    var fx = Instantiate (PlayersMesh [8].gameObject, PlayersMesh [2].transform.position, Quaternion.identity, PlayersMesh [2].transform);

                    PlayersMesh [2].gameObject.SetActive (false);
                }
            }

            if (player4.GetButtonDown ("UISubmit"))
            {
                if (!player4Ready)
                {

                    PlayersReady += 1;
                    thisNPP.NbrPlayer [3].ready = true;

                    DOVirtual.DelayedCall (.05f, () =>
                    {
                        player4Ready = true;
                    });

                    Transform p4 = canvasSelect.transform.GetChild (3).transform;

                    p4.GetComponentsInChildren<Text> () [1].text = "READY !";
                    p4.GetComponentInChildren<Image> ().DOFade (0, .05f);

                    var fx = Instantiate (PlayersMesh [8].gameObject, PlayersMesh [3].transform.position, Quaternion.identity, PlayersMesh [3].transform);

                    PlayersMesh [3].gameObject.SetActive (true);
                }
                else
                {
                    PlayersReady -= 1;
                    thisNPP.NbrPlayer [3].ready = false;

                    DOVirtual.DelayedCall (.05f, () =>
                    {
                        player4Ready = false;
                    });

                    Transform p4 = canvasSelect.transform.GetChild (3).transform;

                    p4.GetComponentsInChildren<Text> () [1].text = "GET READY \n PRESS";
                    p4.GetComponentInChildren<Image> ().DOFade (1, .05f);

                    var fx = Instantiate (PlayersMesh [8].gameObject, PlayersMesh [3].transform.position, Quaternion.identity, PlayersMesh [3].transform);

                    PlayersMesh [3].gameObject.SetActive (false);
                }
            }

            if (PlayersReady >= 2)
            {
                PressStart.DOFade (1, .1f);

                if (player1.GetButtonDown ("Start") || player2.GetButtonDown ("Start") || player3.GetButtonDown ("Start") || player4.GetButtonDown ("Start"))
                {
                    PlayStart ();

                }
            }
            else if (PlayersReady <= 1)
            {
                PressStart.DOFade (0, .1f);
            }

        }
    }
}