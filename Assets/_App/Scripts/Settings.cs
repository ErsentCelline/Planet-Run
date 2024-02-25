using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] 
    private Slider _volume;
    [SerializeField] 
    private AudioSource[] audioSources;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void Start()
    {
        _volume.onValueChanged.AddListener(delegate { OnVolumeChanged(_volume); });
    }

    private void OnVolumeChanged(Slider volume)
    {
        foreach (var audio in audioSources)
        {
            audio.volume = volume.value;
        }
    }
}