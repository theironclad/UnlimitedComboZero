using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public ProjectileController projectilePrefab;
    public Transform muzzle;

    public bool isFiring;
    public float projectileSpeed;
    public float fireRate;
    public float dmg;

    public float shotCounter;

    private ProjectilesContainer pc;
    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
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

        if (shotCounter<-1.0f)
        {
            shotCounter = -.5f;
        }
    }

    public void Fire(){
        
        if (shotCounter <= 0f)
        {
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
            dmg = gmi.lStats.gunPDAP * gmi.lStats.currentGunPD;
            print("Gun damage : " + dmg);
        }
    }
}
