using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public readonly WeaponConfig Config;
    public readonly WeaponType Type;

    public Weapon(WeaponConfig config)
    {
        Config = config;
        Type = config.Type;
    }

    public void Shoot(Transform shootPoint)
    {
        IShell shell = PoolManager.GetObject(PoolWrapperType.ShellPool, Config.Type.ToString(), shootPoint.position, shootPoint.rotation)
            .GetComponent<IShell>();
        shell.Initialize(Config.ShootStats);
        shell.MovementLogic(shootPoint.forward);
    }
}
