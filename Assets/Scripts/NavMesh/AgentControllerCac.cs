using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentControllerCac : MonoBehaviour
{
	public enum AgentEtat
	{
		deadAgent,
		aliveAgent}

	;

	public AgentEtat myEtatAgent;
	public TypeEnemy ThisType;

	private GameObject targetCauldron;
	public int lifeAgent = 1;
	public Animator animAgent;

	public float distancePlayer = 4f;
	public float distanceWeaponBox = 5f;

	public float speedVsCauldron = 20;
	public float speedVsPlayer = 20;

	private NavMeshAgent navAgent;
	public GameObject focusPlayer;
	private AgentsManagerCac agentsM;

	public float timeBeforeDepop = 0.1f;

	private float timeAgent = -5;

	public float timeLeftAgentAttacCac = 1f;
	public bool checkUpdate = false;

	private Camera cam;

	public GameObject MeshDestroy;

	private float timeAttack = 2f;

	public GameObject particleHit;

	void Start ()
	{
		navAgent = transform.GetComponent<NavMeshAgent> ();
		agentsM = Manager.AgentMC;

		myEtatAgent = AgentEtat.aliveAgent;
		targetCauldron = Manager.GameCont.WeaponB.gameObject;
		System.Action<AgentEvent> thisAct = delegate (AgentEvent thisEvnt) {
			checkUpdate = thisEvnt.AgentChecking;
			navAgent.isStopped = checkUpdate;
			myEtatAgent = AgentEtat.deadAgent;
		};
		cam = Manager.GameCont.MainCam;

		Manager.Event.Register (thisAct);
		navAgent.speed = speedVsPlayer;
		timeAttack = Random.Range (timeLeftAgentAttacCac - 0.1f, timeLeftAgentAttacCac + 0.1f);


	}


	void Update ()
	{
		timeAgent += Time.deltaTime;
		if (!checkUpdate) {
			return;
		}



		if (myEtatAgent == AgentEtat.aliveAgent && focusPlayer != null) {

			Vector3 lookAtPosition2 = new Vector3 (focusPlayer.transform.transform.position.x, this.transform.position.y, focusPlayer.transform.transform.position.z);
			transform.LookAt (lookAtPosition2);

			float velocity = navAgent.velocity.magnitude;
			if (velocity > 0.2) {
				animAgent.SetBool ("Move", true);
			} else {
				animAgent.SetBool ("Move", false);

			}

			float dist = Vector3.Distance (transform.position, focusPlayer.transform.position);
			if (dist > 3f) {
				navAgent.SetDestination (focusPlayer.transform.position);
			}

			NavMeshPath path = new NavMeshPath ();

			navAgent.CalculatePath (transform.position, path);
			if (path.status == NavMeshPathStatus.PathPartial) {
				Vector3 getCamPos = cam.WorldToViewportPoint (transform.position);

				if (getCamPos.x > 1f || getCamPos.x < 0f || getCamPos.y > 1f || getCamPos.y < 0f) {
					DeadFonction ();
				}
			} else {
				ShootCac ();
			}
		}

	}

	public void ShootCac ()
	{

		if (timeAgent > timeAttack) {
			if (focusPlayer != null) {
				float dist = Vector3.Distance (transform.position, focusPlayer.transform.position);

				if (focusPlayer.tag == "WeaponBox" && dist < distanceWeaponBox) {
					animAgent.SetBool ("Move", false);
					animAgent.SetTrigger ("MeleeAttack");
					focusPlayer.GetComponent<WeaponBox> ().TakeHit ();
				} else if (focusPlayer.tag == "Player" && dist < distancePlayer) {
					animAgent.SetBool ("Move", false);
					animAgent.SetTrigger ("MeleeAttack");
					focusPlayer.GetComponent<PlayerController> ().GetDamage (transform);
				}
			}
			timeAttack = Random.Range (timeLeftAgentAttacCac - 0.1f, timeLeftAgentAttacCac + 0.1f);
			timeAgent = 0;
		}
	}

	public void SetTarget (GameObject focus)
	{
		focusPlayer = focus;
		if (navAgent != null) {
			navAgent.speed = speedVsPlayer;
		}
	}

	public void SwitchCauldron ()
	{
		focusPlayer = targetCauldron;
		navAgent.speed = speedVsCauldron;
	}

	public void TargetPlayer ()
	{
		if (targetCauldron != null) {
			navAgent.SetDestination (targetCauldron.transform.position);
		}
	}

	IEnumerator WaitRespawn ()
	{
		animAgent.SetBool ("Move", false);
		animAgent.SetTrigger ("Die");
		yield return new WaitForSeconds (timeBeforeDepop);
		Instantiate (MeshDestroy, transform.position, transform.rotation);
		Vector3 newPos = agentsM.CheckBestcheckPoint (focusPlayer.transform);
		navAgent.Warp (newPos);
		focusPlayer = targetCauldron;
		navAgent.speed = speedVsCauldron;
		navAgent.isStopped = false;
		myEtatAgent = AgentEtat.aliveAgent;
		lifeAgent = 1;
		animAgent.SetTrigger ("MeleeAttack");
		animAgent.SetBool ("Move", true);
	}

	public void DeadFonction ()
	{
		navAgent.isStopped = true;
		StartCoroutine (WaitRespawn ());
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "BulletPlayer") {
			GameObject explo = Instantiate (particleHit, transform.position, transform.rotation);
			Destroy (explo, 1f);
			lifeAgent = lifeAgent - 1;
			if (lifeAgent <= 0 && AgentEtat.aliveAgent == myEtatAgent) {
				myEtatAgent = AgentEtat.deadAgent;
				DeadFonction ();
			}
		}
	}

}
