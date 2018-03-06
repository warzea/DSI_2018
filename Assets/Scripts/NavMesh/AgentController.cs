using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public NavMeshAgent[] agents;

    private RaycastHit m_HitInfo = new RaycastHit();
    private Vector3 posPlayer;

    void Start()
    {
        agents = GameObject.FindObjectsOfType<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
            {
                posPlayer = m_HitInfo.point;

            }

            for (int i = 0; i < agents.Length; i++)
            {
                agents[i].SetDestination(posPlayer);
            }
        }
    }
}
