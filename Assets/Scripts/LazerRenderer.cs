using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LazerRenderer : MonoBehaviour
{

    private LineRenderer lineLaser;
    private Transform myTransform;

    public float distMaxLaser = 100;

    // Use this for initialization
    void Start ()
    {
        lineLaser = transform.GetComponent<LineRenderer> ();
        myTransform = this.transform;

    }

    // Update is called once per frame
    void Update ()
    {
        RaycastHit hit;
        if (Physics.Raycast (myTransform.position, myTransform.forward, out hit))
        {
            if (hit.collider.tag == Constants._Wall)
            {
                distMaxLaser = hit.distance;
            }
            else
            {
                distMaxLaser = 50;
            }

            lineLaser.SetPosition (1, new Vector3 (0, 0, distMaxLaser));
        }
    }
}