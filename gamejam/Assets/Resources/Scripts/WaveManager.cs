using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemy;

    public List<Transform> spawnPoints;

    public float spawnInterval;
    float nextSpawnTime;
    
    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime > nextSpawnTime)
        {
            nextSpawnTime += spawnInterval;
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Instantiate(enemy, randomSpawnPoint);
    }
}
