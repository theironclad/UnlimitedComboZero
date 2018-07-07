using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float autoLoadNextLevel = 2f;

    // Use this for initialization
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "00_Splash")
        {
            Invoke("LoadStartMenu", autoLoadNextLevel);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadStartMenu(){
        SceneManager.LoadScene("00_StartMenu");
    }

}
