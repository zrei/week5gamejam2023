using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemy;

    public List<Transform> spawnPoints;
    private List<Transform> unusedSpawnPoints = new List<Transform>();

    public float spawnInterval;
    private float nextSpawnTime;
    private int spawnCount;
    
    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.fixedTime;

        foreach (Transform spawnPoint in spawnPoints)
        {
            unusedSpawnPoints.Add(spawnPoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime > nextSpawnTime)
        {
            nextSpawnTime += spawnInterval;
            spawnInterval -= 0.1f;
            SpawnWave(Random.Range(1, spawnPoints.Count));
        }
    }

    void SpawnWave(int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Transform randomSpawnPoint = unusedSpawnPoints[Random.Range(0, unusedSpawnPoints.Count)];
            Instantiate(enemy, randomSpawnPoint);
            unusedSpawnPoints.Remove(randomSpawnPoint);
        }

        unusedSpawnPoints.Clear();
        foreach (Transform spawnPoint in spawnPoints)
        {
            unusedSpawnPoints.Add(spawnPoint);
        }
    }
}
