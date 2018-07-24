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

    private enum DmgType{projectile,melee};
    //private ComboController cc;
    private MusicManager mm;
    private SFXManager sfxManager;
    public EnemyGun eg;
    private PlayerController target;
    private Vector3 targetPos;
    //private SpawnerController sc;
    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        mm = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        target = GameObject.Find("Player").GetComponent<PlayerController>();

        if (gmi.lStats.currentStage>5)
        {
            int shootRoll = Random.Range(0, gmi.lStats.currentStage);
            if (shootRoll%2==0)
            {
                canShoot = true;
            }
        }
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
            DoDamage(1);
        }

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

    public void DoDamage(int multiplier){
        //PlayerController p = FindObjectOfType<PlayerController>();

        if (!target)
        {
            speed = 0;
            return;
        }

        if ((target.playerHP - strength*multiplier)<=0)
        {
            target.playerHP = 0;
            sfxManager.PlayPlayerDeath();
            isAttacking = false;
            //sc.spawningEnemies = false;
            gmi.DestroyPlayer(target);

        }else{
            target.playerHP -= strength*multiplier;
            sfxManager.PlayPlayerHit();
        }

        hitCooldown = Random.Range(1f,2f);
    }

    void FindAndMove(){
        if (target)
        {
            transform.LookAt(target.transform);
        }
        if (!target){
            speed = 0;
            return;
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


}
