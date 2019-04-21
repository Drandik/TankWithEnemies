using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSettings : Singleton<WeaponSettings>
{
/*    [SerializeField] */public List<WeaponConfig> weapons;

    public List<WeaponConfig> GetWeapons()
    {
        return weapons;
    }
}
