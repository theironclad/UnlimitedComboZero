using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour {

    private MusicManager mm;
    private GameManager gmi;
    private Text csText;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += LevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelFinishedLoading;
    }

    // Use this for initialization
    void Start () {
        gmi = GameManager.Instance;
        csText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        csText.text = "STAGE " + gmi.lStats.currentStage;
	}

    void LevelFinishedLoading(Scene scene, LoadSceneMode mode){
        mm.PlaySceneMusic(scene);
    }
}
