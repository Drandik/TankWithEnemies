using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shell : PoolObject, IShell
{
    public ParticleSystem explosionParticles;
    public float maxLifeTime = 2f;

    private Rigidbody _rigidBody;
    protected WeaponConfig.ShellStats stats;
    private NavMeshAgent enemyAgent;

    public override void Awake()
    {
        base.Awake();
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void MovementLogic(Vector3 forward)
    {
        _rigidBody.velocity = stats.Speed * forward;
    }

    public void Initialize(WeaponConfig.ShellStats _stats)
    {
        stats = _stats;
    }

    private void OnEnable()
    {
        CouldUse = false;
        enemyAgent = null;
        Invoke("ReturnToPool", maxLifeTime);
    }

    private void OnDisable()
    {
        CancelInvoke("ReturnToPool");
        if (enemyAgent != null)
            enemyAgent.enabled = true;
    }

    protected virtual void Hit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health enemyHealth = other.GetComponent<Health>();
            enemyHealth.TakeDamage(stats.Damage);

            Rigidbody targetRigidbody = other.GetComponent<Rigidbody>();

            // If they don't have a rigidbody, go on to the next collider.
            if (!targetRigidbody)
                return;
            targetRigidbody.isKinematic = false;
            enemyAgent = other.GetComponent<NavMeshAgent>();
            enemyAgent.enabled = false;
            // Add an explosion force.
            targetRigidbody.AddExplosionForce(stats.ExplosionForce, transform.position, stats.ExplosionRadius);
            targetRigidbody.isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Hit(other);

        explosionParticles.transform.parent = null;
        explosionParticles.Play();
        ParticleSystem.MainModule mainModule = explosionParticles.main;
        Invoke("ReturnParticle", mainModule.duration);
        ReturnToPool();
    }

    private void ReturnParticle()
    {
        CouldUse = true;
        explosionParticles.transform.parent = _rigidBody.transform;
    }
}
