using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentInfo : MonoBehaviour {

	private NavMeshAgent agents;
	private GameObject focusPlayer;

	// Use this for initialization
	void Start () {
		agents = transform.GetComponent<NavMeshAgent>();
	}

	public void SetFocusPlayer(GameObject focus){
		focusPlayer = focus;
	}
}
