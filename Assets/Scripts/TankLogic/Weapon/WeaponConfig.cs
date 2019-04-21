using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponConfig
{
    public WeaponType Type;
    public ShellStats ShootStats;
    public Color Color;
    public float DelayBetweenShoot;

    [System.Serializable]
    public struct ShellStats
    {
        public float Damage;
        public float Speed;
        public float ExplosionRadius;
        public LayerMask EnemyMask;
        public float ExplosionForce;
    }
}
