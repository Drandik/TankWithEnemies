using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    List<PoolObject> objects;
    Transform objectsParent;

    public void Initialize(int count, PoolObject sample, Transform objects_parent)
    {
        objects = new List<PoolObject>();
        objectsParent = objects_parent;
        for (int i = 0; i < count; i++)
        {
            AddObject(sample, objects_parent);
        }
    }

    public PoolObject GetObject()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].GetGameobject().activeInHierarchy == false && objects[i].CouldUse)
            {
                return objects[i];
            }
        }
        AddObject(objects[0], objectsParent);
        return objects[objects.Count - 1];
    }

    void AddObject(PoolObject sample, Transform objects_parent)
    {
        GameObject temp;
        temp = GameObject.Instantiate(sample.GetGameobject());
        temp.name = sample.GetName();
        temp.transform.SetParent(objects_parent);
        objects.Add(temp.GetComponent<PoolObject>());
        temp.SetActive(false);
    }
}
