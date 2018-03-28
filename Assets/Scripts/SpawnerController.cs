using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerController : MonoBehaviour {

    public Spawner[] allSpawners;
    public int maxSpawn;
    public int totalSpawned;
    public float spawnCooldown;
    public float nextSpawn;
    //[HideInInspector]
    public int inScene = 0;

    private bool allEnemiesSpawned = false;

    private EnemiesContainer ec;
    private StageController sc;
    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        ec = FindObjectOfType<EnemiesContainer>();
        allSpawners = FindObjectsOfType<Spawner>();
        sc = FindObjectOfType<StageController>();
        maxSpawn = gmi.lStats.currentStage * 10;
        nextSpawn = Mathf.Abs(5 - (gmi.lStats.currentStage / 50));
	}
	
	// Update is called once per frame
	void Update () {

        if (totalSpawned == maxSpawn)
        {
            allEnemiesSpawned = true;
            print("all enemies spawned : " + totalSpawned + "/" + maxSpawn);
        }

        spawnCooldown -= Time.deltaTime;

        if (spawnCooldown<=-0.5f)
        {
            spawnCooldown = -0.5f;
        }

        if (spawnCooldown <=0 &&totalSpawned<maxSpawn)
        {
            int spawnerNumber = Random.Range(0, 3);
            allSpawners[spawnerNumber].SpawnEnemy();
            totalSpawned++;

            spawnCooldown = nextSpawn;
        }

        //inScene = ec.transform.childCount+1;
        inScene = ec.transform.childCount;

        StageClear();
	}

    void StageClear(){
        if (allEnemiesSpawned && inScene <=0)
        {
            print("No more enemies. Calling StageClear");
            gmi.lStats.currentStage++;
            //TODO get COMBO info into next scene, add to persistent stats.
            SceneManager.LoadScene("01_MainGame");
        }
    }
        
}
