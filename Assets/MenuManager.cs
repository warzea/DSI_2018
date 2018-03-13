using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Rewired;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class MenuManager : MonoBehaviour {

    bool canPress;

    public static MenuManager Singleton;

    [Header("CANVAS")]
    public CanvasGroup canvasSelect;
    public CanvasGroup canvasMenu;

    public GameObject playersReady;

    bool player1Ready, player2Ready, player3Ready, player4Ready;

    [Header("PLAYER MODELS")]
    public GameObject[] PlayersMesh;
    public float[] PlayersRotate;
    public GameObject Cauldron;

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
        
    }

	
	// Update is called once per frame
	void Update () {

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayStart();
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
                    p1.transform.GetChild(0).GetComponentInChildren<Image>().DOFade(0, .05f);
                    
                    PlayersMesh[0].gameObject.SetActive(true);
                    
                    /*
                    float randomZrotate = UnityEngine.Random.Range(-17, 17);
                    canvasSelect.transform.GetChild(0).transform.DOPunchRotation(new Vector3(1, 1, randomZrotate), 0.2f, 10, 1);

                    p1.DOPunchScale((Vector3.one * .45f), 0.2f, 20, .1f);
                    p1.transform.GetComponent<CanvasGroup>().DOFade(0, .15f);*/

                }
                else
                {

                    DOVirtual.DelayedCall(.05f, () => {
                        player1Ready = false;
                    });

                    canvasSelect.transform.GetChild(0).GetComponentsInChildren<Text>()[1].text = "GET READY \n PRESS";
                    canvasSelect.transform.GetChild(0).GetComponentInChildren<Image>().DOFade(1, .05f);


                    PlayersMesh[0].gameObject.SetActive(false);

                }
            }


        }
	}
}
