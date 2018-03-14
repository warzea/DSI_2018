using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider_positionplayer : MonoBehaviour
{
    public Transform Cauldron;
    public Transform posFinal;
    public Slider sliderCanvas;
    private Vector3 posDepart;
    private float distance = 0;
    private float maxdistance;

    void Start()
    {
        posDepart = Cauldron.transform.position;
        maxdistance = Vector3.Distance(posDepart, posFinal.position);
    }

    void Update()
    {
        distance = Vector3.Distance(Cauldron.position, posFinal.position);
        if (distance <= 1 && distance >= 0)
        {
            float valuePos = 1 - (distance / maxdistance);
            sliderCanvas.value = valuePos;
        }
    }
}
