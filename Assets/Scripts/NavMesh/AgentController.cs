using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentController : MonoBehaviour
{
    public enum CibleAgent { lawPlayer, maxPlayer, leadPlayer, randomPlayer, nothing };
    public enum AgentEtat { deadAgent, aliveAgent };
    public CibleAgent myFocusEtatAgent;
    public bool dead = false;

    public GameObject myFocusPlayer;
    private AgentsManager agentsManager;
    private NavMeshAgent navAgent;


    void Awake()
    {
        agentsManager = GameObject.Find("ManagerNavMesh").GetComponent<AgentsManager>();
        navAgent = transform.GetComponent<NavMeshAgent>();
        myFocusEtatAgent = CibleAgent.nothing;
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
        float distance = Mathf.Abs(Vector3.Distance(transform.position, myFocusPlayer.transform.position));
        navAgent.stoppingDistance = stopDistance;
        if (distance > maxdist)
        {
            navAgent.SetDestination(myFocusPlayer.transform.position);
        }
    }

    #region ChangeEtat
    public void DeadFonction()
    {
        agentsManager.DeadAgent(myFocusEtatAgent.ToString(), this.gameObject);
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
}
