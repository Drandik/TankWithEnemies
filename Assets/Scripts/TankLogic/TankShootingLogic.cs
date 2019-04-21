using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShootingLogic : MonoBehaviour
{
    public MeshRenderer Turret;
    public Transform ShootTransform;

    private WeaponFactory factory;
    private Weapon currentWeapon;

    private float timer = 0;

    private void Awake()
    {
        factory = new WeaponFactory(WeaponSettings.Instance.GetWeapons());
        NextWeapon();
    }

    private void Update()
    {
        if (InputController.Instance.KeyW)
            NextWeapon();
        else if (InputController.Instance.KeyQ)
            PreviousWeapon();

        if (InputController.Instance.KeyX && timer >= currentWeapon.Config.DelayBetweenShoot)
        {
            timer = 0;
            Shoot();
        }
        timer += Time.deltaTime;
    }

    private void NextWeapon()
    {
        currentWeapon = factory.GetNextWeapon();
        Turret.material.color = currentWeapon.Config.Color;
    }

    private void PreviousWeapon()
    {
        currentWeapon = factory.GetPreviousWeapon();
        Turret.material.color = currentWeapon.Config.Color;
    }

    private void Shoot()
    {
        currentWeapon.Shoot(ShootTransform);
    }
}
