using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WeaponFactory
{
    private readonly List<WeaponConfig> weapons = new List<WeaponConfig>();

    public WeaponFactory(List<WeaponConfig> arsenal)
    {
        weapons = arsenal;
    }

    private int currentWeaponIndex = -1;

    public Weapon GetNextWeapon()
    {
        if (currentWeaponIndex + 1 <= weapons.Count - 1)
            currentWeaponIndex += 1;
        else
            currentWeaponIndex = 0;

        return new Weapon(weapons[currentWeaponIndex]);
    }

    public Weapon GetPreviousWeapon()
    {
        if (currentWeaponIndex - 1 < 0)
            currentWeaponIndex = weapons.Count - 1;
        else
            currentWeaponIndex -= 1;

        return new Weapon(weapons[currentWeaponIndex]);
    }
}
