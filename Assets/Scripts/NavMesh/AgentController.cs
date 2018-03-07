using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentController : MonoBehaviour
{
    /// <summary> Public <summary>

    [Header("Info Agent")]
    public bool dead = false;
    public GameObject myFocusPlayer;
    public GameObject bulletAgent;

    public int lifeAgent = 1;

    public enum CibleAgent { lawPlayer, maxPlayer, leadPlayer, randomPlayer, nothing };
    public enum AgentEtat { deadAgent, aliveAgent };
    public CibleAgent myFocusEtatAgent;
    public AgentEtat myEtatAgent;

    private AgentsManager agentsManager;
    private NavMeshAgent navAgent;

    public Material deadMaterial;


    void Awake()
    {
        agentsManager = GameObject.Find("ManagerNavMesh").GetComponent<AgentsManager>();
        navAgent = transform.GetComponent<NavMeshAgent>();
        myFocusEtatAgent = CibleAgent.nothing;
        myEtatAgent = AgentEtat.aliveAgent;
    }

    void Update()
    {
        if (dead)
        {
            DeadFonction();
            dead = false;
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
        agentsManager.DeadAgent(myFocusEtatAgent.ToString(), this.gameObject);
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

    public void SetFocusRandomPlayer(GameObject player)
    {
        myFocusEtatAgent = CibleAgent.randomPlayer;
        myFocusPlayer = player;
    }
    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants._PlayerBullet && myEtatAgent == AgentEtat.aliveAgent)
        {
            Debug.Log("Je suis touché");
            Destroy(other.gameObject);
            lifeAgent = lifeAgent - 1;
            if (lifeAgent <= 0)
            {
                transform.GetComponent<Renderer>().material = deadMaterial;
                myEtatAgent = AgentEtat.deadAgent;
                DeadFonction();
            }
        }
    }
}
