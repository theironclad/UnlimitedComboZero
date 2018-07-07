using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public ProjectileController projectilePrefab;
    public Transform muzzle;

    public bool isFiring;
    public bool spreadFire;
    public float projectileSpeed;
    public float fireRate;
    public float spreadFireRate;
    public float dmg;

    public float shotCounter;
    public AudioClip playerShot;

    private AudioSource aSource;
    private ProjectilesContainer pc;
    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        aSource = GetComponent<AudioSource>();
        pc = FindObjectOfType<ProjectilesContainer>();
        muzzle = transform;
        Invoke("LoadGun", 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
        shotCounter -= Time.deltaTime;
        if (isFiring)
        {
            Fire();
        }

        if (spreadFire)
        {
            FireSpread();
        }

        if (shotCounter<-1.0f)
        {
            shotCounter = -.5f;
        }
    }

    public void Fire(){
        
        if (shotCounter <= 0f)
        {
            aSource.clip = playerShot;
            aSource.Play();

            shotCounter = fireRate;
            ProjectileController newBullet = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
            newBullet.transform.SetParent(pc.transform, true);
            newBullet.speed = projectileSpeed;
            newBullet.dmg = dmg;
        }
    }

    public void LoadNewLevelGun(){
        projectileSpeed = gmi.lStats.currentGunPS;
        fireRate = gmi.lStats.currentGunFR;
        dmg = gmi.lStats.currentGunPD;
    }

    public void LoadGun(){
        if (gmi.inGame)
        {
            LoadNewLevelGun();
        }else if(!gmi.inGame){
            projectileSpeed = gmi.lStats.gunPSAP * gmi.lStats.currentGunPS;
            fireRate = gmi.lStats.currentGunFR/gmi.lStats.gunFRAP;
            spreadFireRate = gmi.lStats.currentGunFR / gmi.lStats.spreadFRAP;
            dmg = gmi.lStats.gunPDAP * gmi.lStats.currentGunPD;
            print("Gun damage : " + dmg);
        }
    }

    public void FireSpread(){
        
        if (shotCounter<=0f)
        {
            aSource.clip = playerShot;
            aSource.Play();

            shotCounter = spreadFireRate;

            ProjectileController newBullet1 = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
            ProjectileController newBullet2 = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
            ProjectileController newBullet3 = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
            ProjectileController newBullet4 = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
            ProjectileController newBullet5 = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);

            ProjectileController[] spreadBullets = new ProjectileController[5] { newBullet1, newBullet2, newBullet3, newBullet4, newBullet5 };

            foreach (ProjectileController bullet in spreadBullets)
            {
                bullet.transform.SetParent(pc.transform, true);
                bullet.speed = gmi.lStats.spreadPSAP;
                bullet.dmg = gmi.lStats.spreadPDAP;
            }

            newBullet1.transform.Rotate(0, -30, 0);
            newBullet2.transform.Rotate(0, -20, 0);
            newBullet4.transform.Rotate(0, 20, 0);
            newBullet5.transform.Rotate(0, 30, 0);
        }
    }
}
