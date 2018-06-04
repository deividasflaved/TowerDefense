using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour {

    public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    private float timeBetweenEnemies = 0.5f;

    private int waveNumber = 1;

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
    }
    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
        waveNumber++;
    }
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab,spawnPoint.position, spawnPoint.rotation);
    }
}
