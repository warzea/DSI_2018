using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class activeMonster : MonoBehaviour
{
    public List<AgentController> agentDist;
    public List<AgentControllerCac> agentCac;

    public void SpawnEnemy ()
    {
        for (int i = 0; i < agentDist.Count; i++)
        {
            agentDist [i].checkUpdate = true;
        }
        for (int i = 0; i < agentCac.Count; i++)
        {
            agentCac [i].checkUpdate = true;
        }
        this.gameObject.SetActive (false);
    }

    void OnTriggerEnter (Collider col)
    {
        if (col.tag == "Player")
        {
            Manager.Ui.OnTuto = false;
            SpawnEnemy ();
        }
    }
}