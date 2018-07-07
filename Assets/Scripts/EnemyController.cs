using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float health;
    public float speed;
    public float strength;
    public int pointValue = 1;

    public bool canShoot = false;
    public float shotCooldown =3f;

    public bool isAttacking;
    public float hitCooldown;

    public ParticleSystem pps;

    [Header("Enemy Sounds")]
    public AudioSource aSource;
    public AudioClip shotSound;
    public AudioClip deathSound;

    private ComboController cc;
    private MusicManager mm;
    private SFXManager sfxManager;
    private EnemyGun eg;
    private GameObject ec;
    private Rigidbody[] rbs;
    private PlayerController target;
    private SpawnerController sc;
    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        eg = GetComponentInChildren<EnemyGun>();
        mm = FindObjectOfType<MusicManager>();
        sfxManager = FindObjectOfType<SFXManager>();
        target = FindObjectOfType<PlayerController>();
        sc = FindObjectOfType<SpawnerController>();
        health = 5 * gmi.lStats.currentStage;
        cc = FindObjectOfType<ComboController>();
	}
	
	// Update is called once per frame
	void Update () {
        FindAndMove();

        hitCooldown -= Time.deltaTime;
        if (hitCooldown<=-0.5f)
        {
            hitCooldown = -0.5f;
        }

        if (isAttacking && hitCooldown<=0 && target.playerHP > 0)
        {
            print("Enemy hit cooldown is lessthan or = 0 ");
            DoDamage();
        }

        //if (gameObject.transform.position.y<=-20)
        //{
        //    gmi.lStats.enemiesDefeated++;
        //    gmi.lStats.currentPoints ++;
        //    gmi.lStats.atPoints ++;

        //    cc.StartComboTimer();
        //    sc.CallSpawn(1);
        //    Destroy(gameObject);
        //    sfxManager.PlayEnemyDeath();

        //}

        shotCooldown -= Time.deltaTime;
        if (shotCooldown <= -0.5f)
        {
            shotCooldown = -0.5f;
        }

        if (canShoot && shotCooldown<=0)
        {
            eg.Fire();
        }

    }

	private void OnCollisionEnter(Collision col)
	{
        GameObject go = col.gameObject;

        if (go.GetComponent<PlayerController>())
        {
            isAttacking = true;
        }

        if (go.GetComponent<EnemyController>())
        {
            Physics.IgnoreCollision(go.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    public void DoDamage(){
        PlayerController p = FindObjectOfType<PlayerController>();

        if (!p)
        {
            speed = 0;
            return;
        }

        if ((p.playerHP - strength)<=0)
        {
            p.playerHP = 0;
            DestroyPlayer(p);
            sc.spawningEnemies = false;
        }else{
            print("Doing " + strength + " damage");
            p.playerHP -= strength;
        }

        print("Setting hit cooldown to 2f");
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

    public void DestroyPlayer(PlayerController pc){
        
        rbs = GameObject.Find("EnemiesContainer").GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in rbs){
            rb.useGravity = false;
        }
        mm.audioSource.Stop();
        SpawnPlayerDeathParticles();
        gmi.lStats.playerLivesLost++;
        Destroy(pc.gameObject);
        isAttacking = false;
        sfxManager.PlayPlayerDeath();
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
