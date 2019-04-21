using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using FastPath;

public class TestControl : MonoBehaviour
{
    public FastPath.Generator.Config config;
    public Transform start;
    public Transform end;
    public Zombie enemy;
    public Transform destination;
    PhysicMovement movement;

    private void Awake()
    {
        FastPath.FastPath.DefaultMap = FastPath.FastPath.Generate(config);

        movement = new PhysicMovement();
        //movement.Inintialize(enemy.gameObject, 3);
        //movement.SetDestination(destination.position);
        enemy.ChangeMovementStrategy(movement);

        enemy.Initialize(new EnemySpawnerConfig.EnemyStats() { AttackDelay = .5f, Damage = 30, Defence = .3f, DistanceToAttack = 1, MaxHealth = 100, Speed = 3 }, 123);
    }

    private void Update()
    {
        //FastPath.FastPath.DrawMapInEditor(FastPath.FastPath.DefaultMap);
    }
}
