using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private bool AutoExpand;
    private Transform Container;
    private T[] Prefab;

    private List<T> pool;

    public ObjectPool(PoolData<T> poolData)
    {
        AutoExpand = poolData.AutoExpand;
        Container  = poolData.Container;
        Prefab = poolData.Prefab;
        CreatePool(poolData.MaxCapacity, poolData.Ratio);

        Debug.Log("POOL CREATE");
    }

    public void Clear()
    {
        foreach (var item in pool)
            UnityEngine.Object.Destroy(item.gameObject);
        
        pool.Clear();

        Debug.Log("POOL DISPOSE");

        GC.Collect();
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var mono in pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
        {
            element.gameObject.SetActive(true);
            return element;
        }

        if (AutoExpand)
        {
            return CreateObject(Prefab[UnityEngine.Random.Range(0, 2)], true);
        }

        throw new Exception($"There is no free element in pool of type {typeof(T)}");
    }

    private void CreatePool(int size, float ratio)
    {
        pool = new List<T>();
        var collection = new List<T>();

        System.Random rnd = new System.Random();

        int blackHoleCount  = Mathf.FloorToInt(size * ratio);
        int planetCount     = size - blackHoleCount;

        for (int i = 0; i < blackHoleCount; i++)
        {
            collection.Add(CreateObject(Prefab[1]));
        }

        for (int i = 0; i < planetCount; i++)
        {
            collection.Add(CreateObject(Prefab[0]));
        }

        pool = collection.OrderBy(x => rnd.Next()).ToList();
    }

    private T CreateObject(T objectToCreate, bool IsActiveByDefault = false)
    {
        var mono = UnityEngine.Object.Instantiate(objectToCreate, Container);
        mono.gameObject.SetActive(IsActiveByDefault);
        return mono;
    }
}

[System.Serializable]
public struct PoolData<T>
{
    [SerializeField] private bool _autoExpand;
    public bool AutoExpand { get => _autoExpand; private set => _autoExpand = value; }

    [SerializeField] private int _maxCapacity;
    public int MaxCapacity { get => _maxCapacity; }

    [SerializeField] private Transform _container;
    public Transform Container { get => _container; }

    [SerializeField] private T[] _prefab;
    public T[] Prefab { get => _prefab; }

    [HideInInspector] public float Ratio;
}