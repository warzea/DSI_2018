using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsManagerCac : ManagerParent
{
    [Header("------------------")]
    [Header("----INFO Level----")]
    [Header("------------------")]
    public Transform[] posRespawn;

    [Header("------------------")]
    [Header("----INFO AGENT----")]
    [Header("------------------")]
    public AgentControllerCac[] agents;


    [Header("Cauldron")]
    public GameObject playerCauldron;

    [Header("Distance max checkpoints")]
    public float distanceSave = 200;

    void initialisation()
    {
        for (int i = 0; i < agents.Length; i++)
        {
            agents[i].SetTarget(playerCauldron);
        }
    }

    protected override void InitializeManager()
    {
        agents = GameObject.FindObjectsOfType<AgentControllerCac>();
        playerCauldron = Manager.GameCont.WeaponB.gameObject;
        initialisation();
    }

    public Vector3 CheckBestcheckPoint(Transform posTarget)
    {
        Vector3 bestSpawn = new Vector3();
        float lastdist = 0;
        List<Transform> bestSpawnlist = new List<Transform>();

        for (int i = 0; i < posRespawn.Length; i++)
        {
            float distanceAgent = Vector3.Distance(posRespawn[i].localPosition, posTarget.localPosition);
            if (distanceSave > distanceAgent)
            {
                bestSpawnlist.Add(posRespawn[i]);
            }
            else
            {
                if (lastdist == 0)
                {
                    lastdist = distanceAgent;
                    bestSpawn = posRespawn[i].position;
                }
                else if (lastdist > distanceAgent)
                {
                    bestSpawn = posRespawn[i].position;
                }
            }
        }

        int randomSpawnPlayer = Random.Range(0, bestSpawnlist.Count);

        if (bestSpawnlist.Count != 0)
        {
            bestSpawn = bestSpawnlist[randomSpawnPlayer].position;
            return bestSpawn;
        }
        else
        {
            return bestSpawn;
        }
    }
}
