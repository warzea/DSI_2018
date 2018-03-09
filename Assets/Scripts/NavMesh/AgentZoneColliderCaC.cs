using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentZoneColliderCaC : MonoBehaviour
{
    private AgentControllerCac aCac;
    void Start()
    {
        aCac = transform.GetComponentInParent<AgentControllerCac>();
    }

    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            aCac.SetTarget(other.gameObject);
        }
    }
}
