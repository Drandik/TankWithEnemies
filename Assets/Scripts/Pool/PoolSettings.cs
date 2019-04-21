using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSettings : MonoBehaviour
{
    [SerializeField] private PoolManager.PoolWrapper[] pools;

    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        PoolManager.Initialize(pools);
    }
}
