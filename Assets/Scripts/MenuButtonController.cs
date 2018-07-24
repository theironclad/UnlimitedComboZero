using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour {

    public GameObject gameOverMenu;
    public GameObject shopMenu;
    public GameObject playerShop;
    public GameObject gunShop;
    public GameObject secretShop;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    private MusicManager mm;
    private GameManager gmi;

	// Use this for initialization
	void Start () {
        mm = FindObjectOfType<MusicManager>();
        gmi = GameManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void ShowGameOver(){
        print("Gameover showing");
        GetOnScreenMenus();
        gameOverMenu.SetActive(true);
        shopMenu.SetActive(false);
    }

    public void ShowShop(){
        GetOnScreenMenus();
        gameOverMenu.SetActive(false);
        shopMenu.SetActive(true);
        playerShop.SetActive(true);
    }

    public void ShowGunShop(){
        GetOnScreenMenus();
        GetShopMenus();
        playerShop.SetActive(false);
        gunShop.SetActive(true);
        if (gmi.lStats.spreadUnlocked)
        {
            gunShop.transform.Find("SpreadGun_Button").gameObject.SetActive(true);
        }
        secretShop.SetActive(false);
    }

    public void ShowPlayerShop()
    {
        GetOnScreenMenus();
        GetShopMenus();
        playerShop.SetActive(true);
        gunShop.SetActive(false);
        secretShop.SetActive(false);
    }

    public void ShowSecretShop()
    {
        GetOnScreenMenus();
        GetShopMenus();
        playerShop.SetActive(false);
        gunShop.SetActive(false);
        secretShop.SetActive(true);
    }

    public void ShowPause(){
        GetOnScreenMenus();
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(false);
            Time.timeScale = 1;
            return;
        }else{
            pauseMenu.SetActive(true);
            optionsMenu.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public void ShowOptions(){
        GetOnScreenMenus();
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void RetryGame(){
        gmi.RestartGame();
        GameManager.SaveGame("Player");
        mm.audioSource.Play();
        SceneManager.LoadScene("01_MainGame");
    }

    public void QuitGame(){
        gmi.DestroySounds();
        gmi.RestartGame();
        GameManager.SaveGame("Player");
        SceneManager.LoadScene("00_StartMenu");
    }

    void GetOnScreenMenus(){
        gameOverMenu = GameObject.Find("OnScreenMenu").transform.GetChild(0).gameObject;
        shopMenu = GameObject.Find("OnScreenMenu").transform.GetChild(1).gameObject;
        pauseMenu = GameObject.Find("OnScreenMenu").transform.GetChild(2).gameObject;
        optionsMenu = GameObject.Find("OnScreenMenu").transform.GetChild(3).gameObject;

    }

    void GetShopMenus(){
        if (shopMenu)
        {
            GameObject shopPanel = shopMenu.transform.Find("Shop_Panel").gameObject;
            print(shopPanel);
            playerShop = shopPanel.transform.GetChild(0).gameObject;
            gunShop = shopPanel.transform.GetChild(1).gameObject;
            secretShop = shopPanel.transform.GetChild(2).gameObject;
        }
    }

    public void ShowDefaultGunShop(){
        GetMenusAndSubMenus();
        gunShop.transform.GetChild(0).gameObject.SetActive(true);
        gunShop.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void ShowSpreadGunShop(){
        GetMenusAndSubMenus();
        gunShop.transform.GetChild(0).gameObject.SetActive(false);
        gunShop.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void HidePauseOptions(){
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void GetMenusAndSubMenus(){
        GetOnScreenMenus();
        GetShopMenus();
    }

    public void StartGame(){
        SceneManager.LoadScene("01_MainGame");
    }

    public void ResetSaveFile(){
        gmi.SetPSDefaults();
    }
}
