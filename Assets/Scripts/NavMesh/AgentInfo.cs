using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentInfo : MonoBehaviour {

	private NavMeshAgent agents;

	// Use this for initialization
	void Start () {
		agents = transform.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(agents.isStopped){
			Debug.Log ("ici");
		}
	}
}
