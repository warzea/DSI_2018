using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using Rewired;

using UnityEngine;

public class GameController : ManagerParent
{
    #region Variables
    public GameObject PlayerPrefab;
    public GameObject StartWeapon;
    public Transform PlayerPosSpawn;

    public Camera MainCam;
    public WeaponBox WeaponB;
    public float TimerCheckPlayer = 1;

    public CameraFollow GetCameraFollow;

    [HideInInspector]
    public Transform Garbage;

    [HideInInspector]
    public PlayerInfoInput [] GetPlayersInput;

    [HideInInspector]
    public List<GameObject> Players;
    public Material [] PlayerMaterial;
    public GameObject [] LaserFX;
    public GameObject [] PlayerTrail;
    public AbstractMedal [] AllMedal;

    [HideInInspector]
    public List<MedalsPlayer> MedalInfo;
    List<PlayerController> getPlayerCont;

    public int NbrPlayer = 0;
    #endregion

    #region Mono
    #endregion

    #region Public Methods
    public void StartGame ()
    {
        Manager.Audm.OpenAudio (AudioType.MusicBackGround, "gameplay", true);
        if (WeaponB == null)
        {
            WeaponB = (WeaponBox) FindObjectOfType (typeof (WeaponBox));
        }

        SpawnPlayer ();

        GetCameraFollow.InitGame ();
        checkPlayer ();
    }

    public void EndGame ()
    {
        Manager.Audm.OpenAudio (AudioType.MusicBackGround, "score", true);
        var newEtat = new AgentEvent ();
        newEtat.AgentChecking = true;
        newEtat.Raise ();

        Players.Clear ();
        Manager.Ui.EndScreenStart ();

        DOVirtual.DelayedCall (2, () =>
        {

            ScoreInfo [] allSc = Manager.Ui.GetScores.AllScore.ToArray ();
            ScoreInfo thisScore = allSc [0];

            for (int a = 0; a < allSc.Length; a++)
            {
                if (allSc [a].ScoreTpe == ScoreType.BoxWeapon)
                {
                    thisScore = allSc [a];
                    break;
                }
            }

            DOVirtual.DelayedCall (1, () =>
            {
                for (int a = 0; a < AllMedal.Length; a++)
                {
                    AllMedal [a].gameObject.SetActive (true);

                    AllMedal [a].StartCheck (getPlayerCont.ToArray ());
                }

                int getVal;
                MedalsPlayer thisMP;
                for (int a = 0; a < MedalInfo.Count; a++)
                {
                    thisMP = MedalInfo [a];
                    while (thisMP.ThisMedal.Count > 3)
                    {
                        getVal = Random.Range (0, thisMP.ThisMedal.Count);
                        Destroy (thisMP.ThisMedal [getVal].gameObject);

                        thisMP.ThisMedal.RemoveAt (getVal);
                    }

                    for (int b = 0; b < 3; b++)
                    {
                        Manager.Ui.EndScreenMedals (thisMP.ThisMedal [b].transform, thisMP.IDPlayer, b);
                    }
                }
            });

            Manager.Ui.GetScores.UpdateValue (thisScore.FinalScore, ScoreType.EndScore, false, Manager.Ui.EndScreenFinished);

        });
    }

    public void EtatAgent (bool thisEtat)
    {

        /*System.Action <AgentEvent> thisAct = delegate( AgentEvent thisEvnt )
        {
            thisEvnt.AgentChecking = thisEtat;
        };

        Manager.Event.Raise(thisAct);*/
    }

    #endregion

    #region Private Methods
    protected override void InitializeManager ()
    {
        MedalInfo = new List<MedalsPlayer> ();
        for (int a = 0; a < 4; a++)
        {
            MedalInfo.Add (new MedalsPlayer ());
            MedalInfo [a].IDPlayer = a;
            MedalInfo [a].ThisMedal = new List<AbstractMedal> ();
        }
        getPlayerCont = new List<PlayerController> ();
        Garbage = transform.Find ("Garbage");
        GetPlayersInput = new PlayerInfoInput [4];

        for (int a = 0; a < 4; a++)
        {
            GetPlayersInput [a] = new PlayerInfoInput ();
            GetPlayersInput [a].IdPlayer = a;
            GetPlayersInput [a].InputPlayer = ReInput.players.GetPlayer (a);
            GetPlayersInput [a].EnablePlayer = false;
        }

        if (WeaponB == null)
        {
            WeaponB = (WeaponBox) FindObjectOfType (typeof (WeaponBox));
        }

        if (StartWeapon == null)
        {
            StartWeapon = WeaponB.AllStartWeap [0];
        }

        if (MainCam == null)
        {
            MainCam = Camera.main;
            GetCameraFollow = MainCam.transform.parent.GetComponent<CameraFollow> ();
        }
    }

    void SpawnPlayer ()
    {
        PlayerInfoInput [] getPlayers = GetPlayersInput;
        PlayerController getPC;
        GameObject getPlayer;
        GameObject getWeapon;
        GameObject [] getPlayerHud = Manager.Ui.PlayersHUD;

        List<GameObject> getPotGets = new List<GameObject> ();

        for (int a = 0; a < getPlayers.Length; a++)
        {
            getPlayers [a].ReadyPlayer = false;
            getPlayerHud [a].SetActive (getPlayers [a].EnablePlayer);

            getPlayer = (GameObject) Instantiate (PlayerPrefab);
            getPlayer.name = getPlayer.name + "+" + a.ToString ();
            if (!getPlayers [a].EnablePlayer)
            {
                getPlayer.SetActive (false);
            }
            else
            {
                NbrPlayer++;
            }
            /* getPlayer.transform.SetParent (PlayerPosSpawn);
             getPlayer.transform.localPosition = Vector3.zero;
             getPlayer.transform.SetParent (null);*/

            //getPlayer.transform.localPosition += Vector3.up * 0.5f;
            if (PlayerPosSpawn != null)
            {
                getPlayer.transform.localPosition = PlayerPosSpawn.position + new Vector3 (a * 1.5f, 0, 0);
            }
            else
            {
                getPlayer.transform.position = new Vector3 (a * 1.5f, 0, 0);
            }

            foreach (Renderer thisMat in getPlayer.GetComponentsInChildren<Renderer> ())
            {
                if (thisMat.gameObject.name == "Corpus")
                {
                    thisMat.material = PlayerMaterial [a];
                    Instantiate (PlayerTrail [a], getPlayer.transform.position, Quaternion.identity, getPlayer.transform);
                    break;
                }
            }

            Instantiate (LaserFX [a], new Vector3 (getPlayer.transform.position.x + 0.3f, getPlayer.transform.position.y, getPlayer.transform.position.z), Quaternion.identity, getPlayer.transform);

            getPC = getPlayer.GetComponent<PlayerController> ();
            getPC.IdPlayer = getPlayers [a].IdPlayer;
            getPC.AmmoUI = Manager.Ui.PlayersAmmo [a].transform;

            getWeapon = (GameObject) Instantiate (Manager.Ui.PlayerText [a], Manager.Ui.GetInGame);
            getWeapon.GetComponent<FollowPlayerUI> ().getCam = MainCam;
            getWeapon.GetComponent<FollowPlayerUI> ().ThisPlayer = getPlayer.transform;

            getWeapon = (GameObject) Instantiate (Manager.Ui.PotionGet, Manager.Ui.GetInGame);
            getWeapon.GetComponent<PotionFollowP> ().getCam = MainCam;
            getWeapon.GetComponent<PotionFollowP> ().ThisPlayer = getPlayer.transform;
            getPotGets.Add (getWeapon);

            getWeapon = (GameObject) Instantiate (StartWeapon, getPC.WeaponPos.transform);
            getWeapon.transform.localPosition = Vector3.zero;
            //getWeapon.transform.localRotation = Quaternion.identity;

            //getPlayer.GetComponent<PlayerController>().WeapText = Manager.Ui.textWeapon[a];

            getPC.UpdateWeapon (getWeapon.GetComponent<WeaponAbstract> ());
            Players.Add (getPlayer);
            getPlayerCont.Add (getPlayer.GetComponent<PlayerController> ());
        }

        Manager.Ui.AllPotGet = getPotGets.ToArray ();
        Manager.AgentM.player = Players.ToArray ();
        Manager.AgentM.InitGame ();
    }

    void checkPlayer ()
    {
        PlayerController [] playerCont = getPlayerCont.ToArray ();
        GameObject lowLife = Players [0];
        GameObject maxLife = Players [0];
        GameObject boxWeapon = Players [0];

        bool check = false;

        int getID = 0;
        int a;

        for (a = 0; a < playerCont.Length; a++)
        {
            if (playerCont [a].LifePlayer < playerCont [getID].LifePlayer && !playerCont [a].dead)
            {
                getID = a;
            }
            else if (playerCont [a].LifePlayer == playerCont [getID].LifePlayer && !playerCont [a].dead)
            {
                if (Random.Range (0, 2) == 0)
                {
                    getID = a;
                }
            }
        }

        if (playerCont [getID].dead)
        {
            check = false;
            for (a = 0; a < playerCont.Length; a++)
            {
                if (!playerCont [a].dead)
                {
                    getID = a;
                    check = true;
                    break;
                }
            }

            if (!check)
            {
                DOVirtual.DelayedCall (TimerCheckPlayer, () =>
                {
                    checkPlayer ();
                });
                return;
            }
        }

        lowLife = playerCont [getID].gameObject;

        for (a = 0; a < playerCont.Length; a++)
        {
            if (playerCont [a].LifePlayer > playerCont [getID].LifePlayer)
            {
                getID = a;
            }
            else if (playerCont [a].LifePlayer == playerCont [getID].LifePlayer)
            {
                if (Random.Range (0, 2) == 0)
                {
                    getID = a;
                }
            }
        }

        maxLife = playerCont [getID].gameObject;

        check = false;
        for (a = 0; a < playerCont.Length; a++)
        {
            if (playerCont [a].driveBox)
            {
                boxWeapon = playerCont [a].gameObject;
                check = true;
                break;
            }
        }

        if (!check)
        {
            boxWeapon = playerCont [Random.Range (0, playerCont.Length)].gameObject;
        }

        Manager.AgentM.ChangeEtatFocus (lowLife, maxLife, boxWeapon);

        DOVirtual.DelayedCall (TimerCheckPlayer, () =>
        {
            checkPlayer ();
        });
    }
    #endregion
}

public class MedalsPlayer
{
    public List<AbstractMedal> ThisMedal;
    public int IDPlayer;
}