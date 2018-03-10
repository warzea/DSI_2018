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
    private CheckRoom[] roomFight;

    private Camera cam;

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
        roomFight = GameObject.FindObjectsOfType<CheckRoom>();
        cam = Manager.GameCont.MainCam;

        initialisation();
    }

    public Vector3 CheckBestcheckPoint(Transform posTarget)
    {
        Vector3 bestSpawn = new Vector3();
        float lastdist = 0;
        List<Transform> bestSpawnlist = new List<Transform>();
        bool inRoom = false;
        List<Transform> inRoomList = new List<Transform>();

        Debug.Log(roomFight.Length);

        for (int i = 0; i < roomFight.Length; i++)
        {
            if (roomFight[i].GetEtatRoom())
            {
                for (int j = 0; j < roomFight[i].checkPoint.Length; j++)
                {
                    Vector3 getCamPos = cam.WorldToViewportPoint(roomFight[i].checkPoint[j].transform.position);

                    if (getCamPos.x > 0.97f || getCamPos.x < 0.03f || getCamPos.y > 0.97f || getCamPos.y < 0.03f)
                    {
                        inRoomList.Add(roomFight[i].checkPoint[j]);
                        inRoom = true;
                    }
                }
                if (inRoom)
                {
                    int randomPos = Random.Range(0, inRoomList.Count);
                    bestSpawn = roomFight[i].checkPoint[randomPos].position;
                }
            }
        }

        if (!inRoom)
        {
            for (int i = 0; i < posRespawn.Length; i++)
            {
                Vector3 getCamPos = cam.WorldToViewportPoint(posRespawn[i].transform.position);

                if (getCamPos.x > 0.97f || getCamPos.x < 0.03f || getCamPos.y > 0.97f || getCamPos.y < 0.03f)
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
            }

            int randomSpawnPlayer = Random.Range(0, bestSpawnlist.Count);

            if (bestSpawnlist.Count != 0)
            {
                bestSpawn = bestSpawnlist[randomSpawnPlayer].position;
            }
        }
        return bestSpawn;
    }
}
