using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyManager
{
    private static EnemySpawnerFactory factory;

    private static int maxEnemiesCount;
    private static int currentEnemiesCount;
    private static float spawnDelay = 2;
    private static List<EnemySpawner> spawners = new List<EnemySpawner>();
    private static Coroutine spawnCoroutine = null;

    public static void Initialize(List<EnemySpawnerConfig> newEnemySpawners, int enemiesCount, float spawnTime)
    {

        spawnDelay = spawnTime;
        maxEnemiesCount = enemiesCount;
        currentEnemiesCount = 0;
        factory = new EnemySpawnerFactory(newEnemySpawners);
        spawners = factory.GetSpawners();
        SpawnEnemies();
    }

    public static void EnemyDeath(int spawnerId)
    {
        currentEnemiesCount--;
        foreach(var enemySpawner in spawners)
            if(enemySpawner.CompareSpawner(spawnerId))
                enemySpawner.EnemyDeath();
        SpawnEnemies();
    }

    private static void SpawnEnemies()
    {
        if (spawnCoroutine == null)
            spawnCoroutine = RoutineRunner.Instance.StartCoroutine(SpawnCoroutine());
    }

    private static IEnumerator SpawnCoroutine()
    {
        int index = 0;
        while (currentEnemiesCount < maxEnemiesCount)
        {
            do
            {
                index = Random.Range(0, spawners.Count);
                currentEnemiesCount++;
                yield return new WaitForSeconds(spawnDelay);
            }
            while (spawners[index].SpawnEnemy());

            currentEnemiesCount--;
        }
        spawnCoroutine = null;
    }
}
