﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentsManager : ManagerParent
{

    [Header("------------------")]
    [Header("----INFO Level----")]
    [Header("------------------")]
    public Transform[] posRespawn;

    /// <summary> Public <summary>
    [Header("-------------------")]
    [Header("----INFO PLAYER----")]
    [Header("-------------------")]

    [Header("PlayerPref")]
    public GameObject[] player;

    [Header("Timer Agent Sleep and Info")]
    public float maxPosPlayer;
    public float timeLeftAgentLook = 0.5f;

    [Header("------------------")]
    [Header("----INFO FOCUS----")]
    [Header("------------------")]
    public int pourcentLowPV;
    public int pourcentMaxPV;
    public int pourcentLead;

    [Header("------------------")]
    [Header("----INFO AGENT----")]
    [Header("------------------")]

    [Header("List Agent Focus Law")]
    public List<AgentController> lawpvAgents;

    [Header("List Agent Focus Max")]
    public List<AgentController> maxpvAgents;

    [Header("List Agent Focus Lead")]
    public List<AgentController> leadAgents;

    [Header("List Agent Focus Other")]
    public List<AgentController> othersAgents;


    /// <summary> Private <summary>

    //Agent
    private AgentController[] agents;
    private int nbAgents;
    private int nbLawPv;
    private int nbMaxPv;
    private int nbLead;

    //AgentFocus;
    private GameObject playerLaw;
    private GameObject playerMax;
    private GameObject playerLead;

    //Timer
    private float timeAgent = 0;
    bool startCheck = false;

    protected override void InitializeManager()
    {
        agents = GameObject.FindObjectsOfType<AgentController>();
    }

    private void Update()
    {
        timeAgent += Time.deltaTime;
        if (startCheck && timeAgent > timeLeftAgentLook)
        {
            for (int i = 0; i < agents.Length; i++)
            {
                if (agents[i].GetEtatAgent())
                {
                    float randStopDist = Random.Range(10, 20);
                    agents[i].TargetPlayer(randStopDist, maxPosPlayer);
                }
            }
            timeAgent = 0;
        }
    }

    public Vector3 CheckBestcheckPoint(Transform posTarget)
    {
        float distanceSave = 500;
        Vector3 bestSpawn = new Vector3();
        for (int i = 0; i < posRespawn.Length; i++)
        {
            float distanceAgent = Vector3.Distance(posRespawn[i].localPosition, posTarget.localPosition);
            if (distanceSave > distanceAgent)
            {
                distanceSave = distanceAgent;
                bestSpawn = posRespawn[i].position;
            }
        }
        return bestSpawn;
    }

    #region WhoFocus
    public void ChangeEtatFocus(GameObject lawP, GameObject maxP, GameObject leadP)
    {
        playerLaw = lawP;
        playerMax = maxP;
        playerLead = leadP;
        ChangeFocusEtat();
    }


    public void ChangeFocusEtat()
    {
        for (int i = 0; i < agents.Length; i++)
        {
            if (agents[i].myFocusEtatAgent.ToString() == "lawPlayer")
            {
                agents[i].SetFocusLawPlayer(playerLaw);
            }
            else if (agents[i].myFocusEtatAgent.ToString() == "maxPlayer")
            {
                agents[i].SetFocusMaxPlayer(playerMax);
            }
            else if (agents[i].myFocusEtatAgent.ToString() == "leadPlayer")
            {
                agents[i].SetFocusLeadPlayer(playerLead);
            }
        }
    }

    #endregion

    #region IniGame
    public void InitGame()
    {
        startCheck = true;

        playerLaw = player[0];
        playerMax = player[0];
        playerLead = player[0];

        nbAgents = agents.Length;

        nbLawPv = (int)Mathf.Round(nbAgents * pourcentLowPV / 100);
        nbMaxPv = (int)Mathf.Round(nbAgents * pourcentMaxPV / 100);
        nbLead = (int)Mathf.Round(nbAgents * pourcentLead / 100);

        for (int i = 0; i < agents.Length; i++)
        {
            int randomPlayer = Random.Range(0, player.Length);
            othersAgents.Add(agents[i]);
            agents[i].SetFocusRandomPlayer(player[randomPlayer]);
        }

        CheckFocusIni();
    }
    #endregion

    #region Check if you need Agent
    public void CheckFocusIni()
    {
        if (lawpvAgents.Count < nbLawPv)
        {
            int needAgent = nbLawPv - lawpvAgents.Count;
            for (int i = 0; i < needAgent; i++)
            {
                lawpvAgents.Add(othersAgents[othersAgents.Count - 1]);
                othersAgents[othersAgents.Count - 1].SetFocusLawPlayer(playerLaw);
                othersAgents.Remove(othersAgents[othersAgents.Count - 1]);
            }
        }
        if (maxpvAgents.Count < nbMaxPv)
        {
            int needAgent = nbMaxPv - maxpvAgents.Count;
            for (int i = 0; i < needAgent; i++)
            {
                maxpvAgents.Add(othersAgents[othersAgents.Count - 1]);
                othersAgents[othersAgents.Count - 1].SetFocusMaxPlayer(playerMax);
                othersAgents.Remove(othersAgents[othersAgents.Count - 1]);
            }
        }
        if (leadAgents.Count < nbLead)
        {
            int needAgent = nbLead - leadAgents.Count;
            for (int i = 0; i < needAgent; i++)
            {
                leadAgents.Add(othersAgents[othersAgents.Count - 1]);
                othersAgents[othersAgents.Count - 1].SetFocusLeadPlayer(playerLead);
                othersAgents.Remove(othersAgents[othersAgents.Count - 1]);
            }
        }
    }
    #endregion

    #region Check if dead Agent
    public void DeadAgent(string etatAgentDead, GameObject agentDead)
    {
        int randomPlayer = Random.Range(0, player.Length);
        if (etatAgentDead == "lawPlayer")
        {
            for (int i = 0; i < lawpvAgents.Count; i++)
            {
                if (lawpvAgents[i].gameObject == agentDead)
                {
                    othersAgents.Insert(0, lawpvAgents[i]);
                    othersAgents[i].SetFocusRandomPlayer(player[randomPlayer]);
                    lawpvAgents.Remove(lawpvAgents[i]);
                    break;
                }
            }
        }
        else if (etatAgentDead == "maxPlayer")
        {
            for (int i = 0; i < maxpvAgents.Count; i++)
            {
                if (maxpvAgents[i].gameObject == agentDead)
                {
                    othersAgents.Insert(0, maxpvAgents[i]);
                    maxpvAgents[i].SetFocusRandomPlayer(player[randomPlayer]);
                    maxpvAgents.Remove(maxpvAgents[i]);
                    break;
                }
            }
        }
        else if (etatAgentDead == "leadPlayer")
        {
            for (int i = 0; i < leadAgents.Count; i++)
            {
                if (leadAgents[i].gameObject == agentDead)
                {
                    othersAgents.Insert(0, leadAgents[i]);
                    leadAgents[i].SetFocusRandomPlayer(player[randomPlayer]);
                    leadAgents.Remove(leadAgents[i]);
                    break;
                }
            }
        }
        else
        {
            Debug.Log("Je suis Random");
        }
        CheckFocusIni();
    }
    #endregion

}
