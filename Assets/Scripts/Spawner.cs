using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public int[] colors = new int[3];

    public EnemyController enemyPrefab;

    private EnemiesContainer ec;
    private StageController sc;
    private GameManager gmi;

	void Start()
	{
        gmi = GameManager.Instance;
        ec = FindObjectOfType<EnemiesContainer>();
        sc = FindObjectOfType<StageController>();
	}

	public void SpawnEnemy()
    {
        float spawnPointX=1;
        float spawnPointY = 5;
        float spawnPointZ=1;

        if (transform.localScale.x>1.0)
        {
            spawnPointX = Random.Range(-75,75);
            spawnPointZ = transform.position.z;
        }

        if (transform.localScale.z>1.0)
        {
            spawnPointZ = Random.Range(-75,75);
            spawnPointX = transform.position.x;
        }

        Vector3 spawnFinal = new Vector3(spawnPointX, spawnPointY, spawnPointZ);

        EnemyController newEnemy = Instantiate(enemyPrefab, spawnFinal, Quaternion.identity);
        newEnemy.transform.SetParent(ec.transform, true);


        RandomColor(gmi.lStats.currentStage);
        newEnemy.GetComponent<Renderer>().material.color = new Color32((byte)colors[0], (byte)colors[1], (byte)colors[2], 255);
        newEnemy.health = Mathf.Abs(colors[0]-254);
        newEnemy.speed = Mathf.Abs (colors[1] - 250);
        newEnemy.strength = Mathf.Abs(colors[2] - 254);
        if (newEnemy.health <= 0)
        {
            newEnemy.health = 1;
        }
        if (newEnemy.speed <= 0)
        {
            newEnemy.speed = 5;
        }
        if (newEnemy.strength<=0)
        {
            newEnemy.strength = 1;
        }
    }

   void RandomColor(int stageNumber){
        if (stageNumber>765)
        {
            colors[0] = 255;
            colors[1] = 255;
            colors[2] = 255;
            return;
        }

        //int r=255;
        //int g=255;
        //int b=255;

        int rOne = Random.Range(0, stageNumber);
        //stageNumber -= rOne;
        int rTwo = Random.Range(0, stageNumber);
        //stageNumber -= rTwo;
        int rThree = stageNumber;

        colors[0] = 255 - rOne;
        colors[1] = 255 - rTwo;
        colors[2] = 255 - rThree;

    }
}
