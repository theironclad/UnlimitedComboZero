using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public MusicManager Instance;
    public AudioClip[] levelMusicArray;
    public AudioSource audioSource;

    public bool musicPlaying = false;

	private void Awake()
	{
        if (Instance==null)
        {
            Instance = this;
        }else if(Instance!=this){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

	}

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	
	void Update () {
        
	}
    
    public void PlaySceneMusic(Scene scene){
        string sceneName = scene.name;
        switch (sceneName)
        {
            case "00_Splash":
                audioSource.clip = levelMusicArray[0];
                break;
            case "00_StartMenu":
                audioSource.clip = levelMusicArray[1];
                break;
            case "01_MainGame" :
                audioSource.clip = levelMusicArray[1];
                audioSource.loop = true;
                audioSource.Play();
                break;
            default:
                break;
        }
        if (sceneName == "01_MainGame" )
        {
            audioSource.clip = levelMusicArray[1];
        }
    }

    void PrintSceneName(){
        string sceneName = SceneManager.GetActiveScene().name;
        print(sceneName);
    }
}
