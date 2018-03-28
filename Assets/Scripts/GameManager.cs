using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public PersistentStatsController lStats = new PersistentStatsController();
    public GameObject onScreenMenu;

    private PointsController pc;
    private SpawnerController spawnCtrl;
    private GameManager gmi;

    public bool inGame=false;

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
        pc = FindObjectOfType<PointsController>();
        LoadGame("Player");
        spawnCtrl = FindObjectOfType<SpawnerController>();
    }

    public void GameOverActive(){
        pc.CalculatePoints();
        FindGameOver();
        onScreenMenu.SetActive(true);
        spawnCtrl.totalSpawned = spawnCtrl.maxSpawn;
    }

    void FindGameOver()
    {
        onScreenMenu = GameObject.Find("OnScreenMenu").transform.GetChild(0).gameObject;
    }

    static void SetPSDefaults(){
        GameManager.Instance.lStats.playerStartingHP = 10;
        GameManager.Instance.lStats.playerHPAP = 1;

        GameManager.Instance.lStats.currentGunFR = 2;
        GameManager.Instance.lStats.currentGunPD = 3;
        GameManager.Instance.lStats.currentGunPS = 5;
        GameManager.Instance.lStats.gunFRAP = 1;
        GameManager.Instance.lStats.gunPDAP = 1;
        GameManager.Instance.lStats.gunPSAP = 1;

        GameManager.Instance.lStats.cost_gunFRAP = 1;
        GameManager.Instance.lStats.cost_gunPDAP = 1;
        GameManager.Instance.lStats.cost_gunPSAP = 1;
        GameManager.Instance.lStats.cost_playerHPAP = 1;

        GameManager.Instance.lStats.currentStage = 1;
    }

    public void RestartGame(){
        lStats.currentPoints = 0;
        lStats.currentStage = 1;
        lStats.spThisRound=0;
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
            SetPSDefaults();
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
}
