using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    private PoolData<SpaceObject> _poolData;

    private ObjectPool<SpaceObject> pool;
    private LevelData Selected;

    private float DistanceBetweenObjects => Random.Range(Selected.MinDistance, Selected.MaxDistance);

    private Vector3 LastPosition;

    public void SetData(int levelData)
    {
        Unsubscribe();

        Selected = Resources.Load<LevelData>($"LevelData/{levelData}");
        Debug.Log($"LEVEL: Current level - {Selected.name}");
        _poolData.Ratio = Selected.BlackHoleRatio;

        Subscribe();
    }

    public Level(int level, PoolData<SpaceObject> poolData)
    {
        _poolData = poolData;

        SetData(level);
    }

    public void EnableObjects()
    {
        pool = new ObjectPool<SpaceObject>(_poolData);

        for (int i = 0; i < _poolData.MaxCapacity - 5; i++)
        {
            pool.GetFreeElement().transform.position = LastPosition;
            LastPosition.x = Random.Range(ScreenHandler.MinScreenX + 5f, ScreenHandler.MaxScreenX - 5f);
            LastPosition.y += DistanceBetweenObjects;
        }
    }

    public void Reset()
    {
        LastPosition = Vector3.zero;

        if (pool != null)
            pool.Clear();
    }

    private void OnPlanetDisable()
    {
        pool.GetFreeElement().transform.position = LastPosition;
        LastPosition.x = Random.Range(ScreenHandler.MinScreenX + 5f, ScreenHandler.MaxScreenX - 5f);
        LastPosition.y += DistanceBetweenObjects;
    }

    private void Subscribe()
    {
        SpaceObject.OnPlanetDisable += OnPlanetDisable;
    }

    private void Unsubscribe()
    {
        SpaceObject.OnPlanetDisable -= OnPlanetDisable;
    }
}