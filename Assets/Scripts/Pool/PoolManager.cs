using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoolManager
{
    private static PoolWrapper[] poolWrappers;
    private static GameObject objectsParent;

    [System.Serializable]
    public struct PoolWrapper
    {
        public PoolWrapperType Type;
        public PoolPart[] pools;
    }

    [System.Serializable]
    public struct PoolPart
    {
        public string Type;
        public PoolObject prefab;
        public int count;
        public Pool ferula;
    }

    public static void Initialize(PoolWrapper[] newPools)
    {
        poolWrappers = newPools;
        objectsParent = new GameObject();
        objectsParent.name = "Pool";
        for (int i = 0; i < poolWrappers.Length; i++)
        {
            var objectsParentWrapper = new GameObject();
            objectsParentWrapper.transform.parent = objectsParent.transform;
            objectsParentWrapper.name = poolWrappers[i].Type.ToString();

            for (int j = 0; j < poolWrappers[i].pools.Length; j++)
                if (poolWrappers[i].pools[j].prefab != null)
                {
                    var objectsInWrapper = new GameObject();
                    objectsInWrapper.transform.parent = objectsParentWrapper.transform;
                    objectsInWrapper.name = poolWrappers[i].pools[j].Type.ToString();

                    poolWrappers[i].pools[j].ferula = new Pool();
                    poolWrappers[i].pools[j].ferula.Initialize(poolWrappers[i].pools[j].count, poolWrappers[i].pools[j].prefab, objectsInWrapper.transform);
                }
        }
    }

    public static GameObject GetObject(PoolWrapperType wrapperType, string poolType, Vector3 position, Quaternion rotation)
    {
        GameObject result = null;
        if (poolWrappers != null)
        {
            for (int i = 0; i < poolWrappers.Length; i++)
            {
                if (poolWrappers[i].Type == wrapperType)
                {
                    for (int j = 0; j < poolWrappers[i].pools.Length; j++)
                        if (string.Compare(poolWrappers[i].pools[j].Type, poolType) == 0)
                        {
                            result = poolWrappers[i].pools[j].ferula.GetObject().GetGameobject();
                            result.transform.position = position;
                            result.transform.rotation = rotation;
                            result.SetActive(true);
                            return result;
                        }
                }
            }
        }
        return result;
    }
}
