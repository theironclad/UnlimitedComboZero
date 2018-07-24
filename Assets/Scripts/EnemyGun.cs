using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour {

    public EnemyProjectile projectilePrefab;
    public Transform muzzle;

    public bool isFiring;
    public float projectileSpeed;
    public float fireRate;
    public float dmg;

    public float shotCounter;

    public EnemyController ec;

    public ProjectilesContainer pc;
    private GameManager gmi;

    // Use this for initialization
    void Start()
    {
        gmi = GameManager.Instance;
        pc = GameObject.Find("ProjectilesContainer").GetComponent<ProjectilesContainer>();
        //ec = GetComponentInParent<EnemyController>();
        muzzle = transform;
        Invoke("LoadGun", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        shotCounter -= Time.deltaTime;
        if (isFiring)
        {
            Fire();
        }

        if (shotCounter < -1.0f)
        {
            shotCounter = -.5f;
        }
    }

    public void Fire()
    {

        if (shotCounter <= 0f)
        {
            shotCounter = fireRate;
            EnemyProjectile newBullet = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
            newBullet.ec = GetComponentInParent<EnemyController>();
            newBullet.transform.SetParent(pc.transform, true);
            newBullet.speed = ec.speed + 5;
            ec.aSource.clip = ec.shotSound;
            ec.aSource.Play();
        }
    }

    public void LoadNewLevelGun()
    {
        projectileSpeed = 5;
        fireRate = 3;

    }

    public void LoadGun()
    {
        if (gmi.inGame)
        {
            LoadNewLevelGun();
        }
        else if (!gmi.inGame)
        {
            projectileSpeed = ec.speed + 5;
            fireRate = 3;
        }
    }
}
