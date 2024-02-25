using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    [SerializeField] private DistanceVisualizer _prefab;

    private DistanceVisualizer _mark;
    private float MaxY = -22;

    private void Start()
    {
        MaxY = PlayerPrefs.GetFloat("MaxY", -22);

        _mark = Instantiate(_prefab, new Vector3(0, MaxY, 0), Quaternion.identity, transform);

        Ship.OnShipArriveToPlanet += SaveNewPosition;
        Ship.OnShipDisabled       += SetPoint;
    }

    private void SetPoint()
    {
        _mark.ChangePosition(new Vector3(0, MaxY, 0));
    }

    private void SaveNewPosition(SpaceObject spaceObject)
    {
        if (spaceObject.transform.position.y > MaxY && spaceObject.transform.position.y > 10)
        {
            MaxY = spaceObject.transform.position.y;
            if (MaxY > PlayerPrefs.GetFloat("MaxY"))
                PlayerPrefs.SetFloat("MaxY", MaxY);
        }
    }

    private void OnDestroy()
    {
        Ship.OnShipArriveToPlanet -= SaveNewPosition;
        Ship.OnShipDisabled       -= SetPoint;
    }
}