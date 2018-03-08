using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentControllerCac : MonoBehaviour
{
    public enum AgentEtat { deadAgent, aliveAgent };
    public AgentEtat myEtatAgent;
    private GameObject targetCauldron;
    public int lifeAgent = 1;

    public Material deadMaterial;
    public Material aliveMaterial;

    private NavMeshAgent navAgent;
    public GameObject focusPlayer;
    private AgentsManagerCac agentsM;

    private float timeAgent = -5;

    public float timeLeftAgentAttacCac = 1f;
    void Awake()
    {
        agentsM = GameObject.Find("ManagerNavMesh").GetComponent<AgentsManagerCac>();
        navAgent = transform.GetComponent<NavMeshAgent>();
        myEtatAgent = AgentEtat.aliveAgent;
        targetCauldron = Manager.GameCont.WeaponB.gameObject;
    }

    void Update()
    {
        if (myEtatAgent == AgentEtat.aliveAgent)
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
                        Debug.Log("WeaponBox");
                        //Attaque WeaponBox
                    }
                    else if (focusPlayer.tag == "Player")
                    {
                        Debug.Log("Player");
                        //Attaque Player
                    }
                }
            }
            timeAgent = 0;
        }
    }

    public void SetTarget(GameObject focus)
    {
        focusPlayer = focus;
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
        navAgent.isStopped = false;
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
                navAgent.isStopped = true;
                myEtatAgent = AgentEtat.deadAgent;
                transform.GetComponent<Renderer>().material = deadMaterial;
                StartCoroutine(WaitRespawn());
            }
        }
    }

}
