using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicMovement : IMovementStrategy
{
    private Rigidbody rigidbody;
    private float movementSpeed;
    private float turnSpeed = 20;
    private Vector3 destination = Vector3.zero;

    private FastPath.Path path;
    private int currentIndex = 0;

    private void FindPath()
    {
        if (destination == Vector3.zero)
        {
            Debug.Log("FindPath vector zero");
            return;
        }
        path = new FastPath.Path(rigidbody.position, destination);
        currentIndex = 1;
    }

    public void FixedUpdateMove()
    {
        if (destination == Vector3.zero)
        {
            Debug.Log("vector zero");
            return;
        }

        if (path == null || !path.IsReady)
        {
            Debug.Log("path null");
            return;
        }

        if (!path.ValidPath || currentIndex >= path.Length)
        {
            Debug.Log("new path " + path.ValidPath);
            FindPath();
            return;
        }

        Vector3 nextPoint = path[currentIndex];

        rigidbody.MovePosition(rigidbody.position + rigidbody.transform.forward * movementSpeed * Time.deltaTime);

        float turnAngle = Vector3.Angle(rigidbody.transform.forward, (nextPoint - rigidbody.position).normalized);
        if (Vector3.Cross(rigidbody.transform.forward, (nextPoint - rigidbody.position).normalized).y < 0)
            turnAngle = -turnAngle;
        
        rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(0, turnAngle * turnSpeed * Time.deltaTime, 0));

        if (Vector3.Distance(rigidbody.position, path[currentIndex]) < 0.1f)
            currentIndex += 1;
        Debug.Log("path " + Vector3.Distance(rigidbody.position, path[currentIndex]));
        path.DrawPath();
    }

    public void Inintialize(GameObject go, float speed)
    {
        rigidbody = go.GetComponent<Rigidbody>();
        movementSpeed = speed;
        path = null;
        FastPath.FastPath.DefaultMap.OnUpdate += () => { FindPath(); };
    }

    public void SetDestination(Vector3 position)
    {
        if (destination == position)
            return;
        Debug.Log("SetDestination");
        destination = position;
        FindPath();
    }

    public void Stop()
    {
        destination = Vector3.zero;
        path = null;
    }
}
