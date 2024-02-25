using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Level", fileName = "new Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField, Range(12f, 20f)] private float _minDistance, _maxDistance;

    public float MinDistance { get => _minDistance; }
    public float MaxDistance { get => _maxDistance; }

    [SerializeField, Range(0f, 1f)] private float _blackHoleToPlanetRatio;

    public float BlackHoleRatio { get => _blackHoleToPlanetRatio; }
}