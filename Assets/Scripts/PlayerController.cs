﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public float playerHP = 10f;   
    public float regenTimer = 0;

    private Camera mainCam;
    private GunController gun;
    private GameObject pauseMenu;
    private GameObject optionsMenu;
    private GameManager gmi;

	
	void Start () {
        gmi = GameManager.Instance;
        gmi.player = gameObject;
        mainCam = FindObjectOfType<Camera>();
        gun = GetComponentInChildren<GunController>();
        pauseMenu = GameObject.Find("OnScreenMenu").transform.GetChild(2).gameObject;
        optionsMenu = GameObject.Find("OnScreenMenu").transform.GetChild(3).gameObject;
        Invoke("LoadPlayer", 0.05f);
    }
		
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
               
        if (Input.GetMouseButtonDown(0)&&playerHP>0)
        {
            gun.isFiring=true;
        }

        if(Input.GetMouseButtonUp(0)){
            gun.isFiring = false;
        }

        if (Input.GetMouseButtonDown(1)&&gmi.lStats.spreadUnlocked && playerHP>0)
        {
            gun.spreadFire = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            gun.spreadFire = false;
        }

        if (gmi.lStats.playerRegenAP>1 && playerHP >0)
        {
            regenTimer += Time.deltaTime;
            if (regenTimer >=1 && playerHP < gmi.lStats.currentPlayerHP)
            {
                playerHP += Mathf.RoundToInt(.1f * gmi.lStats.playerRegenAP);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeSelf && !optionsMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                return;
            }
            if (optionsMenu.activeSelf && pauseMenu.activeSelf)
            {
                optionsMenu.SetActive(false);
                return;
            }

            if (optionsMenu.activeSelf)
            {
                optionsMenu.SetActive(false);
                pauseMenu.SetActive(true);
                return;
            }

            if (pauseMenu.activeSelf)
            {
                optionsMenu.SetActive(false);
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
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
            print("LP ! in game");
            if (gmi.lStats.current_playerHPAP>0)
            {
                playerHP = gmi.lStats.playerStartingHP * gmi.lStats.current_playerHPAP;
            }else{
                playerHP = gmi.lStats.playerStartingHP * gmi.lStats.playerHPAP;
            }

            gmi.lStats.currentPlayerHP = playerHP;
            gmi.inGame = true;
        }
    }


}
