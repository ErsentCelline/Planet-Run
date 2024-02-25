using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] 
    private PoolData<SpaceObject> _poolData;
    [SerializeField] 
    private Dropdown _dropdown;

    private Level Level;

    private void Awake()
    {
        Level = new Level(1, _poolData);

        Ship.OnShipDisabled += OnShipDisabled;

        _dropdown.onValueChanged.AddListener(delegate
        {
            Level.SetData(_dropdown.value + 1);
            if (Bonuses.SpeedEnabled)
                Time.timeScale = 1 + ((_dropdown.value + 1) / 50f);
            else
                Time.timeScale = 1 + ((_dropdown.value + 1) / 10f);
        });

        Transition.OnTransitionStart += Level.EnableObjects;
    }

    private void OnShipDisabled()
    {
        Level.Reset();
    }
}
