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
    public GameObject statManager;
    private MusicManager mm;
    private GameManager gmi;
    public MenuController mc;

	// Use this for initialization
	void Start () {
        mm = FindObjectOfType<MusicManager>();
        gmi = GameManager.Instance;
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
        statManager.SetActive(false);
    }

    public void ShowPlayerShop()
    {
        GetMenusAndSubMenus();
        playerShop.SetActive(true);
        gunShop.SetActive(false);
        secretShop.SetActive(false);
        statManager.SetActive(false);
    }

    public void ShowStatManager(){
        GetMenusAndSubMenus();
        playerShop.SetActive(false);
        gunShop.SetActive(false);
        secretShop.SetActive(false);
        StatManager sm = statManager.GetComponentInChildren<StatManager>();
        print(sm);
        //statManager.GetComponentInChildren<StatManager>().statString = transform.parent.name;
        sm.statString = transform.parent.name;
        statManager.SetActive(true);
    }


    public void ShowSecretShop()
    {
        GetMenusAndSubMenus();
        playerShop.SetActive(false);
        gunShop.SetActive(false);
        secretShop.SetActive(true);
        statManager.SetActive(false);
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
        //gameOverMenu = GameObject.Find("OnScreenMenu").transform.GetChild(0).gameObject;
        gameOverMenu = mc.topMenus[0];
        //shopMenu = GameObject.Find("OnScreenMenu").transform.GetChild(1).gameObject;
        shopMenu = mc.topMenus[1];
        pauseMenu = mc.topMenus[2];
        optionsMenu = mc.topMenus[3];

    }

    void GetShopMenus(){
        if (shopMenu)
        {
            //GameObject shopPanel = shopMenu.transform.Find("Shop_Panel").gameObject;
            //print(shopPanel);
            playerShop = mc.midMenus[2];
            gunShop = mc.midMenus[1];
            secretShop = mc.midMenus[3];
            statManager = mc.midMenus[0];
        }
    }

    void CloseMidMenus(){
        foreach(GameObject go in mc.midMenus){
            go.SetActive(false);
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
