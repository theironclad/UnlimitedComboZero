﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float speed;
    public float dmg;

    private ComboController cc;
    private SpawnerController sc;
    private GameManager gmi;

    void Start(){
        gmi = GameManager.Instance;
        sc = FindObjectOfType<SpawnerController>();
        cc = FindObjectOfType<ComboController>();
    }

	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
        Destroy(gameObject, 8f);
	}

    private void OnCollisionEnter(Collision col)
    {
        GameObject go = col.gameObject;
        if (go.GetComponent<EnemyController>())
        {
            DoDamage(go);
            Destroy(gameObject);
        }

        if (go.GetComponent<ProjectileController>())
        {
            print("Hitting projectile");
            Physics.IgnoreCollision(go.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    void DoDamage(GameObject dmgTarget)
    {
        if((dmgTarget.GetComponent<EnemyController>().health -= dmg)<=0){
            DestroyEnemy(dmgTarget);
        }else{
            dmgTarget.GetComponent<EnemyController>().health -= dmg;
        }

    }

    void DestroyEnemy(GameObject obj){
        EnemyController oec = obj.GetComponent<EnemyController>();
        gmi.lStats.enemiesDefeated++;
        gmi.lStats.currentPoints += (oec.pointValue * gmi.lStats.currentStage);
        gmi.lStats.atPoints += (oec.pointValue);
        Destroy(obj);
        cc.StartComboTimer();
        sc.spawnCooldown = 0.1f;
    }
}
