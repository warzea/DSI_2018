using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentZoneExitColliderCaC : MonoBehaviour
{
    private AgentControllerCac aCac;
    void Start()
    {
        aCac = transform.GetComponentInParent<AgentControllerCac>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            aCac.SwitchCauldron();
        }
    }
}
