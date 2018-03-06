using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
	/// <summary> Public <summary>
	[Header("-------------------")]
	[Header("----INFO PLAYER----")]
	[Header("-------------------")]

	[Header("PlayerPref")]
	public GameObject[] player;

	[Header("Timer Agent Sleep and Info")]
	public float maxPosPlayer;
	public float timeLeftAgentLook = 0.5f;

	[Header("------------------")]
	[Header("----INFO FOCUS----")]
	[Header("------------------")]
	public int pourcentLowPV;
	public int pourcentMaxPV;
	public int pourcentLead;


	/// <summary> Private <summary>

	//Agent
	private NavMeshAgent[] agents;

	//Timer
	private float timeAgent = 5;


    void Start()
    {
        agents = GameObject.FindObjectsOfType<NavMeshAgent>();
		InitGame ();
    }

	public void InitGame(){
		int nbAgents = agents.Length;

		float nbLawPv = Mathf.Round(nbAgents * pourcentLowPV/100);
		float nbMaxPv = Mathf.Round(nbAgents * pourcentMaxPV/100);
		float nbLead = Mathf.Round(nbAgents * pourcentLead/100);
		float nbOther = nbAgents - nbLawPv - nbMaxPv - nbLead;

	}

    private void Update()
    {
		timeAgent += Time.deltaTime;
		if ( timeAgent > timeLeftAgentLook)
		{
			for (int i = 0; i < agents.Length; i++)
			{
				float distance = Mathf.Abs (Vector3.Distance (agents [i].transform.position, player[0].transform.position));
				if(distance>maxPosPlayer){
					float randStopDist = Random.Range (15, 25);
					agents[i].stoppingDistance = randStopDist;
					agents[i].SetDestination(player[0].transform.position);
				}
			}
			timeAgent = 0;
		}
    }
}
