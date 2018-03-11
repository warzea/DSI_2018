﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentController : MonoBehaviour
{
    /// <summary> Public <summary>

    [Header("------------------")]
    [Header("----INFO AGENT----")]
    [Header("------------------")]
    public TypeEnemy ThisType;
    public GameObject myFocusPlayer;
    public int lifeAgent = 1;
    public float timeBeforeDepop = 2f;
    public float timeBeforeAlive = 1f;

    [Header("------------------")]
    [Header("----INFO SHOOT----")]
    [Header("------------------")]
    public GameObject bulletAgent;
    public Transform spawnBulletAgent;
    Transform parentBullet;
    public float SpeedBulletAgent = 10f;
    public float timeLeftAgentshoot = 2f;
    public float distanceShoot = 20;


    public enum CibleAgent { lawPlayer, maxPlayer, leadPlayer, cauldron, randomPlayer, nothing };
    public enum AgentEtat { deadAgent, aliveAgent };

    [Header("Info Agent")]
    public CibleAgent myFocusEtatAgent;
    public AgentEtat myEtatAgent;
    private AgentsManager agentsManager;
    private NavMeshAgent navAgent;

    [Header("Materials dead/alive")]
    public Material deadMaterial;
    public Material aliveMaterial;

    private float timeAgent = -5;

    void Awake()
    {
        agentsManager = GameObject.Find("ManagerNavMesh").GetComponent<AgentsManager>();
        navAgent = transform.GetComponent<NavMeshAgent>();
        myFocusEtatAgent = CibleAgent.nothing;
        myEtatAgent = AgentEtat.aliveAgent;
        timeLeftAgentshoot = Random.Range(timeLeftAgentshoot - 0.3f, timeLeftAgentshoot + 0.3f);
    }

    void Start ( )
    {
         parentBullet = Manager.GameCont.Garbage;
    }

    void Update()
    {
        if (myEtatAgent == AgentEtat.aliveAgent && myFocusPlayer != null)
        {
            NavMeshPath path = new NavMeshPath();

            navAgent.CalculatePath(myFocusPlayer.transform.position, path);
            if (path.status == NavMeshPathStatus.PathPartial)
            {
                DeadFonction();
            }
            else
            {
                ShootAgent();
            }
        }
    }

    public void ShootAgent()
    {
        timeAgent += Time.deltaTime;
        if (timeAgent > timeLeftAgentshoot)
        {
            float distance = Vector3.Distance(transform.position, myFocusPlayer.transform.position);

            if (distance < distanceShoot)
            {
                Vector3 lookAtPosition = new Vector3(myFocusPlayer.transform.transform.position.x, this.transform.position.y, myFocusPlayer.transform.transform.position.z);
                transform.LookAt(lookAtPosition);

                RaycastHit hit;

                if (Physics.Raycast(spawnBulletAgent.position, spawnBulletAgent.forward, out hit))
                {
                    if (hit.transform.tag == "Player" || hit.transform.tag == "WeaponBox")
                    {
                        GameObject killeuse = (GameObject)Instantiate(bulletAgent, spawnBulletAgent.position, spawnBulletAgent.rotation, parentBullet);
                    }
                    else
                    {
                        //  Debug.Log("I need Move");
                    }
                }
            }
            timeAgent = 0;
        }
    }

    public void TargetPlayer(float stopDistance, float maxdist)
    {
        if (myFocusPlayer != null)
        {
            float distance = Mathf.Abs(Vector3.Distance(transform.position, myFocusPlayer.transform.position));
            navAgent.stoppingDistance = stopDistance;
            if (distance > maxdist)
            {
                navAgent.SetDestination(myFocusPlayer.transform.position);
            }
        }
    }

    #region ChangeEtat
    public void DeadFonction()
    {
        myEtatAgent = AgentEtat.deadAgent;
        navAgent.isStopped = true;
        transform.GetComponent<Renderer>().material = deadMaterial;
        agentsManager.DeadAgent(myFocusEtatAgent.ToString(), this.gameObject);
        StartCoroutine(WaitRespawn());
    }

    public bool GetEtatAgent()
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

    public void SetFocusLawPlayer(GameObject player)
    {
        myFocusEtatAgent = CibleAgent.lawPlayer;
        myFocusPlayer = player;
    }

    public void SetFocusMaxPlayer(GameObject player)
    {
        myFocusEtatAgent = CibleAgent.maxPlayer;
        myFocusPlayer = player;
    }

    public void SetFocusLeadPlayer(GameObject player)
    {
        myFocusEtatAgent = CibleAgent.leadPlayer;
        myFocusPlayer = player;
    }

    public void SetFocusCauldron(GameObject player)
    {
        myFocusEtatAgent = CibleAgent.cauldron;
        myFocusPlayer = player;
    }

    public void SetFocusRandomPlayer(GameObject player)
    {
        myFocusEtatAgent = CibleAgent.randomPlayer;
        myFocusPlayer = player;
    }
    #endregion

    IEnumerator WaitRespawn()
    {
        yield return new WaitForSeconds(timeBeforeDepop);
        transform.GetComponent<Renderer>().material = aliveMaterial;
        navAgent.Warp(agentsManager.CheckBestcheckPoint(myFocusPlayer.transform));
        yield return new WaitForSeconds(timeBeforeAlive);
        myEtatAgent = AgentEtat.aliveAgent;
        navAgent.isStopped = false;
        lifeAgent = 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants._PlayerBullet && myEtatAgent == AgentEtat.aliveAgent)
        {
            BulletAbstract getBA = other.GetComponent<BulletAbstract>();

            lifeAgent -= getBA.BulletDamage;

            if (lifeAgent <= 0 && AgentEtat.aliveAgent == myEtatAgent)
            {
                DeadFonction();
            }
        }
    }
}
