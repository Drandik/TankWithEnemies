using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : Shell
{
    protected override void Hit(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, stats.ExplosionRadius, stats.EnemyMask);

        // Go through all the colliders...
        for (int i = 0; i < colliders.Length; i++)
        {
            // ... and find their rigidbody.
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            // If they don't have a rigidbody, go on to the next collider.
            if (!targetRigidbody)
                continue;

            // Add an explosion force.
            targetRigidbody.AddExplosionForce(stats.ExplosionForce, transform.position, stats.ExplosionRadius);

            // Find the TankHealth script associated with the rigidbody.
            Health targetHealth = targetRigidbody.GetComponent<Health>();

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (!targetHealth)
                continue;

            // Calculate the amount of damage the target should take based on it's distance from the shell.

            // Deal this damage to the tank.
            targetHealth.TakeDamage(stats.Damage);
        }
    }
}
