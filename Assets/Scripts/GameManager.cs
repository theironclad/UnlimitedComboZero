using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public PersistentStatsController lStats = new PersistentStatsController();
    public GameObject onScreenMenu;

    public ParticleSystem pps;
    public AudioMixer am;
    private Rigidbody[] rbs;
    private Vector3 playerPos;
    private MusicManager mm;
    private PointsController pc;
    private SpawnerController spawnCtrl;
    private GameManager gmi;

    public bool inGame=false;
    public bool alreadyInGame = false;

	private void Awake()
	{
        if(Instance == null){
            Instance = this;
        }else if(Instance!=this){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}
	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;

        GetMM();
        FindPointsController();
        LoadGame("Player");
        SetVolumes();
        CalculateCurrentStage();

    }

    public void GameOverActive(){
        spawnCtrl = FindObjectOfType<SpawnerController>();
        spawnCtrl.spawningEnemies = false;
        FindPointsController();
        pc.CalculatePoints();
        FindGameOver();
        onScreenMenu.SetActive(true);
        mm.musicPlaying = false;
        mm.audioSource.Stop();
        EnemyController[] ec = GameObject.Find("EnemiesContainer").GetComponentsInChildren<EnemyController>();
        foreach (EnemyController e in ec)
        {
            e.canShoot = false;
        }
        if (mm.audioSource.isPlaying)
        {
            mm.audioSource.Stop();
        }
    }

    void FindGameOver()
    {
        onScreenMenu = GameObject.Find("OnScreenMenu").transform.GetChild(0).gameObject;
    }

    void FindPointsController(){
        pc = FindObjectOfType<PointsController>();
    }

    public void SetPSDefaults(){
        GameManager.Instance.lStats.playerStartingHP = 10;
        GameManager.Instance.lStats.currentPlayerHP = 10;
        GameManager.Instance.lStats.playerHPAP = 1;
        GameManager.Instance.lStats.playerRegenAP = 1;
        GameManager.Instance.lStats.atPoints = 0;
        GameManager.Instance.lStats.spendablePoints = 0;
        GameManager.Instance.lStats.spFactor = 5;
        GameManager.Instance.lStats.spFactorAP = 1;
        GameManager.Instance.lStats.playerSSAP = 1;
        GameManager.Instance.lStats.spawnAddAP = 1;

        GameManager.Instance.lStats.currentGunFR = 2;
        GameManager.Instance.lStats.currentGunPD = 3;
        GameManager.Instance.lStats.currentGunPS = 5;
        GameManager.Instance.lStats.gunFRAP = 1;
        GameManager.Instance.lStats.gunPDAP = 1;
        GameManager.Instance.lStats.gunPSAP = 1;
        GameManager.Instance.lStats.spreadUnlocked = false;

        GameManager.Instance.lStats.spreadFRAP = 1;
        GameManager.Instance.lStats.spreadPDAP = 1;
        GameManager.Instance.lStats.spreadPSAP = 1;

        GameManager.Instance.lStats.cost_gunFRAP = 1;
        GameManager.Instance.lStats.cost_gunPDAP = 1;
        GameManager.Instance.lStats.cost_gunPSAP = 1;
        GameManager.Instance.lStats.cost_spreadUnlock = 100;
        GameManager.Instance.lStats.cost_spreadFRAP = 1;
        GameManager.Instance.lStats.cost_spreadPDAP = 1;
        GameManager.Instance.lStats.cost_spreadPSAP = 1;
        GameManager.Instance.lStats.cost_playerHPAP = 1;
        GameManager.Instance.lStats.cost_playerSSAP = 1;
        GameManager.Instance.lStats.cost_spFactorAP = 1;
        GameManager.Instance.lStats.cost_playerRegenAP = 1;
        GameManager.Instance.lStats.cost_comboTimerMaxAP = 1;
        GameManager.Instance.lStats.cost_spawnAddAP = 1;

        GameManager.Instance.lStats.currentStage = 1;
        GameManager.Instance.lStats.startingStage = 1;
        GameManager.Instance.lStats.comboTimerMaxAP = 1;


        GameManager.Instance.lStats.enemiesDefeated = 0;
    }

    public void RestartGame(){
        
        lStats.currentPoints = 0;
        CalculateCurrentStage();
        lStats.spThisRound=0;

        lStats.currentCombo = 0;
        lStats.currentComboTimer = 0;

    }

    public static void SaveGame(string type){
        string filepath = Application.persistentDataPath + "/" + type + ".dat";

        if (File.Exists(filepath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filepath, FileMode.Open);

            bf.Serialize(file, GameManager.Instance.lStats);

            file.Close();
        }else{
            print("Save file does not exist, creating");
            StreamWriter newSave = new StreamWriter(filepath);
            newSave.Close();
            GameManager.Instance.SetPSDefaults();
            print("Created. Verifying save");
            SaveGame(type);
        }
    }

    public static void LoadGame(string type){
        string filepath = Application.persistentDataPath + "/" + type + ".dat";

        if (File.Exists(filepath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filepath, FileMode.Open);

            GameManager.Instance.lStats = (PersistentStatsController)bf.Deserialize(file);

            file.Close();
        }else{
            print("File does not exist. Creating");
            SaveGame(type);
        }
    }

    //This needs to be simplified. Strongly discouraged to use reflection for things
    public void ModifyStat(string statName, int modifier){
        int statChange = (int)lStats.GetType().GetField(statName).GetValue(lStats);
        print(statChange);
        System.Reflection.FieldInfo fI = lStats.GetType().GetField(statName);
        print(fI);
        fI.SetValue(lStats, (statChange +=modifier));
    }

    public int GetStat(string statname){
        int stat = (int)lStats.GetType().GetField(statname).GetValue(lStats);
        return stat;
    }

    public void CalculateCurrentStage(){
        lStats.startingStage = gmi.lStats.playerSSAP;
        lStats.currentStage = lStats.startingStage;
    }

    public void SetVolumes(){
        am.SetFloat("sfxVolume", lStats.sfxVolumeSet);
        am.SetFloat("bgmVolume", lStats.bgmVolumeSet);
        am.SetFloat("masterVolume", lStats.masterVolume);
    }


    public void DestroyPlayer(PlayerController player)
    {
        GetMM();
        inGame = false;
        alreadyInGame = false;
        playerPos = player.transform.position;
        ComboController cc = GameObject.Find("Combo_Text").GetComponent<ComboController>();
        cc.enabled = !cc.enabled;
        ProjectileController[] projectiles = GameObject.Find("ProjectilesContainer").GetComponentsInChildren<ProjectileController>();
        foreach (ProjectileController p in projectiles){
            p.enabled = !p.enabled;
            p.gameObject.SetActive(false);
        }
        EnemyController[] ec = GameObject.Find("EnemiesContainer").GetComponentsInChildren<EnemyController>();
        foreach (EnemyController e in ec)
        {
            e.canShoot = false;
        }

        rbs = GameObject.Find("EnemiesContainer").GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            rb.useGravity = false;
        }

        if (mm.audioSource.isPlaying)
        {
            mm.audioSource.Stop();
        }

        Invoke("GameOverActive", 2.5f);
        SpawnPlayerDeathParticles();
        gmi.lStats.playerLivesLost++;
        Destroy(player.gameObject);
    }

    public void SpawnPlayerDeathParticles()
    {
        ParticleSystem newParticles = Instantiate(pps, playerPos, Quaternion.identity);
        float desTime = newParticles.main.duration;
        EnemyProjectile[] projectiles = GameObject.Find("ProjectilesContainer").GetComponentsInChildren<EnemyProjectile>();
        foreach (EnemyProjectile projectile in projectiles)
        {
            Destroy(projectile.gameObject);
        }
        Destroy(newParticles, desTime);
    }

    public void CallGO()
    {
        GameOverActive();
    }

    public void GetMM(){
        mm = FindObjectOfType<MusicManager>();
    }

    public void DestroySounds(){
        GameObject mpp = GameObject.Find("MusicManager");
        GameObject spp = GameObject.Find("SFXManager");
        Destroy(mpp);
        Destroy(spp);
    }

}
