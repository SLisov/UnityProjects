using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public GameObject[] animalPrefabsLeft;
    public GameObject[] animalPrefabsRight;
    float spawnRangeX = 20;
    float spawnRangexSides = 27;
    float spawnRangeZ = 20;

    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", 2, 3f);
        InvokeRepeating("SpawnRandomAnimalLeft", 2, 3f);
        InvokeRepeating("SpawnRandomAnimalRight", 2, 3f);
    }

    void Update()
    {

    }

    void SpawnRandomAnimal()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPosTop = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnRangeZ);
        
        Instantiate(animalPrefabs[animalIndex], spawnPosTop, animalPrefabs[animalIndex].transform.rotation);

    }

    void SpawnRandomAnimalLeft()
    {
        int animalIndex = Random.Range(0, animalPrefabsLeft.Length);
        Vector3 leftSpawnPos = new Vector3(spawnRangexSides, 0, Random.Range(-4, 6));
        Instantiate(animalPrefabsLeft[animalIndex], leftSpawnPos, animalPrefabsLeft[animalIndex].transform.rotation);
    }
    void SpawnRandomAnimalRight()
    {
        int animalIndex = Random.Range(0, animalPrefabsRight.Length);
        Vector3 rightspawnPos = new Vector3(-spawnRangexSides, 0, Random.Range(-4, 6));
        Instantiate(animalPrefabsRight[animalIndex], rightspawnPos, animalPrefabsRight[animalIndex].transform.rotation);

    }
}
