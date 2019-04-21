using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;

public class Zombie : PoolObject, IEnemy, IDeath
{
    private enum States
    {
        None,
        Move,
        Attack,
        Death
    }

    public Animator animator;
    //public float dist = 0;
    //public int Index = 0;

    private EnemySpawnerConfig.EnemyStats stats;
    private int hashAttack;
    private int hashDeath;
    private States state = States.None;
    private Health tankHealth;
    private Health ownHealth;
    private int spawnerID;
    private bool isAttacking = false;

    private IMovementStrategy movement;

    //private static int number = 0;
    //private int index = 0;

    public override void Awake()
    {
        base.Awake();
        hashAttack = Animator.StringToHash("Attack");
        hashDeath = Animator.StringToHash("Death");
        ownHealth = GetComponent<Health>();
        movement = new NavMeshMovement();
        //number++;
        //index = number;
        //Index = index;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            tankHealth = other.GetComponent<Health>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tankHealth = null;
        }
    }

    private void Update()
    {
        //dist = agent.remainingDistance;
        switch (state)
        {
            case States.None:
                StartMove();
                break;
            case States.Move:
                if (Vector3.Distance(Tank.TankTransform.position, transform.position) <= stats.DistanceToAttack)
                    StartAttack();
                else
                    StartMove();
                break;
            case States.Attack:
                if (Vector3.Distance(Tank.TankTransform.position, transform.position) > stats.DistanceToAttack)
                    StartMove();
                break;
            case States.Death:
                break;
        }
    }

    private void FixedUpdate()
    {
        movement.FixedUpdateMove();
    }

    private void StartAttack()
    {
        state = States.Attack;
        movement.Stop();
        animator.SetBool(hashAttack, true);

        if (!isAttacking)
        {
            StartCoroutine(AttackCoroutine(stats.AttackDelay));
        }
    }

    private IEnumerator AttackCoroutine(float attackDelay)
    {
        isAttacking = true;
        while (tankHealth != null)
        {
            tankHealth.TakeDamage(stats.Damage);
            yield return new WaitForSeconds(attackDelay);
        }
        isAttacking = false;
    }

    protected virtual void StartMove()
    {
        state = States.Move;
        SetDestionation(Tank.TankTransform.position);
        animator.SetBool(hashAttack, false);
        //print("startmove " + index + " tank " + Tank.TankTransform.position + " its " + transform.position +
        //    " dist " + Vector3.Distance(Tank.TankTransform.position, transform.position) + " dist agent " + agent.remainingDistance + " agent dest " + agent.destination);
    }

    private void SetDestionation(Vector3 position)
    {
        movement.SetDestination(position);
    }

    public override void ReturnToPool()
    {
        state = States.None;
        base.ReturnToPool();
    }

    public void Initialize(EnemySpawnerConfig.EnemyStats enemyStats, int spawnerId)
    {
        //print("Initialize " + index + " " + state);
        stats = enemyStats;
        movement.Inintialize(gameObject, stats.Speed);

        if (ownHealth == null)
            ownHealth = GetComponent<Health>();

        ownHealth.Initialize(stats.MaxHealth, stats.Defence);
        spawnerID = spawnerId;
    }

    public void Death()
    {
        if (state == States.Death)
            return;

        state = States.Death;
        tankHealth = null;
        movement.Stop();
        animator.SetBool(hashAttack, false);
        animator.SetBool(hashDeath, true);
        GameController.Instance.SetScore();
        EnemyManager.EnemyDeath(spawnerID);
        Invoke("ReturnToPool", 2);
    }

    public void ChangeMovementStrategy(IMovementStrategy newMovement)
    {
        movement = newMovement;
    }
}
