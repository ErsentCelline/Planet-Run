using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] 
    private AudioSource[] _audio;

    public void ChangeVolume(UnityEngine.UI.Slider slider)
    {
        float target = slider.value;

        foreach (var s in _audio)
            s.volume = target;
    }

    private void Start()
    {
        Ship.OnShipArriveToPlanet += delegate
        {
            _audio[0].Play();
        };
    }
}
