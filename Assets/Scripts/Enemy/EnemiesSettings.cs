using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSettings : MonoBehaviour
{
    public int EnemiesCount = 10;
    public float spawnDelay = 2;

    [SerializeField] private List<EnemySpawnerConfig> enemySpawners;

    private void Awake()
    {
        EnemyManager.Initialize(enemySpawners, EnemiesCount, spawnDelay);
    }
}
