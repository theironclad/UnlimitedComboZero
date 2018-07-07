using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

    private SFXManager sfxManager;
    private ComboController cc;
    private SpawnerController sc;
    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        sc = FindObjectOfType<SpawnerController>();
        cc = FindObjectOfType<ComboController>();
        sfxManager = FindObjectOfType<SFXManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision col)
    {
        print("Somthing collided!");
        GameObject go = col.gameObject;
        if (go.GetComponent<EnemyController>())
        {
            EnemyController oec = go.GetComponent<EnemyController>();
            gmi.lStats.enemiesDefeated++;
            gmi.lStats.currentPoints += (oec.pointValue * gmi.lStats.currentStage);
            gmi.lStats.atPoints += (oec.pointValue);
            Destroy(go);
            cc.StartComboTimer();
            sc.CallSpawn(gmi.lStats.spawnAddAP);
            sfxManager.PlayEnemyDeath();
        }
    }


}
