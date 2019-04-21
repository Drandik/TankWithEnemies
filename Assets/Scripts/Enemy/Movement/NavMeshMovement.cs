using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement : IMovementStrategy
{
    private NavMeshAgent agent;
    private float ownSpeed;

    public void SetDestination(Vector3 position)
    {
        if (agent != null)
            agent.SetDestination(position);
    }

    public void Stop()
    {
        agent.isStopped = true;
        agent.ResetPath();
    }

    public void Inintialize(GameObject go, float speed)
    {
        agent = go.GetComponent<NavMeshAgent>();
        ownSpeed = speed;
        agent.speed = ownSpeed;
    }

    public void FixedUpdateMove()
    {
       
    }
}
