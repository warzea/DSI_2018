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
    float valuePos;
    void Start ()
    {
        Cauldron = Manager.GameCont.WeaponB.transform;
        posDepart = Cauldron.transform.position;

        if (posFinal == null)
        {
            posFinal = GameObject.FindObjectOfType<EndTrigger> ().transform;
        }
        maxdistance = Vector3.Distance (posDepart, posFinal.position);
    }

    void Update ()
    {
        distance = Vector3.Distance (Cauldron.position, posFinal.position);
        float valuePos = 1 - (distance / maxdistance);

        if (valuePos <= 1 && valuePos >= 0)
        {
            sliderCanvas.value = valuePos;
        }
    }
}