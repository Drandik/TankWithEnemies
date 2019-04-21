using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    public readonly EnemySpawnerConfig Config;
    public readonly EnemyType Type;

    private int currentCountEnemies;
    private int id;

    public EnemySpawner(EnemySpawnerConfig config)
    {
        Config = config;
        Type = config.Type;
        currentCountEnemies = 0;
        id = Random.Range(10, 1000);
    }

    public bool CompareSpawner(int spawnerId)
    {
        return id == spawnerId;
    }

    public void EnemyDeath()
    {
        currentCountEnemies--;
        //Debug.Log("death " + id + " count " + currentCountEnemies + " max " + Config.Count);
    }

    public bool SpawnEnemy()
    {
        bool flag = false;

        if(currentCountEnemies < Config.Count)
        {
            currentCountEnemies++;
            Transform spawnPoint = Config.spawnPoints[Random.Range(0, Config.spawnPoints.Count)];
            GameObject enemy = PoolManager.GetObject(PoolWrapperType.EnemyPool, Type.ToString(), spawnPoint.position, spawnPoint.rotation);
            enemy.GetComponent<IEnemy>().Initialize(Config.Stats, id);
            flag = true;
        }

        return flag;
    }
}
