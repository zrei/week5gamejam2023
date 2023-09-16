using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemy;

    public List<Transform> spawnPoints;

    public float spawnInterval;
    private float nextSpawnTime;
    
    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime > nextSpawnTime)
        {
            nextSpawnTime += spawnInterval;
            spawnInterval -= 0.1f;
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Instantiate(enemy, randomSpawnPoint);
    }
}
