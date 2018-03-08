using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAgent : MonoBehaviour
{

    public float timeBulletLife = 5;
    public float speedBullet = 10;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, timeBulletLife);
    }
    void Update()
    {
        transform.position += transform.forward * speedBullet * Time.deltaTime;
    }


    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
