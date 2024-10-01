using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject enemySpawn;
    public GameObject hardEnemySpawn;
    public GameObject[] powerUpPrefabs;
    public GameObject theBoss;
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;

    void Start()
    {
        SpawnEnemyWaves(waveNumber);
        Instantiate(powerUpPrefabs[0], GenerateSpawnPosition(), powerUpPrefabs[0].transform.rotation);
    }


    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWaves(waveNumber);
            int prefubToSpawn = Random.Range(0, 3);
            Instantiate(powerUpPrefabs[prefubToSpawn], GenerateSpawnPosition(), powerUpPrefabs[prefubToSpawn].transform.rotation);
        }
    }

    void SpawnEnemyWaves(int enemiesToSpawn) 
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            if (Random.Range(1,3) == 1 && enemiesToSpawn != 7)
            {
                Instantiate(enemySpawn, GenerateSpawnPosition(), enemySpawn.transform.rotation);
            }
            else if(enemiesToSpawn != 7)
            {
                Instantiate(hardEnemySpawn, GenerateSpawnPosition(), enemySpawn.transform.rotation);
            }
            if (enemiesToSpawn == 7)
            {
                waveNumber = 0;
                enemiesToSpawn = 1;
                Instantiate(theBoss, GenerateSpawnPosition(), enemySpawn.transform.rotation);
            }
        }
    }

    private Vector3 GenerateSpawnPosition() 
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

}
