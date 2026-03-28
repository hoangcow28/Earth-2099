using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenmySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenSpawns = 2f;
    [SerializeField] private int maxEnemies = 14;
    private int currentEnemies = 0;

    void Start()
    {
        StartCoroutine(SpawnEnenmyCoroutine());
    }
   
    private IEnumerator SpawnEnenmyCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);

            if (currentEnemies >= maxEnemies) continue;

            GameObject enemy = enemies[Random.Range(0, enemies.Length)];
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject obj = Instantiate(enemy, spawnPoint.position, Quaternion.identity);
            obj.GetComponent<Enemy>().Init(this);
            currentEnemies++; 
        }
    }
    public void EnemyDied()
    {
        currentEnemies--;
    }

}
