using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nebula : MonoBehaviour
{
    [SerializeField] 
    private Material _nebula;
    [SerializeField] 
    private Color[] _mainColors;
    [SerializeField] 
    private Color[] _backColors;

    private const string MAIN_COLOR_KEY   = "_MainColor";
    private const string TARGET_COLOR_KEY = "_TargetColor";

    private int index = 1;

    private Color TargetMainColor { get => _mainColors[index]; }
    private Color TargetBackColor { get => _backColors[index]; }

    private Vector2 Offset = Vector2.zero;

    private void Update()
    {
        if (_nebula.GetColor(MAIN_COLOR_KEY) == TargetMainColor)
        {
            index++;
            if (index > 2) index = 0;
        }

        _nebula.SetColor(MAIN_COLOR_KEY,    Color.Lerp(_nebula.GetColor(MAIN_COLOR_KEY),   TargetMainColor, Time.deltaTime * .5f));
        _nebula.SetColor(TARGET_COLOR_KEY,  Color.Lerp(_nebula.GetColor(TARGET_COLOR_KEY), TargetBackColor, Time.deltaTime * .5f));

        Offset.y += (Time.deltaTime * Time.deltaTime);
        _nebula.mainTextureOffset = Offset;
    }
}
