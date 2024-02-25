using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] 
    private Ship _ship;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _ship.InOrbit)
            Ship.ExitTheOrbit();
    }
}