using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsManagerCac : MonoBehaviour
{
    private AgentControllerCac[] agents;
    public GameObject[] player;
    private GameObject playerCauldron;

    void Start()
    {
        agents = GameObject.FindObjectsOfType<AgentControllerCac>();
        playerCauldron = Manager.GameCont.WeaponB.gameObject;
    }

    void initialisation()
    {
        for (int i = 0; i < agents.Length; i++)
        {
            //agents[i];
        }
    }
}
