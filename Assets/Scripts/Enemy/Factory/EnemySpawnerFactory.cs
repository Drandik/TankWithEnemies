using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerFactory
{
    private readonly List<EnemySpawnerConfig> enemySpawners = new List<EnemySpawnerConfig>();

    public EnemySpawnerFactory(List<EnemySpawnerConfig> spawners)
    {
        enemySpawners = spawners;
    }

    public List<EnemySpawner> GetSpawners()
    {
        List<EnemySpawner> spawners = new List<EnemySpawner>();
        foreach (var config in enemySpawners)
            spawners.Add(new EnemySpawner(config));
        return spawners;
    }
}
