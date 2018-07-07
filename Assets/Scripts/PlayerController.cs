using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public float playerHP = 10f;

    public float regenTimer = 0;
    //private int playerLives;
    //private float stamina;

    private Camera mainCam;
    private GunController gun;

    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        mainCam = FindObjectOfType<Camera>();
        gun = GetComponentInChildren<GunController>();
        Invoke("LoadPlayer", 0.05f);
	}
	
	// Update is called once per frame
	void Update () {
        Ray camRay = mainCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(camRay,out rayLength))
        {
            Vector3 lookPoint = camRay.GetPoint(rayLength);
            Debug.DrawLine(camRay.origin, lookPoint, Color.red);

            transform.LookAt(new Vector3(lookPoint.x,transform.position.y,lookPoint.z));
        }
               
        if (Input.GetMouseButtonDown(0))
        {
            gun.isFiring=true;
        }

        if(Input.GetMouseButtonUp(0)){
            gun.isFiring = false;
        }

        if (Input.GetMouseButtonDown(1)&&gmi.lStats.spreadUnlocked)
        {
            gun.spreadFire = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            gun.spreadFire = false;
        }

        if (gmi.lStats.playerRegenAP>1)
        {
            regenTimer += Time.deltaTime;
            if (regenTimer >=1 && playerHP < gmi.lStats.playerStartingHP)
            {
                playerHP += Mathf.RoundToInt(.1f * gmi.lStats.playerRegenAP);
                print("Regen done.");
            }
        }
    }

    public void LoadNewLevelPlayer(){
        playerHP = gmi.lStats.currentPlayerHP;
    }

    public void LoadPlayer(){
        if (gmi.inGame)
        {
            LoadNewLevelPlayer();
        }else if(!gmi.inGame){
            print("New game hp" + gmi.lStats.playerStartingHP + " "+ gmi.lStats.playerHPAP);
            playerHP = gmi.lStats.playerStartingHP * gmi.lStats.playerHPAP;
            gmi.lStats.currentPlayerHP = playerHP;
            print("Current player HP = " + playerHP);
            print("Current player HP = " + gmi.lStats.currentPlayerHP);
        }
    }
}
