using UnityEngine;

public abstract class PoolObject: MonoBehaviour
{
    private GameObject _gameObject;
    private bool isSetup = false;

    public bool CouldUse { get; protected set; }

    public virtual void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        if (isSetup)
            return;

        CouldUse = true;
        isSetup = true;
        _gameObject = gameObject;
    }

    public GameObject GetGameobject()
    {
        Setup();
        return _gameObject;
    }

    public string GetName()
    {
        Setup();
        return _gameObject.name;
    }

    public virtual void ReturnToPool()
    {
        _gameObject.SetActive(false);
    }
}
