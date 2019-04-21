using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementStrategy
{
    void SetDestination(Vector3 position);
    void Stop();
    void Inintialize(GameObject go, float speed);
    void FixedUpdateMove();
}
