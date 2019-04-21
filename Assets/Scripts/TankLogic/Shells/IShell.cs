using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShell
{
    void MovementLogic(Vector3 forward);
    void Initialize(WeaponConfig.ShellStats stats);
}
