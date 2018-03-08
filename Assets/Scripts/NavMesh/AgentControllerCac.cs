using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentControllerCac : MonoBehaviour
{
    public enum AgentEtat { deadAgent, aliveAgent };
    public AgentEtat myEtatAgent;
    public GameObject targetCauldron;
    public int lifeAgent = 1;

    public Material deadMaterial;
    public Material aliveMaterial;

    private NavMeshAgent navAgent;
    private GameObject playerFocus;
    private AgentsManagerCac agentsM;


    void Awake()
    {
        agentsM = GameObject.Find("ManagerNavMesh").GetComponent<AgentsManagerCac>();
        navAgent = transform.GetComponent<NavMeshAgent>();
        myEtatAgent = AgentEtat.aliveAgent;
    }

    public void SetTarget(GameObject focus)
    {
        targetCauldron = focus;
    }

    public void TargetPlayer()
    {
        if (targetCauldron != null)
        {
            navAgent.SetDestination(targetCauldron.transform.position);
        }
    }

    public void DeadFonction()
    {
        //agentsManager.DeadAgent(myFocusEtatAgent.ToString(), this.gameObject);
    }

    IEnumerator WaitRespawn()
    {
        yield return new WaitForSeconds(1);
        transform.GetComponent<Renderer>().material = aliveMaterial;
        //navAgent.Warp(agentsM.CheckBestcheckPoint(myFocusPlayer.transform));
        yield return new WaitForSeconds(1);
        myEtatAgent = AgentEtat.aliveAgent;
        lifeAgent = 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BulletPlayer")
        {
            Destroy(other.gameObject);
            lifeAgent = lifeAgent - 1;
            if (lifeAgent <= 0)
            {
                myEtatAgent = AgentEtat.deadAgent;
                transform.GetComponent<Renderer>().material = deadMaterial;
                DeadFonction();
                StartCoroutine(WaitRespawn());
            }
        }
    }

}
