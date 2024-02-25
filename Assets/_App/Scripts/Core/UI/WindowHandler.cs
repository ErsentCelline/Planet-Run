using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowHandler : MonoBehaviour
{
    public static WindowHandler Instance { get; private set; }

    [SerializeField] private List<UIWindow> _uiWindows;

    private Vector2 WindowNormalSize;
    private Vector2 WindowTargetSize;

    public void ChangeSafeArea(Slider slider)
    {
        WindowTargetSize = WindowNormalSize - new Vector2(0, slider.value * 125);

        foreach (var w in _uiWindows)
        {
            w.GetComponent<RectTransform>().sizeDelta = WindowTargetSize;
        }
    }

    public void OpenWindow(string name)
    {
        var w = _uiWindows.Find(w => w.name == name);

        w.Enable();

        OnWindowOpen(w);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        WindowNormalSize = _uiWindows[0].GetComponent<RectTransform>().sizeDelta;

        WindowTargetSize = WindowNormalSize - new Vector2(0, 100);

        foreach (var w in _uiWindows)
            w.GetComponent<RectTransform>().sizeDelta = WindowTargetSize;
    }

    private void OnWindowOpen(UIWindow window)
    {
        foreach (var w in _uiWindows)
            if (w != window) 
                w.Disable();
    }
}
