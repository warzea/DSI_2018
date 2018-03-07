using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAgent : MonoBehaviour {

	public float timeBulletLife = 5;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, timeBulletLife);
	}
	
	void OnCollisionEnter(Collision col)
	{
		Destroy (gameObject);
	}
}
