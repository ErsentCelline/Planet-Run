using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenHandler
{
    public static float MinScreenX => Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x; 
    public static float MaxScreenX => Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
    public static float MaxScreenY => Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
    public static float MinScreenY => Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
}