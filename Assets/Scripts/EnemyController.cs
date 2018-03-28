using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float health;
    public float speed;
    public float strength;
    public int pointValue = 1;

    public bool isAttacking;
    public float hitCooldown;

    public ParticleSystem pps;
    private PlayerController target;
    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        target = FindObjectOfType<PlayerController>();
        health = 5 * gmi.lStats.currentStage;
	}
	
	// Update is called once per frame
	void Update () {
        FindAndMove();

        hitCooldown -= Time.deltaTime;
        if (hitCooldown<=-0.5f)
        {
            hitCooldown = -0.5f;
        }

        if (isAttacking && hitCooldown<=0)
        {
            DoDamage();
        }
    }

	private void OnCollisionEnter(Collision col)
	{
        GameObject go = col.gameObject;
        print(go);
        if (go.GetComponent<PlayerController>())
        {
            isAttacking = true;
        }

        if (go.GetComponent<EnemyController>())
        {
            Physics.IgnoreCollision(go.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    void DoDamage(){
        PlayerController p = FindObjectOfType<PlayerController>();

        if ((p.playerHP - strength)<=0)
        {
            p.playerHP = 0;
            DestroyPlayer(p);
        }else{
            p.playerHP -= strength;
        }

        hitCooldown = 2f;
    }

    void FindAndMove(){
        if (target)
        {
            transform.LookAt(target.transform);
        }
        if (!target){
            speed = 0;
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void DestroyPlayer(PlayerController pc){
        SpawnPlayerDeathParticles();
        gmi.lStats.playerLivesLost++;
        Destroy(pc.gameObject);
        isAttacking = false;
    }

    public void SpawnPlayerDeathParticles()
    {
        ParticleSystem newParticles = Instantiate(pps, target.transform.position, Quaternion.identity);
        float desTime = newParticles.main.duration;
        Destroy(newParticles, desTime);
        Invoke("CallGO", desTime);
        gmi.inGame = false;
    }

    public void CallGO(){
        gmi.GameOverActive();
    }
}
