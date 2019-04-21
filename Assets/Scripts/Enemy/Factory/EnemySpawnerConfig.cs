using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnerConfig
{
    [System.Serializable]
    public struct EnemyStats
    {
        public float Damage;
        public float Speed;
        public float MaxHealth;
        public float Defence;
        public float DistanceToAttack;
        public float AttackDelay;
    }

    public EnemyType Type;
    public EnemyStats Stats;
    public int Count;
    public List<Transform> spawnPoints;
}
