using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyMeshEnnemi : MonoBehaviour
{

    public float timeBeforeDepop = 3;
    void Start()
    {
        Destroy(this.gameObject, timeBeforeDepop);
    }
}
