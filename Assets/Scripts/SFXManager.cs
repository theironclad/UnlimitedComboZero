using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    public SFXManager Instance;
    public AudioSource aSource;
    public AudioClip enemyDeath;
    public AudioClip playerDeath;
    public AudioClip enemyAppear;
    public AudioClip playerHit;

	void Awake()
	{
        if (Instance==null)
        {
            Instance = this;
        }else if(Instance!=this){
            Destroy(gameObject);
        }DontDestroyOnLoad(gameObject);
        aSource = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //if (Instance == null)
        //{
        //    Instance = this;
        //}
        //else if (Instance != this)
        //{
        //    Destroy(gameObject);
        //}
	}

    public void PlayPlayerDeath(){
        aSource.clip = playerDeath;
        aSource.Play();
    }

    public void PlayEnemyDeath(){
        aSource.clip = enemyDeath;
        aSource.Play();
    }

    public void PlayEnemyAppear(){
        aSource.clip = enemyAppear;
    }

    public void PlayPlayerHit(){
        aSource.clip = playerHit;
        aSource.Play();
    }
}
