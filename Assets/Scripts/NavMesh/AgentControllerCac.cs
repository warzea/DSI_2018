using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentControllerCac : MonoBehaviour
{
    public enum AgentEtat { deadAgent, aliveAgent };
    public AgentEtat myEtatAgent;
    public TypeEnemy ThisType;

    private GameObject targetCauldron;
    public int lifeAgent = 1;

    public float speedVsCauldron = 20;
    public float speedVsPlayer = 20;
    public Material deadMaterial;
    public Material aliveMaterial;

    private NavMeshAgent navAgent;
    public GameObject focusPlayer;
    private AgentsManagerCac agentsM;

    private float timeAgent = -5;

    public float timeLeftAgentAttacCac = 1f;
    bool checkUpdate = true;

    private Camera cam;

    void Start()
    {
        myEtatAgent = AgentEtat.aliveAgent;
        targetCauldron = Manager.GameCont.WeaponB.gameObject;
        System.Action<AgentEvent> thisAct = delegate (AgentEvent thisEvnt)
      {
          checkUpdate = thisEvnt.AgentChecking;
          navAgent.isStopped = checkUpdate;
      };
        cam = Manager.GameCont.MainCam;

        Manager.Event.Register(thisAct);
    }
    void Awake()
    {
        navAgent = transform.GetComponent<NavMeshAgent>();
        agentsM = GameObject.Find("ManagerNavMesh").GetComponent<AgentsManagerCac>();
    }

    void Update()
    {

        if (!checkUpdate)
        {
            return;
        }

        NavMeshPath path = new NavMeshPath();

        navAgent.CalculatePath(transform.position, path);
        if (path.status == NavMeshPathStatus.PathPartial)
        {
            Vector3 getCamPos = cam.WorldToViewportPoint(transform.position);

            if (getCamPos.x > 1f || getCamPos.x < 0f || getCamPos.y > 1f || getCamPos.y < 0f)
            {
                DeadFonction();
            }
        }
        else
        {
            ShootCac();
        }

    }

    public void ShootCac()
    {
        timeAgent += Time.deltaTime;

        if (timeAgent > timeLeftAgentAttacCac)
        {
            if (targetCauldron != null)
            {
                float dist = Vector3.Distance(transform.position, focusPlayer.transform.position);
                Vector3 lookAtPosition = new Vector3(focusPlayer.transform.transform.position.x, this.transform.position.y, focusPlayer.transform.transform.position.z);
                if (dist > 2f)
                {
                    navAgent.SetDestination(focusPlayer.transform.position);
                }
                else
                {
                    transform.LookAt(lookAtPosition);
                    if (focusPlayer.tag == "WeaponBox")
                    {
                        focusPlayer.GetComponent<WeaponBox>().TakeHit();
                    }
                    else if (focusPlayer.tag == "Player")
                    {
                        Debug.Log("Shhooottt");
                        focusPlayer.GetComponent<PlayerController>().GetDamage(transform);
                    }
                }
            }
            timeAgent = 0;
        }
    }

    public void SetTarget(GameObject focus)
    {
        focusPlayer = focus;
        if (navAgent != null)
        {
            navAgent.speed = speedVsPlayer;
        }
    }

    public void SwitchCauldron()
    {
        focusPlayer = targetCauldron;
        navAgent.speed = speedVsCauldron;
    }

    public void TargetPlayer()
    {
        if (targetCauldron != null)
        {
            navAgent.SetDestination(targetCauldron.transform.position);
        }
    }

    IEnumerator WaitRespawn()
    {
        yield return new WaitForSeconds(1);
        transform.GetComponent<Renderer>().material = aliveMaterial;
        navAgent.Warp(agentsM.CheckBestcheckPoint(focusPlayer.transform));
        yield return new WaitForSeconds(1);
        focusPlayer = targetCauldron;
        navAgent.speed = speedVsCauldron;
        navAgent.isStopped = false;
        myEtatAgent = AgentEtat.aliveAgent;
        lifeAgent = 1;
    }

    public void DeadFonction()
    {
        myEtatAgent = AgentEtat.deadAgent;
        navAgent.isStopped = true;
        transform.GetComponent<Renderer>().material = deadMaterial;
        StartCoroutine(WaitRespawn());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BulletPlayer")
        {
            lifeAgent = lifeAgent - 1;
            if (lifeAgent <= 0 && AgentEtat.aliveAgent == myEtatAgent)
            {
                DeadFonction();
            }
        }
    }

}
