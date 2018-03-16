using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    /// <summary> Public <summary>

    [Header ("------------------")]
    [Header ("----INFO AGENT----")]
    [Header ("------------------")]
    public TypeEnemy ThisType;
    public GameObject myFocusPlayer;
    public int lifeAgent = 1;
    public float timeBeforeDepop = 2f;
    public Animator animAgent;

    [Header ("------------------")]
    [Header ("----INFO SHOOT----")]
    [Header ("------------------")]
    public GameObject bulletAgent;
    public Transform spawnBulletAgent;
    Transform parentBullet;
    public float SpeedBulletAgent = 10f;
    public float timeLeftAgentshoot = 2f;
    public float distanceShoot = 20;

    public CapsuleCollider mycollider;

    public enum CibleAgent
    {
        lawPlayer,
        maxPlayer,
        leadPlayer,
        cauldron,
        randomPlayer,
        nothing
    }

    ;

    public enum AgentEtat
    {
        deadAgent,
        aliveAgent
    }

    ;

    [Header ("Info Agent")]
    public CibleAgent myFocusEtatAgent;
    public AgentEtat myEtatAgent;
    private AgentsManager agentsManager;
    private NavMeshAgent navAgent;
    public bool checkUpdate = false;
    private float timeAgent = -5;

    private Camera cam;

    private float timeAttack = 2f;

    public GameObject particleHit;
    public GameObject particulWeapon;

    void Awake ()
    {
        navAgent = transform.GetComponent<NavMeshAgent> ();
        myFocusEtatAgent = CibleAgent.nothing;
        myEtatAgent = AgentEtat.aliveAgent;
    }

    void Start ()
    {
        agentsManager = Manager.AgentM;
        parentBullet = Manager.GameCont.Garbage;
        navAgent.stoppingDistance = distanceShoot;
        cam = Manager.GameCont.MainCam;

        System.Action<AgentEvent> thisAct = delegate (AgentEvent thisEvnt)
        {
            checkUpdate = thisEvnt.AgentChecking;
            navAgent.isStopped = checkUpdate;
            myEtatAgent = AgentEtat.deadAgent;
        };

        Manager.Event.Register (thisAct);

        timeAttack = Random.Range (timeLeftAgentshoot - 0.2f, timeLeftAgentshoot + 0.2f);

    }

    void Update ()
    {
        if (!checkUpdate)
        {
            return;
        }

        if (myFocusPlayer != null && !myFocusPlayer.gameObject.activeSelf)
        {
            myFocusPlayer = Manager.GameCont.Players [0];
        }

        if (myEtatAgent == AgentEtat.aliveAgent && myFocusPlayer != null)
        {

            float distance = Vector3.Distance (transform.position, myFocusPlayer.transform.position);

            if (distance > distanceShoot)
            {
                navAgent.SetDestination (myFocusPlayer.transform.position);
            }
            float velocity = navAgent.velocity.magnitude;
            if (velocity > 0.1)
            {
                animAgent.SetBool ("IsMoving", true);
            }
            else
            {
                animAgent.SetBool ("IsMoving", false);
                Vector3 lookAtPosition2 = new Vector3 (myFocusPlayer.transform.transform.position.x, this.transform.position.y, myFocusPlayer.transform.transform.position.z);
                transform.LookAt (lookAtPosition2);
            }

            NavMeshPath path = new NavMeshPath ();

            navAgent.CalculatePath (myFocusPlayer.transform.position, path);
            if (path.status == NavMeshPathStatus.PathPartial)
            {
                Vector3 getCamPos = cam.WorldToViewportPoint (transform.position);

                if (getCamPos.x > 1f || getCamPos.x < 0f || getCamPos.y > 1f || getCamPos.y < 0f)
                {
                    DeadFonction ();
                }
            }
            else
            {
                ShootAgent ();
            }

        }
    }

    public void ShootAgent ()
    {
        timeAgent += Time.deltaTime;
        if (timeAgent > timeAttack)
        {
            float distance = Vector3.Distance (transform.position, myFocusPlayer.transform.position);

            if (distance < distanceShoot)
            {
                Vector3 lookAtPosition = new Vector3 (myFocusPlayer.transform.transform.position.x, this.transform.position.y, myFocusPlayer.transform.transform.position.z);
                transform.LookAt (lookAtPosition);
                spawnBulletAgent.transform.LookAt (lookAtPosition);

                RaycastHit hit;

                if (Physics.Raycast (spawnBulletAgent.position, spawnBulletAgent.forward, out hit))
                {
                    if (hit.transform.tag == "Player" || hit.transform.tag == "WeaponBox")
                    {
                        animAgent.SetBool ("IsMoving", false);
                        animAgent.SetTrigger ("Attack");
                        StartCoroutine (WaitAnimShoot ());
                    }
                    else
                    {
                        //  Debug.Log("I need Move");
                    }
                }
            }
            timeAttack = Random.Range (timeLeftAgentshoot - 0.2f, timeLeftAgentshoot + 0.2f);
            timeAgent = 0;
        }
    }

    public void TargetPlayer (float stopDistance, float maxdist)
    {
        if (myFocusPlayer != null)
        {
            float distance = Mathf.Abs (Vector3.Distance (transform.position, myFocusPlayer.transform.position));
            navAgent.stoppingDistance = stopDistance;
            distanceShoot = stopDistance;

            if (distance > maxdist)
            {
                navAgent.SetDestination (myFocusPlayer.transform.position);
            }
        }
    }

    #region ChangeEtat

    public void DeadFonction ()
    {
        particulWeapon.SetActive (false);
        navAgent.isStopped = true;
        agentsManager.DeadAgent (myFocusEtatAgent.ToString (), this.gameObject);
        StartCoroutine (WaitRespawn ());
    }

    public bool GetEtatAgent ()
    {
        if (myEtatAgent == AgentEtat.aliveAgent)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetFocusLawPlayer (GameObject player)
    {
        myFocusEtatAgent = CibleAgent.lawPlayer;
        myFocusPlayer = player;
    }

    public void SetFocusMaxPlayer (GameObject player)
    {
        myFocusEtatAgent = CibleAgent.maxPlayer;
        myFocusPlayer = player;
    }

    public void SetFocusLeadPlayer (GameObject player)
    {
        myFocusEtatAgent = CibleAgent.leadPlayer;
        myFocusPlayer = player;
    }

    public void SetFocusCauldron (GameObject player)
    {
        myFocusEtatAgent = CibleAgent.cauldron;
        myFocusPlayer = player;
    }

    public void SetFocusRandomPlayer (GameObject player)
    {
        myFocusEtatAgent = CibleAgent.randomPlayer;
        myFocusPlayer = player;
    }

    #endregion

    IEnumerator WaitAnimShoot ()
    {
        yield return new WaitForSeconds (0.4f);
        GameObject killeuse = (GameObject) Instantiate (bulletAgent, spawnBulletAgent.position, spawnBulletAgent.rotation, parentBullet);
    }

    IEnumerator WaitRespawn ()
    {
        animAgent.SetBool ("IsMoving", false);
        animAgent.ResetTrigger ("TakeDamage");
        animAgent.SetTrigger ("Die");
        yield return new WaitForSeconds (timeBeforeDepop);
        newPos ();
    }

    async void newPos ()
    {
        Vector3 newPos = agentsManager.CheckBestcheckPoint (myFocusPlayer.transform);
        navAgent.Warp (newPos);
        mycollider.enabled = true;
        myEtatAgent = AgentEtat.aliveAgent;
        navAgent.isStopped = false;
        lifeAgent = 1;
        particulWeapon.SetActive (true);

    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == Constants._PlayerBullet && myEtatAgent == AgentEtat.aliveAgent)
        {
            BulletAbstract getBA = other.GetComponent<BulletAbstract> ();
            GameObject explo = Instantiate (particleHit, transform.position, transform.rotation);
            Destroy (explo, 1f);
            if (getBA != null)
            {
                lifeAgent -= getBA.BulletDamage;
            }
            else
            {
                lifeAgent -= 1;
            }

            if (lifeAgent <= 0 && AgentEtat.aliveAgent == myEtatAgent)
            {
                myEtatAgent = AgentEtat.deadAgent;
                mycollider.enabled = false;
                DeadFonction ();
            }
            else
            {
                animAgent.SetTrigger ("TakeDamage");
            }
        }
    }
}