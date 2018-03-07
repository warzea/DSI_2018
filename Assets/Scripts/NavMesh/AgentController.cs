using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentController : MonoBehaviour
{
    /// <summary> Public <summary>

    [Header("Info Agent")]
    public bool dead = false;
    public GameObject myFocusPlayer;
	public int lifeAgent = 1;


	[Header("Info Bullet")]
    public GameObject bulletAgent;
	public Transform spawnBulletAgent;
	public GameObject parentBullet;
	public float SpeedBulletAgent = 10f;
	public float timeLeftAgentshoot = 2f;
	public float distanceShoot = 20;


    public enum CibleAgent { lawPlayer, maxPlayer, leadPlayer, randomPlayer, nothing };
    public enum AgentEtat { deadAgent, aliveAgent };
    public CibleAgent myFocusEtatAgent;
    public AgentEtat myEtatAgent;

    private AgentsManager agentsManager;
    private NavMeshAgent navAgent;

    public Material deadMaterial;

	private float timeAgent = -5;

    void Awake()
    {
        agentsManager = GameObject.Find("ManagerNavMesh").GetComponent<AgentsManager>();
        navAgent = transform.GetComponent<NavMeshAgent>();
        myFocusEtatAgent = CibleAgent.nothing;
        myEtatAgent = AgentEtat.aliveAgent;
    }

    void Update()
    {
		if(myEtatAgent == AgentEtat.aliveAgent){
			ShootAgent ();
		}

    }

	public void ShootAgent(){
		timeAgent += Time.deltaTime;
		if (timeAgent > timeLeftAgentshoot) {

			float distance = Vector3.Distance (transform.position, myFocusPlayer.transform.position);

			if(distance < distanceShoot){
				transform.LookAt (myFocusPlayer.transform.position);
				GameObject killeuse = (GameObject)Instantiate (bulletAgent,spawnBulletAgent.position,spawnBulletAgent.rotation,parentBullet.transform);
				killeuse.GetComponent<Rigidbody> ().velocity = killeuse.transform.forward * SpeedBulletAgent;
			}
			timeAgent = 0;
		}

		if (dead)
		{
			DeadFonction();
			dead = false;
		}
	}

    public void TargetPlayer(float stopDistance, float maxdist)
    {
        if (myFocusPlayer != null)
        {
            float distance = Mathf.Abs(Vector3.Distance(transform.position, myFocusPlayer.transform.position));
            navAgent.stoppingDistance = stopDistance;
            if (distance > maxdist)
            {
                navAgent.SetDestination(myFocusPlayer.transform.position);
            }
        }
    }

    #region ChangeEtat
    public void DeadFonction()
    {
        agentsManager.DeadAgent(myFocusEtatAgent.ToString(), this.gameObject);
    }

    public bool GetEtatAgent()
    {
        if (myEtatAgent == AgentEtat.aliveAgent)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetFocusLawPlayer(GameObject player)
    {
        myFocusEtatAgent = CibleAgent.lawPlayer;
        myFocusPlayer = player;
    }

    public void SetFocusMaxPlayer(GameObject player)
    {
        myFocusEtatAgent = CibleAgent.maxPlayer;
        myFocusPlayer = player;
    }

    public void SetFocusLeadPlayer(GameObject player)
    {
        myFocusEtatAgent = CibleAgent.leadPlayer;
        myFocusPlayer = player;
    }

    public void SetFocusRandomPlayer(GameObject player)
    {
        myFocusEtatAgent = CibleAgent.randomPlayer;
        myFocusPlayer = player;
    }
    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants._PlayerBullet && myEtatAgent == AgentEtat.aliveAgent)
        {
            Debug.Log("Je suis touché");
            Destroy(other.gameObject);
            lifeAgent = lifeAgent - 1;
            if (lifeAgent <= 0)
            {
                transform.GetComponent<Renderer>().material = deadMaterial;
                myEtatAgent = AgentEtat.deadAgent;
                DeadFonction();
            }
        }
    }
}
