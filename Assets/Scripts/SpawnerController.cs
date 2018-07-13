using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerController : MonoBehaviour {

    public Spawner[] allSpawners;
    public int maxSpawn;
    public int totalSpawned;
    public float spawnCooldown;
    public float multiSpawnCooldown;
    public float nextSpawn;
    //[HideInInspector]
    public int inScene = 0;

    private bool allEnemiesSpawned = false;
    public bool spawningEnemies = false;
    private bool spawnWaiting = false;

    private ComboController cc;
    private EnemiesContainer ec;
    private StageController sc;
    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        ec = FindObjectOfType<EnemiesContainer>();
        allSpawners = FindObjectsOfType<Spawner>();
        sc = FindObjectOfType<StageController>();
        cc = FindObjectOfType<ComboController>();
        spawningEnemies = true;
        maxSpawn = gmi.lStats.currentStage * 10;
        nextSpawn = Mathf.Abs(5 - (gmi.lStats.currentStage / 50));
	}
	
	// Update is called once per frame
	void Update () {

        if (totalSpawned >= maxSpawn)
        {
            allEnemiesSpawned = true;
            spawningEnemies = false;
        }

        spawnCooldown -= Time.deltaTime;
        multiSpawnCooldown -= Time.deltaTime;

        if (spawnCooldown<=-0.5f)
        {
            spawnCooldown = -0.5f;
        }

        if (spawnCooldown <= 0 && spawningEnemies)
        {
            CallSpawn(1);
            spawnCooldown = nextSpawn;
        }

        inScene = ec.transform.childCount;
        StageClear();
	}

    void StageClear(){
        if (allEnemiesSpawned && inScene <=0 && gmi.inGame == true)
        {
            gmi.alreadyInGame = true;
            gmi.lStats.currentStage++;
            gmi.lStats.currentCombo = cc.comboCount;
            gmi.lStats.currentComboTimer = cc.comboTimer;
            GameManager.SaveGame("Player");
            SceneManager.LoadScene("01_MainGame");
        }
    }

    public void CallSpawn(int number){
        int remaining = maxSpawn - totalSpawned;
        if (number <=remaining)
        {
            totalSpawned += number;
            StartCoroutine(_spawnWait(number));
        }else{
            for (int i = 0; i < remaining; i++)
            {
                totalSpawned += remaining;
                _spawnWait(remaining);
            }
            spawningEnemies = false;
        }
    }

    public IEnumerator _spawnWait(int number){
        int i = 0;
        while(i<number){
            yield return new WaitForSeconds(.1f);
            int spawnerNumber = Random.Range(0, 4);
            allSpawners[spawnerNumber].SpawnEnemy();
            i++;
        }
    }
}
