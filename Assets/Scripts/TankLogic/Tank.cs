using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour, IDeath
{
    public static Transform TankTransform { get; private set; }
    public float StartingHealth = 100f;
    public float Defence = .35f;

    private TankMoveLogic movement;
    private Health health;

    private void Awake()
    {
        TankTransform = transform;
        movement = GetComponent<TankMoveLogic>();
        health = GetComponent<Health>();
        if (health != null)
            health.Initialize(StartingHealth, Defence);
    }

    public void Death()
    {
        GameController.Instance.GameOver();
    }
}
