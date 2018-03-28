using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour {

    public GameObject gameOverMenu;
    public GameObject shopMenu;

    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowGameOver(){
        GetOnScreenMenus();
        gameOverMenu.SetActive(true);
        shopMenu.SetActive(false);
    }

    public void ShowShop(){
        GetOnScreenMenus();
        gameOverMenu.SetActive(false);
        shopMenu.SetActive(true);
    }

    public void RetryGame(){
        gmi.RestartGame();
        GameManager.SaveGame("Player");
        SceneManager.LoadScene("01_MainGame");
    }

    public void QuitGame(){
        gmi.RestartGame();
        GameManager.SaveGame("Player");
        SceneManager.LoadScene("00_StartMenu");
    }

    void GetOnScreenMenus(){
        gameOverMenu = GameObject.Find("OnScreenMenu").transform.GetChild(0).gameObject;
        shopMenu = GameObject.Find("OnScreenMenu").transform.GetChild(1).gameObject;
    }
}
