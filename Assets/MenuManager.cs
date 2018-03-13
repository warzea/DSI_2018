using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Rewired;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    bool canPress;

    public static MenuManager Singleton;

    [Header("CANVAS")]
    public CanvasGroup canvasSelect;
    public CanvasGroup canvasMenu;
    

    bool player1Ready, player2Ready, player3Ready, player4Ready;

    public Image backgroundFlash;

    [Header("PLAYER MODELS")]
    public GameObject[] PlayersMesh;
    public float[] PlayersRotate;
    public GameObject Cauldron;
    public GameObject CamCinemachine;

    public PlayableDirector timelineDirector;
    public MenuButton firstItemMenu;

    Player player1, player2, player3, player4;


    private void Awake()
    {

        if(Singleton == null)
        {
            Singleton = this;
        } else
        {
            Destroy(this);
        }

    }

    // Use this for initialization
    void Start()
    {

        player1 = ReInput.players.GetPlayer(0);
        player2 = ReInput.players.GetPlayer(1);
        player3 = ReInput.players.GetPlayer(2);
        player4 = ReInput.players.GetPlayer(3);

        DOVirtual.DelayedCall(3, () => {
            firstItemMenu.Select();
        });
    }

    public void PlayReady()
    {
        canvasSelect.DOFade(1, .25f);
        canvasMenu.DOFade(0, .1f);
        Debug.Log("playready");


        canvasSelect.transform.GetChild(0).transform.DOScale(3, 0);
        canvasSelect.transform.GetChild(0).transform.DOScale(1, .2f);
        canvasSelect.transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(1, .2f);

        DOVirtual.DelayedCall(.2f, () => {
            canvasSelect.transform.GetChild(1).transform.DOScale(3, 0);
            canvasSelect.transform.GetChild(1).transform.DOScale(1, .2f);
            canvasSelect.transform.GetChild(1).GetComponent<CanvasGroup>().DOFade(1, .2f);
        });


        DOVirtual.DelayedCall(.4f, () => {
            canvasSelect.transform.GetChild(2).transform.DOScale(3, 0);
            canvasSelect.transform.GetChild(2).transform.DOScale(1, .2f);
            canvasSelect.transform.GetChild(2).GetComponent<CanvasGroup>().DOFade(1, .2f);
        });


        DOVirtual.DelayedCall(.6f, () => {
            canvasSelect.transform.GetChild(3).transform.DOScale(3, 0);
            canvasSelect.transform.GetChild(3).transform.DOScale(1, .2f);
            canvasSelect.transform.GetChild(3).GetComponent<CanvasGroup>().DOFade(1, .2f);
        });

        DOVirtual.DelayedCall(1, () => {

            canPress = true;

        });

    }

    public void PlayStart()
    {
        canPress = false;

        for (int i = 0; i < 4; i++)
        {
            PlayersMesh[i].transform.DOLocalRotate(new Vector3(0, PlayersRotate[i], 0), .4f, RotateMode.LocalAxisAdd).SetEase(Ease.InCirc);
        }

        Cauldron.GetComponentInChildren<Animator>().SetTrigger("More");

        DOVirtual.DelayedCall(1, () => {

            PlayersMesh[4].SetActive(true);

            ScreenShake(1);

            DOVirtual.DelayedCall(.5f, () => {

                PlayersMesh[5].SetActive(true);
                ScreenShake(2);

                DOVirtual.DelayedCall(.4f, () => {

                    PlayersMesh[6].SetActive(true);
                    ScreenShake(3);

                    DOVirtual.DelayedCall(.3f, () => {

                        PlayersMesh[7].SetActive(true);
                        ScreenShake(4);

                        DOVirtual.DelayedCall(.3f, () => {

                            backgroundFlash.DOFade(1, 1f).OnComplete(() => {

                                StartCoroutine("LoadLevel");

                            });

                        });
                    });
                });
            });
        });
        
    }

    IEnumerator LoadLevel()
    {
        AsyncOperation opLevel = SceneManager.LoadSceneAsync("Alex", LoadSceneMode.Additive);

        opLevel.allowSceneActivation = false;

        while (opLevel.progress < .9f)
        {
            yield return null;
        }

        DOVirtual.DelayedCall(4, () => {

            backgroundFlash.DOFade(0, .25f).OnComplete(() => {


                opLevel.allowSceneActivation = true;
            });
        });
    }

    void ScreenShake(int number)
    {

        Transform cam = Camera.main.transform;
        Destroy(CamCinemachine.gameObject);

        if (number == 1)
        {
            float rdmX = UnityEngine.Random.Range(-.5f, .5f);
            float rdmZ = UnityEngine.Random.Range(-.5f, .5f);
            cam.transform.DOPunchPosition(new Vector3(rdmX, 0, rdmZ), .5f, 2, 1);
            cam.transform.DOPunchRotation(new Vector3(0, 0, rdmZ), .5f, 2, 1);
        }

        if(number == 2)
        {
            float rdmX = UnityEngine.Random.Range(-1, 1);
            float rdmZ = UnityEngine.Random.Range(-1, 1);
            cam.transform.DOPunchPosition(new Vector3(rdmX, 0, rdmX), .35f, 2, 1);
            cam.transform.DOPunchRotation(new Vector3(0, 0, rdmZ), .35f, 2, 1);
        }

        if (number == 3)
        {
            float rdmX = UnityEngine.Random.Range(-2, 2);
            float rdmZ = UnityEngine.Random.Range(-2, 2);
            cam.transform.DOPunchPosition(new Vector3(rdmX, 0, rdmX), .25f, 2, 1);
            cam.transform.DOPunchRotation(new Vector3(0, 0, rdmZ), .25f, 2, 1);
        }

        if (number == 4)
        {
            transform.DOKill(true);
            Camera.main.transform.DORotate(Vector3.zero, 1f).SetEase(Ease.InOutExpo);
            Camera.main.transform.DOShakeRotation(2f, 2f, 15, 90);
            Camera.main.transform.DOShakePosition(2f, 2f, 15, 90);
        }

    }

	// Update is called once per frame
	void Update () {

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayStart();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            ScreenShake(1);
        }
#endif

        if (canPress)
        {
            if(player1.GetButtonDown("UISubmit"))
            {
                if (!player1Ready)
                {
                    DOVirtual.DelayedCall(.05f, () =>
                    {
                        player1Ready = true;
                    });

                    Transform p1 = canvasSelect.transform.GetChild(0).transform;

                    p1.GetComponentsInChildren<Text>()[1].text = "READY !";
                    p1.GetComponentInChildren<Image>().DOFade(0, .05f);
                    
                    PlayersMesh[0].gameObject.SetActive(true);
                }
                else
                {
                    DOVirtual.DelayedCall(.05f, () => {
                        player1Ready = false;
                    });

                    Transform p1 = canvasSelect.transform.GetChild(0).transform;

                    p1.GetComponentsInChildren<Text>()[1].text = "GET READY \n PRESS";
                    p1.GetComponentInChildren<Image>().DOFade(1, .05f);

                    PlayersMesh[0].gameObject.SetActive(false);

                }
            }

            if (player2.GetButtonDown("UISubmit"))
            {
                if (!player2Ready)
                {
                    DOVirtual.DelayedCall(.05f, () =>
                    {
                        player2Ready = true;
                    });

                    Transform p2 = canvasSelect.transform.GetChild(0).transform;

                    p2.GetComponentsInChildren<Text>()[1].text = "READY !";
                    p2.GetComponentInChildren<Image>().DOFade(0, .05f);

                    PlayersMesh[0].gameObject.SetActive(true);
                }
                else
                {
                    DOVirtual.DelayedCall(.05f, () => {
                        player2Ready = false;
                    });

                    Transform p2 = canvasSelect.transform.GetChild(0).transform;

                    p2.GetComponentsInChildren<Text>()[1].text = "GET READY \n PRESS";
                    p2.GetComponentInChildren<Image>().DOFade(1, .05f);

                    PlayersMesh[1].gameObject.SetActive(false);

                }
            }

            if (player3.GetButtonDown("UISubmit"))
            {
                if (!player3Ready)
                {
                    DOVirtual.DelayedCall(.05f, () =>
                    {
                        player3Ready = true;
                    });

                    Transform p3 = canvasSelect.transform.GetChild(0).transform;

                    p3.GetComponentsInChildren<Text>()[1].text = "READY !";
                    p3.GetComponentInChildren<Image>().DOFade(0, .05f);

                    PlayersMesh[2].gameObject.SetActive(true);
                }
                else
                {
                    DOVirtual.DelayedCall(.05f, () => {
                        player3Ready = false;
                    });

                    Transform p3 = canvasSelect.transform.GetChild(0).transform;

                    p3.GetComponentsInChildren<Text>()[1].text = "GET READY \n PRESS";
                    p3.GetComponentInChildren<Image>().DOFade(1, .05f);

                    PlayersMesh[3].gameObject.SetActive(false);
                }
            }

            if (player4.GetButtonDown("UISubmit"))
            {
                if (!player4Ready)
                {
                    DOVirtual.DelayedCall(.05f, () =>
                    {
                        player4Ready = true;
                    });

                    Transform p4 = canvasSelect.transform.GetChild(0).transform;

                    p4.GetComponentsInChildren<Text>()[1].text = "READY !";
                    p4.GetComponentInChildren<Image>().DOFade(0, .05f);

                    PlayersMesh[3].gameObject.SetActive(true);
                }
                else
                {
                    DOVirtual.DelayedCall(.05f, () => {
                        player4Ready = false;
                    });

                    Transform p4 = canvasSelect.transform.GetChild(0).transform;

                    p4.GetComponentsInChildren<Text>()[1].text = "GET READY \n PRESS";
                    p4.GetComponentInChildren<Image>().DOFade(1, .05f);

                    PlayersMesh[3].gameObject.SetActive(false);
                }
            }

            if (player1Ready && player2Ready && player3Ready && player4Ready)
            {
                if (player1.GetButtonDown("UIStart") || player2.GetButtonDown("UIStart") || player3.GetButtonDown("UIStart") || player4.GetButtonDown("UIStart"))
                {
                    PlayReady();
                }
            }


        }
	}
}
