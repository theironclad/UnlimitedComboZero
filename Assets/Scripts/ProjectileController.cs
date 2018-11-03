using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float speed;
    public float dmg;

    private SFXManager sfxManager;
    private ComboController cc;
    private SpawnerController sc;
    private GameManager gmi;

    void Start(){
        gmi = GameManager.Instance;
        sc = FindObjectOfType<SpawnerController>();
        cc = FindObjectOfType<ComboController>();
        sfxManager = FindObjectOfType<SFXManager>();
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

        if(go.GetComponent<Shredder>()){
            Debug.Log("Hit shredder");
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
        Animator a = obj.GetComponent<Animator>();       
        gmi.lStats.enemiesDefeated++;
        switch(name){
            case "SpreadProjectile(Clone)":
                gmi.lStats.spreadGunKills++;
                break;
            case "PlayerProjectile(Clone)":
                gmi.lStats.defaultGunKills++;
                break;
            default:
                break;
        }
        gmi.lStats.currentPoints += (oec.pointValue * gmi.lStats.currentStage);
        gmi.lStats.atPoints += (oec.pointValue);
        oec.DeathParticles();
        Destroy(obj);
        cc.StartComboTimer();
        sc.CallSpawn(gmi.lStats.spawnAddAP);
        sfxManager.PlayEnemyDeath();
    }
}
